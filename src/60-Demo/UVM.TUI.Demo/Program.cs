using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UVM.Engine;
using UVM.Interface.Enums;
using UVM.TUI.Demo;

AnsiConsole.MarkupLine("[bold red]Welcome to UVM.TUI.Demo.[/]");

String gitDirectoryPath = AnsiConsole.Ask<String>($"Please enter the absolute path to the git directory > ");
String commitIdRef = AnsiConsole.Ask<String>($"Please enter the reference commit id > ");
String CommitId = AnsiConsole.Ask<String>($"Please enter the target commit id > ");

SelectionPrompt<string> configurationPrompt = new SelectionPrompt<string>();
configurationPrompt.Title($"Please choose the desired configuration > ");
configurationPrompt.AddChoices(["Release", "Debug"]);
String configuration = AnsiConsole.Prompt(configurationPrompt);
AnsiConsole.MarkupLine($"Please choose the desired configuration > {configuration}");


SelectionPrompt<String> symbolPrompt = new SelectionPrompt<string>();
symbolPrompt.Title($"Please choose yes to add symbols to the output > ");
symbolPrompt.AddChoices(["no", "yes"]);
String symbol = AnsiConsole.Prompt(symbolPrompt);
AnsiConsole.MarkupLine($"Please choose yes to add symbols to the output > {symbol}");

SelectionPrompt<String> buildTypePrompt = new SelectionPrompt<string>();
buildTypePrompt.Title($"Please choose the desired build type > ");
buildTypePrompt.AddChoices(Enum.GetValues<BuildType>().Select(bt => bt.ToString()));
BuildType buildT = Enum.Parse<BuildType>(AnsiConsole.Prompt(buildTypePrompt));
AnsiConsole.MarkupLine($"Please choose the desired build type > {buildT}");

SelectionPrompt<String> digitTypePrompt = new SelectionPrompt<string>();
digitTypePrompt.Title($"Please choose the desired digit to upgrade > ");
digitTypePrompt.AddChoices(Enum.GetValues<DigitType>().Select(bt => bt.ToString()));
DigitType digitT = Enum.Parse<DigitType>(AnsiConsole.Prompt(digitTypePrompt));
AnsiConsole.MarkupLine($"Please choose the desired digit to upgrade > {digitT}");

// For simplicity reason, we will create false modified files
// However, that is an example on how to do it for your project.
// IEnumerable<IVersionable> vfPool = []; // Fill it using your criteria.
// IEnumerable<string> modifiedFiles = UvmGitUtils.GetGitDiff(gitDirectoryPath, commitIdRef, CommitId);
// IEnumerable<IVersionable> roots = vfPool.Where(vf => modifiedFiles.Contains($@"{vf.DirPath}\{vf.Name}.{vf.Extension}"));

String uvmSlnPath = AnsiConsole.Ask<String>($"Please enter the absolute path to the UVM.slnx file > ");
FileInfo uvmSlnFileInfo = new FileInfo(uvmSlnPath);
DirectoryInfo? uvmSlnDirectoryInfo = uvmSlnFileInfo.Directory;
if (uvmSlnDirectoryInfo is null)
{
    AnsiConsole.MarkupLine("[bold red]The given path to UVM.slnx can not be used. Please ensure you gave the right path, or move the project to another directory.[/]");
    return;
}

String asm1_1 = $@"{uvmSlnDirectoryInfo.FullName}/60-Demo/UVM.TUI.Demo/Resources/Asm_1_1.xml".Replace("\\", "/");
String asm2_1 = $@"{uvmSlnDirectoryInfo.FullName}/60-Demo/UVM.TUI.Demo/Resources/Asm_2_1.xml".Replace("\\", "/");
String asm2_2 = $@"{uvmSlnDirectoryInfo.FullName}/60-Demo/UVM.TUI.Demo/Resources/Asm_2_2.xml".Replace("\\", "/");
String asm3_1 = $@"{uvmSlnDirectoryInfo.FullName}/60-Demo/UVM.TUI.Demo/Resources/Asm_3_1.xml".Replace("\\", "/");
List<string> asmPaths = [asm1_1, asm2_1, asm2_2, asm3_1];

CsprojFile[] vfPool = asmPaths.Select(mf => new CsprojFile(mf)).Where(csproj => csproj.IsPackable).ToArray();
for (Int32 i = 0; i < vfPool.Count(); i++)
{
    vfPool[i].ComputeDependencies(vfPool);
}

List<string> modifiedFiles = [asm2_2];
List<CsprojFile> roots = vfPool.Where(vf => modifiedFiles.Contains($@"{vf.Path}")).ToList();

IEnumerable<IEnumerable<CsprojFile>> childrenTree = UvmManager.ComputeChildrenTree(vfPool, roots);

IEnumerable<IEnumerable<BuildType>> buildTs = childrenTree.Select(subTree => subTree.Select(node => buildT));
IEnumerable<IEnumerable<DigitType>> digitTs = childrenTree.Select(subTree => subTree.Select(node => digitT));
UvmUpgrader.UpgradeFiles(childrenTree, buildTs, digitTs);

UvmDumper.DumpFiles(childrenTree);

IEnumerable<IEnumerable<IDictionary<String, String>>> arguments = childrenTree.Select(subTree => subTree.Select(node => new Dictionary<String, String>()));
UvmPackager.GenerateFiles(childrenTree, arguments);