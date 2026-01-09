using System;
using System.Collections.Generic;
using System.Reflection;
using UVM.Interface.Interfaces;
using UVM.Logging;
using UVM.Logging.Enums;

namespace UVM.Engine
{
    /// <summary>
    /// Library for <see cref="I_VersionableFile"> dumping.
    /// </summary>
    public class UVMWriter
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Dump the <see cref="I_VersionableFile"> to the filesystem at the given path.
        /// </summary>
        /// <param name="vfToDump"><see cref="I_VersionableFile"> to dump to the filesystem.</param>
        /// <param name="outputPath"><see cref="String"> representation of the absolute path to use, for file dumping.</param>
        /// <returns><see langword="true"/> => dumped succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean DumpFile(I_VersionableFile vfToDump, String outputPath)
        {
            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(DumpFile)}");
            String message = $"Dumping file {vfToDump.VFName} to {outputPath}.";
            UVMLogger.AddLog(E_LogLevel.INFO, title, message);

            vfToDump.DumpFile(outputPath);
            return true;
        }

        /// <summary>
        /// Dump a <see cref="List{T}"> of <see cref="I_VersionableFile"> to the filesystem at the given paths.
        /// </summary>
        /// <param name="vfsToDump"><see cref="List{T}"> of <see cref="I_VersionableFile"> to dump to the filesystem.</param>
        /// <param name="outputPaths"><see cref="List{T}"> of <see cref="String"> representation of the absolute path to use, for file dumping.</param>
        /// <returns><see langword="true"/> => dumped succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean DumpFiles(List<I_VersionableFile> vfsToDump, List<string> outputPaths)
        {
            if (vfsToDump.Count != outputPaths.Count)
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(DumpFile)}");
                String message = $"filesToDump and pathsToDump must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);
                return false;
            }

            Boolean success = true;
            for (int i = 0; i < vfsToDump.Count; i++)
            {
                I_VersionableFile fileToDump = vfsToDump[i];
                String pathToDump = outputPaths[i];

                if (DumpFile(fileToDump, pathToDump) is false)
                {
                    success = false;
                }

            }
            return success;
        }

        /// <summary>
        /// Dump a <see cref="List{T}"> of <see cref="List{T}"> of <see cref="I_VersionableFile"> to the filesystem at the given paths.
        /// </summary>
        /// <param name="vfsToDump"><see cref="List{T}"> of <see cref="List{T}"> of <see cref="I_VersionableFile"> to dump to the filesystem.</param>
        /// <param name="outputPaths"><see cref="List{T}"> of <see cref="List{T}"> of <see cref="String"> representation of the absolute path to use, for file dumping.</param>
        /// <returns><see langword="true"/> => dumped succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean DumpFiles(List<List<I_VersionableFile>> vfsToDump, List<List<string>> outputPaths)
        {
            if (vfsToDump.Count != outputPaths.Count)
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(DumpFile)}");
                String message = $"filesToDump and pathsToDump must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);
                return false;
            }

            Boolean success = true;
            for (int i = 0; i < vfsToDump.Count; i++)
            {
                List<I_VersionableFile> filesToDumpSub = vfsToDump[i];
                List<String> pathsToDumpSub = outputPaths[i];
                if (DumpFiles(filesToDumpSub, pathsToDumpSub) is false)
                {
                    success = false;
                }
            }
            return success;
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private
        // TBD
        #endregion Private

        #region DEBUG

        /// <summary>
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        private static String _className = nameof(UVMWriter);

        #endregion DEBUG
    }
}
