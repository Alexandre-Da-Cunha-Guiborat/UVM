using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UVM.Interface;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Logging;

namespace UVM.TUI.Demo;

internal class CsprojFile : IVersionable, IGenerable
{
    #region Public

    public String Id { get; }

    public String Path { get; }

    public IVersion Version { get; }

    public IEnumerable<IVersionable> Dependencies { get; private set; }

    public Boolean IsPackable { get; private set; }

    public CsprojFile(String filepath)
    {
        const string extension = ".xml";

        FileInfo fInfo = new FileInfo(filepath);
        if (fInfo.Exists && fInfo.Extension.Equals(extension))
        {
            Path = fInfo.FullName.Replace("\\", "/");

            _xmlFile = XDocument.Load(filepath);
            if (_xmlFile is null)
            {
                Id = String.Empty;
                Version = CsprojVersion.BadVersion;
                IsPackable = false;
            }
            else
            {
                Id = _ReadId();
                Version = _ReadVersion();
                IsPackable = _ReadIsPackable();
            }

            Dependencies = [];
        }
        else
        {
            Id = String.Empty;
            Path = String.Empty;
            Version = CsprojVersion.BadVersion;
            Dependencies = [];
            IsPackable = false;
            _logger.Log(LogLevel.Error, $"The given path : {filepath}, is not leading to a .csproj file.");
        }
    }

    public void ComputeDependencies(IEnumerable<IVersionable> vfPool)
    {
        Dependencies = _ComputeDep(vfPool);
    }

    public Boolean Upgrade(BuildType buildT, DigitType digitT)
    {
        if (Version.Upgrade(buildT, digitT))
        {
            _UpdateVersion();
            _UpdateDependencies();
            return true;
        }

        return false;
    }

    public Boolean DumpFile()
    {
        if (_xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return false;
        }

        FileStream Fs = File.Create($@"{Path}");
        String xmlString = _xmlFile.ToString();
        Byte[] info = new UTF8Encoding(true).GetBytes(xmlString);
        Fs.Write(info, 0, info.Length);
        Fs.Close();

        return true;
    }

    public Boolean Generate(IDictionary<string, string> args)
    {
        _runCleanCmd();
        _runRestoreCmd();
        _runBuildCmd(args);
        return _runPackCmd(args).Equals(0);
    }

    #endregion Public

    #region Private

    private readonly ILogger _logger = UvmLogger.Instance;

    private XDocument? _xmlFile;

    private String _ReadId()
    {
        if (_xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return String.Empty;
        }

        XElement? csprojXMLRoot = _xmlFile.Root;
        if (csprojXMLRoot is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return String.Empty;
        }

        IEnumerable<XElement>? propertyGroupElement = csprojXMLRoot.Elements("PropertyGroup");
        if (propertyGroupElement is null)
        {
            _logger.LogWarning($"The .csproj file has no element <PropertyGroup>. ({Path})");
            return String.Empty;
        }

        XElement? packageId = propertyGroupElement.Elements("PackageId").LastOrDefault();
        if (packageId is null)
        {
            return String.Empty;
        }

        return packageId.Value;
    }

    private IVersion _ReadVersion()
    {
        if (_xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return CsprojVersion.BadVersion;
        }

        XElement? csprojXMLRoot = _xmlFile.Root;
        if (csprojXMLRoot is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return CsprojVersion.BadVersion;
        }

        IEnumerable<XElement>? propertyGroupElement = csprojXMLRoot.Elements("PropertyGroup");
        if (propertyGroupElement is null)
        {
            _logger.LogWarning($"The .csproj file has no element <PropertyGroup>. ({Path})");
            return CsprojVersion.BadVersion;
        }

        XElement? versionPrefix = propertyGroupElement.Elements("VersionPrefix").LastOrDefault();
        XElement? versionSuffix = propertyGroupElement.Elements("VersionSuffix").LastOrDefault();
        if (versionPrefix is null || versionSuffix is null)
        {
            return CsprojVersion.BadVersion;
        }

        if (versionSuffix.Value.Equals(String.Empty))
        {
            return new CsprojVersion($"{versionPrefix.Value}");
        }

        return new CsprojVersion($"{versionPrefix.Value}-{versionSuffix.Value}");
    }

    private Boolean _ReadIsPackable()
    {
        if (this._xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return false;
        }

        XElement? csprojXMLRoot = _xmlFile.Root;
        if (csprojXMLRoot is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return false;
        }

        IEnumerable<XElement>? propertyGroupElement = csprojXMLRoot.Elements("PropertyGroup");
        if (propertyGroupElement is null)
        {
            _logger.LogWarning($"The .csproj ({Path}) file has no element <PropertyGroup>.");
            return false;
        }

        XElement? packageId = propertyGroupElement.Elements("IsPackable").LastOrDefault();
        if (packageId is null)
        {
            return false;
        }

        XElement? isPackable = propertyGroupElement.Elements("IsPackable").LastOrDefault();
        if (isPackable is null)
        {
            return false;
        }

        return isPackable.Value.Equals("true");
    }

