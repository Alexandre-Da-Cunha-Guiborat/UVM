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
        /// <param name="fileToDump">VersionnableFile to dump to the filesystem.</param>
        /// <param name="pathToDump">String representation of the absolute path to use, for file dumping.</param>
        /// <returns>true => dumped successed, false => otherwise.</returns>
        public static bool Dump(I_VersionnableFile fileToDump, string pathToDump)
        {
            string title = UVMLogger.CreateTitle(_asmName, _className, $"Dump");
            string message = $"Dumping file {fileToDump.VFName} to {pathToDump}.";
            UVMLogger.AddLog(LogLevel.Information, title, message);

            fileToDump.Dump(pathToDump);
            return true;
        }

        /// <summary>
        /// Dump a list of VersionnableFile to the filesystem at the given paths.
        /// </summary>
        /// <param name="filesToDump">List of VersionnableFile to dump to the filesystem.</param>
        /// <param name="pathsToDump">List of String representation of the absolute path to use, for file dumping.</param>
        /// <returns>true => all dump successed, false => otherwise.</returns>
        public static bool Dump(List<I_VersionnableFile> filesToDump, List<string> pathsToDump)
        {
            if (filesToDump.Count != pathsToDump.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"Dump");
                string message = $"filesToDump and pathsToDump must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < filesToDump.Count; i++)
            {
                I_VersionnableFile fileToDump = filesToDump[i];
                string pathToDump = pathsToDump[i];

                Dump(fileToDump, pathToDump);
            }
            return true;
        }

        /// <summary>
        /// Dump a list of list of VersionnableFile to the filesystem at the given paths.
        /// </summary>
        /// <param name="filesToDump">List of List of VersionnableFile to dump to the filesystem.</param>
        /// <param name="pathsToDump">List of List of String representation of the absolute path to use, for file dumping.</param>
        /// <returns>true => all dump successed, false => otherwise.</returns>
        public static bool Dump(List<List<I_VersionnableFile>> filesToDump, List<List<string>> pathsToDump)
        {
            if (filesToDump.Count != pathsToDump.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"Dump");
                string message = $"filesToDump and pathsToDump must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < filesToDump.Count; i++)
            {
                List<I_VersionnableFile> filesToDumpSub = filesToDump[i];
                List<string> pathsToDumpSub = pathsToDump[i];

                Dump(filesToDumpSub, pathsToDumpSub);
            }
            return true;
        }

        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Public

        #region Protected

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


