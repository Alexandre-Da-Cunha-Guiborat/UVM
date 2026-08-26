using Microsoft.Extensions.Logging;
using System;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Logging;

namespace UVM.TUI.Demo;

internal class CsprojVersion : IVersion
{
    #region Public

    public static CsprojVersion BadVersion = new CsprojVersion(String.Empty);

    public UInt16 Major { get; private set; }

    public UInt16 Minor { get; private set; }

    public UInt16 Patch { get; private set; }

    public BuildType BuildT { get; private set; }

    public Byte SemVer { get; private set; }

    public UInt64 Version => (UInt64)Major << 48 | (UInt64)Minor << 32 | (UInt64)Patch << 16 | (UInt64)(Byte)BuildT << 8 | SemVer;

    public CsprojVersion(String version)
    {
        Major = 0;
        Minor = 0;
        Patch = 0;
        BuildT = BuildType.BuildType_NONE;
        SemVer = 0;

        if (version.Equals(String.Empty))
        {
            return;
        }

        string[] versionReleaseDevSplits = version.Split('-');
        if (!versionReleaseDevSplits.Length.Equals(1) && !versionReleaseDevSplits.Length.Equals(2))
        {
            _logger.Log(LogLevel.Warning, $"Could not parse the version, wrong format. ({version}, expecting X.X.X or X.X.X-X.X)");
            return;
        }

        string[] releaseDigits = versionReleaseDevSplits[0].Split('.');
        if (!releaseDigits.Length.Equals(3))
        {
            _logger.Log(LogLevel.Warning, $"Could not parse the version, wrong format. ({version})");
            return;
        }

        UInt16 major;
        UInt16 minor;
        UInt16 patch;

        if (!UInt16.TryParse(releaseDigits[0], out major))
        {
            _logger.Log(LogLevel.Warning, $"Could not parse the {nameof(Major)} digit to a {nameof(UInt16)}. ({releaseDigits[0]})");
            return;
        }

        if (!UInt16.TryParse(releaseDigits[1], out minor))
        {
            _logger.Log(LogLevel.Warning, $"Could not parse the {nameof(Minor)} digit to a {nameof(UInt16)}. ({releaseDigits[1]})");
            return;
        }

        if (!UInt16.TryParse(releaseDigits[2], out patch))
        {
            _logger.Log(LogLevel.Warning, $"Could not parse the {nameof(Patch)} digit to a {nameof(UInt16)}. ({releaseDigits[2]})");
            return;
        }

        Major = major;
        Minor = minor;
        Patch = patch;

        if (versionReleaseDevSplits.Length.Equals(1))
        {
            BuildT = BuildType.RELEASE;
            SemVer = 0;
        }
        else
        {
            String[] devDigits = versionReleaseDevSplits[1].Split('.');
            BuildType buildT;
            Byte semVer;

            if (!devDigits.Length.Equals(2))
            {
                _logger.Log(LogLevel.Warning, $"Could not parse the version, wrong format. ({version})");
                return;
            }

            if (!Enum.TryParse(devDigits[0], out buildT))
            {
                _logger.Log(LogLevel.Warning, $"Could not parse the {nameof(BuildType)} digit to a {nameof(BuildType)}. ({devDigits[0]})");
                return;
            }

            if (!Byte.TryParse(devDigits[1], out semVer))
            {
                _logger.Log(LogLevel.Warning, $"Could not parse the {nameof(SemVer)} digit to a {nameof(Byte)}. ({devDigits[1]})");
                return;
            }

            BuildT = buildT;
            SemVer = semVer;
        }
    }

    public Boolean Upgrade(BuildType buildT, DigitType digitT)
    {
        switch (buildT)
        {
            case BuildType.RELEASE:
                return _UpgradeRelease(digitT);

            case BuildType.ALPHA:
                return _UpgradeAlpha(digitT);

            case BuildType.BETA:
                return _UpgradeBeta(digitT);

            case BuildType.RC:
                return _UpgradeRc(digitT);

            default:
                _logger.Log(LogLevel.Warning, $"The given {nameof(BuildType)} is not supported. Please use ({BuildType.RELEASE.ToString()}, {BuildType.ALPHA.ToString()}, {BuildType.BETA.ToString()}, {BuildType.RC.ToString()})");
                return false;
        }
    }

    public Int32 CompareTo(IVersion? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (Version < other.Version)
        {
            return -1;
        }
        else if (Version > other.Version)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public Boolean Equals(IVersion? other)
    {
        if (other is null)
        {
            return false;
        }

        return Version.Equals(other.Version);
    }

    public override String ToString()
    {
        if (BuildT.Equals(BuildType.RELEASE))
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        return $"{Major}.{Minor}.{Patch}-{BuildT.ToString().ToLower()}.{SemVer}";
    }

    #endregion Public

    #region Private

    private readonly ILogger _logger = UvmLogger.Instance;

    private Boolean _UpgradeRelease(DigitType digitT)
    {
        switch (digitT)
        {
            case DigitType.MAJOR:
                Major += 1;
                Minor = 0;
                Patch = 0;
                BuildT = BuildType.RELEASE;
                SemVer = 0;
                return true;

            case DigitType.MINOR:
                Minor += 1;
                Patch = 0;
                BuildT = BuildType.RELEASE;
                SemVer = 0;
                return true;

            case DigitType.PATCH:
                Patch += 1;
                BuildT = BuildType.RELEASE;
                SemVer = 0;
                return true;

            default:
                _logger.Log(LogLevel.Warning, $"Upgrading the version to a {BuildType.RELEASE.ToString()}, while targeting the {nameof(digitT.SEMVER)}  is not supported.");
                return false;
        }
    }

    private Boolean _UpgradeAlpha(DigitType digitT)
    {
        switch (digitT)
        {
            case DigitType.SEMVER:
                if (BuildT.Equals(BuildType.ALPHA))
                {
                    SemVer += 1;
                }
                else
                {
                    SemVer = 1;
                }

                BuildT = BuildType.ALPHA;
                return true;

            default:
                _logger.Log(LogLevel.Warning, $"Upgrading the version to a {BuildType.ALPHA.ToString()}, while targeting anything other than {nameof(digitT.SEMVER)}  is not supported.");
                return false;
        }
    }

    private Boolean _UpgradeBeta(DigitType digitT)
    {
        switch (digitT)
        {
            case DigitType.SEMVER:
                if (BuildT.Equals(BuildType.BETA))
                {
                    SemVer += 1;
                }
                else
                {
                    SemVer = 1;
                }

                BuildT = BuildType.BETA;
                return true;

            default:
                _logger.Log(LogLevel.Warning, $"Upgrading the version to a {BuildType.BETA.ToString()}, while targeting anything other than {nameof(digitT.SEMVER)}  is not supported.");
                return false;
        }
    }

    private Boolean _UpgradeRc(DigitType digitT)
    {
        switch (digitT)
        {
            case DigitType.SEMVER:
                if (BuildT.Equals(BuildType.RC))
                {
                    SemVer += 1;
                }
                else
                {
                    SemVer = 1;
                }

                BuildT = BuildType.RC;
                return true;

            default:
                _logger.Log(LogLevel.Warning, $"Upgrading the version to a {BuildType.RC.ToString()}, while targeting anything other than {nameof(digitT.SEMVER)}  is not supported.");
                return false;
        }
    }

    #endregion Private
}