    private IEnumerable<T> _ComputeDep<T>(IEnumerable<T> vfPool) where T : IVersionable
    {
        if (_xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return [];
        }

        XElement? csprojXMLRoot = _xmlFile.Root;
        if (csprojXMLRoot is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return [];
        }

        IEnumerable<XElement> packageReferenceElements = csprojXMLRoot.Elements("ItemGroup")?.Elements("PackageReference") ?? [];
        IEnumerable<String> packageReferenceIds = packageReferenceElements.Where(dep => dep.Attribute("Include") is not null).Select(dep => dep.Attribute("Include")?.Value ?? String.Empty);
        IEnumerable<T> packageDep = vfPool.Where(vf => packageReferenceIds.Contains(vf.Id));
        return packageDep;
    }

    private void _UpdateDependencies()
    {
        if (_xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return;
        }

        XElement? csprojXMLRoot = _xmlFile.Root;
        if (csprojXMLRoot is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return;
        }

        IEnumerable<XElement> packageReferenceElements = csprojXMLRoot.Elements("ItemGroup")?.Elements("PackageReference") ?? [];
        //IEnumerable<XElement>? itemGroupElementsRelease = itemGroupElements.Where(item => item.Attribute("Label")?.Value.Equals("Release_Dependencies") ?? false);
        foreach (XElement pkgRef in packageReferenceElements)
        {
            XAttribute? pkgRefInclude = pkgRef.Attribute("Include");
            if (pkgRefInclude is null)
            {
                _logger.LogWarning($"A package reference has a <PackageReference> tag with no Include attribute. ({Path})");
                continue;
            }

            IVersionable? dependency = Dependencies.FirstOrDefault(dep => dep.Id.Equals(pkgRefInclude.Value));
            if (dependency is not null && !dependency.Version.Equals(CsprojVersion.BadVersion))
            {
                pkgRef.SetAttributeValue("VersionOverride", dependency.Version.ToString());
            }
        }
    }

    private void _UpdateVersion()
    {
        if (_xmlFile is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return;
        }

        XElement? csprojXMLRoot = _xmlFile.Root;
        if (csprojXMLRoot is null)
        {
            _logger.LogWarning($"Could not parse the csproj xml file. ({Path})");
            return;
        }

        IEnumerable<XElement>? propertyGroupElement = csprojXMLRoot.Elements("PropertyGroup");
        if (propertyGroupElement is null)
        {
            _logger.LogWarning($"A package reference has a <PackageReference> tag with no Include attribute. ({Path})");
            return;
        }

        XElement? versionPrefix = propertyGroupElement.Elements("VersionPrefix").LastOrDefault();
        XElement? versionSuffix = propertyGroupElement.Elements("VersionSuffix").LastOrDefault();
        if (versionPrefix is null || versionSuffix is null)
        {
            return;
        }

        versionPrefix.Value = $"{Version.Major}.{Version.Minor}.{Version.Patch}";
        versionSuffix.Value = Version.BuildT.Equals(BuildType.RELEASE) ? string.Empty : $"-{Version.BuildT.ToString()}.{Version.SemVer}";
    }

    private Int32 _runCmd(String cmd)
    {
        _logger.Log(LogLevel.Information, $"running : {cmd}");
        return 0;

        // String shellPath = String.Empty;
        // String shellArgs = String.Empty;
        // if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        // {
        //     shellPath = "cmd.exe";
        //     shellArgs = $"/c {cmd}";
        // }
        // else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        // {
        //     shellPath = "/bin/bash";
        //     shellArgs = $"-c \"{cmd}\"";
        // }

        // Process procWithSourcing = new Process();
        // ProcessStartInfo pStartInfo = new ProcessStartInfo
        // {
        //     FileName = shellPath,
        //     Arguments = shellArgs,
        //     CreateNoWindow = false,
        // };
        // procWithSourcing.StartInfo = pStartInfo;
        // procWithSourcing.Start();
        // procWithSourcing.WaitForExit();

        // return procWithSourcing.ExitCode;
    }

    private Int32 _runRestoreCmd()
    {
        return _runCmd($"dotnet restore \"{Path}\"");
    }

    private Int32 _runCleanCmd()
    {
        return _runCmd($"dotnet clean \"{Path}\"");
    }

    private Int32 _runBuildCmd(IDictionary<String, String> args)
    {
        if (args.ContainsKey("--configuration"))
        {
            String configuration = args["--configuration"];
            return _runCmd($"dotnet build \"{Path}\" --configuration {configuration}");
        }

        return _runCmd($"dotnet build \"{Path}\" --configuration Release");
    }

    private Int32 _runPackCmd(IDictionary<String, String> args)
    {
        if (args.ContainsKey("--configuration") && args.ContainsKey("--output"))
        {
            String configuration = args["--configuration"];
            String ouputPath = args["--output"];
            return _runCmd($"dotnet pack \"{Path}\" --configuration {configuration} --output \"{ouputPath}\"");
        }

        return _runCmd($"dotnet pack \"{Path}\" --configuration Release --output \"{UvmConstant.UVM_PACKAGE_FOLDER_PATH_DEFAULT}\"");
    }

    #endregion Private
}
