using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using UVM.Interface;
using UVM.Logging;

namespace UVM.Engine
{
    public class UVMWriter
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly.
        /// </summary>
        private const string _asmName = "UVM.Engine";

        /// <summary>
        /// String representation of the class.
        /// </summary>
        private const string _className = "UVMWriter";

        #endregion DEBUG

        #region Public

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function

        /// <summary>
        /// Dump the VersionnableFile to the filesystem at the given path.
        /// </summary>
        /// <param name="vfToDump">VersionnableFile to dump to the filesystem.</param>
        /// <param name="outputPath">String representation of the absolute path to use, for file dumping.</param>
        /// <returns>true => dumped successed, false => otherwise.</returns>
        public static bool DumpFile(I_VersionnableFile vfToDump, string outputPath)
        {
            string title = UVMLogger.CreateTitle(_asmName, _className, $"Dump");
            string message = $"Dumping file {vfToDump.VFName} to {outputPath}.";
            UVMLogger.AddLog(LogLevel.Information, title, message);

            vfToDump.DumpFile(outputPath);
            return true;
        }

        /// <summary>
        /// Dump a list of VersionnableFile to the filesystem at the given paths.
        /// </summary>
        /// <param name="vfsToDump">List of VersionnableFile to dump to the filesystem.</param>
        /// <param name="outputPaths">List of String representation of the absolute path to use, for file dumping.</param>
        /// <returns>true => all dump successed, false => otherwise.</returns>
        public static bool DumpFiles(List<I_VersionnableFile> vfsToDump, List<string> outputPaths)
        {
            if (vfsToDump.Count != outputPaths.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"Dump");
                string message = $"filesToDump and pathsToDump must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < vfsToDump.Count; i++)
            {
                I_VersionnableFile fileToDump = vfsToDump[i];
                string pathToDump = outputPaths[i];

                DumpFile(fileToDump, pathToDump);
            }
            return true;
        }

        /// <summary>
        /// Dump a list of list of VersionnableFile to the filesystem at the given paths.
        /// </summary>
        /// <param name="vfsToDump">List of List of VersionnableFile to dump to the filesystem.</param>
        /// <param name="outputPaths">List of List of String representation of the absolute path to use, for file dumping.</param>
        /// <returns>true => all dump successed, false => otherwise.</returns>
        public static bool DumpFiles(List<List<I_VersionnableFile>> vfsToDump, List<List<string>> outputPaths)
        {
            if (vfsToDump.Count != outputPaths.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"Dump");
                string message = $"filesToDump and pathsToDump must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < vfsToDump.Count; i++)
            {
                List<I_VersionnableFile> filesToDumpSub = vfsToDump[i];
                List<string> pathsToDumpSub = outputPaths[i];

                DumpFiles(filesToDumpSub, pathsToDumpSub);
            }
            return true;
        }

        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Public

        #region Protected

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Protected

        #region Private

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Private
    }
}


