@echo off

set "NuGetorDir=%CD%\NuGetor"
set "NuGetorExe=%NuGetorDir%\NuGetor.CLI.exe"

set "SolutionPath=%CD%\..\UVM.slnx"
set "ProjectDescriptor=%NuGetorDir%\project_descriptor.json"
set "OutputPath=C:\UVM\Packages"

if not exist "%NuGetorExe%" (
    echo ERROR: Missing executable: "%ProjectPackagerExe%"
    exit /b 1
)

if not exist "%SolutionPath%" (
    echo ERROR: Solution not found: "%SolutionPath%"
    exit /b 1
)

if not exist "%ProjectDescriptor%" (
    echo ERROR: Missing descriptor: "%ProjectDescriptor%"
    exit /b 1
)

for %%A in ("%ProjectDescriptor%") do set "ProjectDescriptor=%%~fA"
for %%A in ("%SolutionPath%") do set "SolutionPath=%%~fA"

"%NuGetorExe%" --package=true --descriptor="%ProjectDescriptor%" --sln="%SolutionPath%" --configuration="Release" --output="%OutputPath%" --symbols=false

exit /b %errorlevel%
pause
