import os
import pathlib
import sys

CMD_CLEAN_DEBUG = "dotnet clean --configuration Debug"
CMD_CLEAN_RELEASE = "dotnet clean --configuration Release"
CMD_RESTORE = "dotnet restore --configfile"
CMD_BUILD_DEBUG = "dotnet build --configuration Debug "
CMD_BUILD_RELEASE = "dotnet build --configuration Release "
CMD_PACK_DEBUG = "dotnet pack --configuration Debug --include-symbols --version-suffix \"DEBUG\""
CMD_PACK_RELEASE = "dotnet pack --configuration Release"

MODE_RELEASE = True
PROJECT_NAME = "UVM.Interface"
CSPROJFOLDER_NAME = f"../10-Common/{PROJECT_NAME}"
CSPROJ_NAME = f"{PROJECT_NAME}.csproj"

NUGET_CONFIG_FILE_NAME = "../nuget.config"
NUGET_OUTPUTFOLDER_DEBUG = f"/UVM/Packages/Debug/{PROJECT_NAME}"
NUGET_OUTPUTFOLDER_RELEASE = f"/UVM/Packages/Release/{PROJECT_NAME}"

def run_cmd(str_cmd) :
    print(f"Running : {str_cmd}")
    os.system(str_cmd)
    print(f"Done")

def debug_pack(str__csproj_abspath) :
    # Restore Csproj 
    str__nuget_config_abspath = f"{str__script_parent_abspath}/{NUGET_CONFIG_FILE_NAME}"
    str__cmd_restore = f"{CMD_RESTORE} {str__nuget_config_abspath} {str__csproj_abspath}"
    run_cmd(str__cmd_restore)

    # Clean Csproj
    str__cmd_clean_debug = f"{CMD_CLEAN_DEBUG} {str__csproj_abspath}"
    run_cmd(str__cmd_clean_debug)

    # Build Csproj
    str__cmd_build_debug = f"{CMD_BUILD_DEBUG} {str__csproj_abspath}"
    run_cmd(str__cmd_build_debug)
    

    # Package Nuspec
    str__cmd_pack_debug = f"{CMD_PACK_DEBUG} {str__csproj_abspath} -o \"{NUGET_OUTPUTFOLDER_DEBUG}\""
    run_cmd(str__cmd_pack_debug)

def release_pack(str__csproj_abspath) :

    # Restore
    str__nuget_config_abspath = f"{str__script_parent_abspath}/{NUGET_CONFIG_FILE_NAME}"
    str__cmd_restore = f"{CMD_RESTORE} {str__nuget_config_abspath} {str__csproj_abspath}"
    run_cmd(str__cmd_restore)

    # Clean
    str__cmd_clean_release = f"{CMD_CLEAN_RELEASE} {str__csproj_abspath}"
    run_cmd(str__cmd_clean_release)

    # Build
    str__cmd_build_release = f"{CMD_BUILD_RELEASE} {str__csproj_abspath}"
    run_cmd(str__cmd_build_release)

    # Pack
    str__cmd_pack_release = f"{CMD_PACK_RELEASE} {str__csproj_abspath} -o \"{NUGET_OUTPUTFOLDER_RELEASE}\""
    run_cmd(str__cmd_pack_release)

if __name__ == "__main__" :

    args = sys.argv
    
    if(args[-1] == "DEBUG") :
        MODE_RELEASE = False

    # Script
    str__script_abspath = os.path.abspath(__file__)
    path__script_parent_abspath = pathlib.Path(str__script_abspath)
    str__script_parent_abspath = str(path__script_parent_abspath.parent.absolute()).replace("\\", "/")

    # Csproj
    str__csproj_relative_path_to_script = f"{CSPROJFOLDER_NAME}/{CSPROJ_NAME}"
    str__csproj_abspath = f"{str__script_parent_abspath}/{str__csproj_relative_path_to_script}"
    # print(str__csproj_abspath)

    if(MODE_RELEASE) :
        release_pack(str__csproj_abspath)
    
    else :
        debug_pack(str__csproj_abspath)
    



