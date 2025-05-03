import os
import pathlib
import sys

LIST_NuGetScript_Ordered = ["Gen_NuGet_UVM.Interface.py", 
                            "Gen_NuGet_UVM.Logging.py", 
                            "Gen_NuGet_UVM.Engine.py", 
                            "Gen_NuGet_UVM.Service.py", 
                            "Gen_NuGet_UVM.SDK.py"]

def run_cmd(str_cmd) :
    print(f"Running : {str_cmd}")
    os.system(str_cmd)
    print(f"Done")

if __name__ == "__main__" :
    args = sys.argv

    # Clear all cached packages to get rid of potential duplicates and restoring a previous version.
    # run_cmd("dotnet nuget locals --clear all")

    for nuget in LIST_NuGetScript_Ordered :
        str_cmd = f"python3 ./{nuget} {args[-1]}"
        run_cmd(str_cmd)




