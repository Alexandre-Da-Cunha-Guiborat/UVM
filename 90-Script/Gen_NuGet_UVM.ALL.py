import os
import sys

LIST__NUGET_SCRIPT_ORDERED = [
    "Gen_NuGet_UVM.Interface.py",

    "Gen_NuGet_UVM.Logging.py",

    "Gen_NuGet_UVM.Engine.py",

    "Gen_NuGet_UVM.Service.py",
    
    "Gen_NuGet_UVM.SDK.py"
    ]

if __name__ == "__main__" :

    # Configuration
    args = sys.argv
    if(len(args) != 2) :
        exit()

    str__configuration = args[1]

    for script in LIST__NUGET_SCRIPT_ORDERED :
        str_cmd = f"python3 ./{script} {str__configuration}"

        print(f"Running : {str_cmd}")
        os.system(str_cmd)
        print(f"\n\n\n")





