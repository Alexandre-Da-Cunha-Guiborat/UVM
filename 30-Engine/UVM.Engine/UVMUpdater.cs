using System;
using System.Collections.Generic;
using System.Reflection;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Logging;
using UVM.Logging.Enums;

namespace UVM.Engine
{
    /// <summary>
    /// Library for <see cref="I_VersionableFile"> updating.
    /// </summary>
    public class UVMUpdater
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Update the given file. (pass it the arguments.)
        /// </summary>
        /// <param name="vfToUpdate"><see cref="I_VersionableFile"/> representation of the file to update.</param>
        /// <param name="versionIndexes"><see cref="List{T}"/> of version indexes to update.</param>
        /// <param name="buildTs"><see cref="List{T}"/> of <see cref="BuildType"/> to use.</param>
        /// <param name="digitTs"><see cref="List{T}"/> of <see cref="DigitType"/> to upgrade.</param>
        /// <param name="semiVersions"><see cref="List{T}"/> of semi version to use for upgrading the version.</param>
        /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
        static public Boolean UpdateFile(I_VersionableFile vfToUpdate,
             List<UInt16> versionIndexes,
             List<BuildType> buildTs,
             List<DigitType> digitTs,
             List<UInt16> semiVersions)
        {
            if (versionIndexes.Count != buildTs.Count || versionIndexes.Count != digitTs.Count || versionIndexes.Count != semiVersions.Count)
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(UpdateFiles)}");
                String message = $"{nameof(versionIndexes)}, {nameof(buildTs)}, {nameof(digitTs)} and {nameof(semiVersions)} must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);
                return false;
            }

            vfToUpdate.Update(versionIndexes, buildTs, digitTs, semiVersions);
            return true;
        }

        /// <summary>
        /// Update all files. (pass it the arguments.) (use positioning. filesToUpdateOrdered[i], versionIndexes[i], buildTs[i], digitTs[i], semiVersions[i])).
        /// </summary>
        /// <param name="vfToUpdateOrdered"><see cref="List{T}"/> of <see cref="I_VersionableFile"/> representation of the file to update. They must be ordered in the chronological way.</param>
        /// <param name="versionIndexes"><see cref="List{T}"/> of <see cref="List{T}"/> of version indexes to update.</param>
        /// <param name="buildTs"><see cref="List{T}"/> of <see cref="List{T}"/> of build type to use.</param>
        /// <param name="digitTs"><see cref="List{T}"/> of <see cref="List{T}"/> of digit to upgrade.</param>
        /// <param name="semiVersions"><see cref="List{T}"/> of <see cref="List{T}"/> of semi version to use for upgrading the version.</param>
        /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
        static public Boolean UpdateFiles(List<I_VersionableFile> vfToUpdateOrdered,
            List<List<UInt16>> versionIndexes,
            List<List<BuildType>> buildTs,
            List<List<DigitType>> digitTs,
            List<List<UInt16>> semiVersions)
        {
            if (vfToUpdateOrdered.Count != versionIndexes.Count
                    || vfToUpdateOrdered.Count != buildTs.Count
                    || vfToUpdateOrdered.Count != digitTs.Count
                    || vfToUpdateOrdered.Count != semiVersions.Count
                    )
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(UpdateFiles)}");
                String message = $"{nameof(versionIndexes)}, {nameof(buildTs)}, {nameof(digitTs)} and {nameof(semiVersions)} must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);
                return false;
            }

            Boolean success = true;
            for (int i = 0; i < vfToUpdateOrdered.Count; i++)
            {
                I_VersionableFile filesToUpdateOrderedSub = vfToUpdateOrdered[i];
                List<UInt16> versionIndexesSub = versionIndexes[i];
                List<BuildType> buildTSub = buildTs[i];
                List<DigitType> digitTSub = digitTs[i];
                List<UInt16> semiVersionsSub = semiVersions[i];

                if (UpdateFile(filesToUpdateOrderedSub, versionIndexesSub, buildTSub, digitTSub, semiVersionsSub) is false)
                {
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Update all files. (pass it the arguments.) (use positioning. filesToUpdateOrdered[i], versionIndexes[i], buildTs[i], digitTs[i], semiVersions[i])).
        /// </summary>
        /// <param name="vfToUpdateOrdered"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="I_VersionableFile"/> representation of the file to update. They must be ordered in the chronological way.</param>
        /// <param name="versionIndexes"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="List{T}"/> of version indexes to update.</param>
        /// <param name="buildTs"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="List{T}"/> of build type to use.</param>
        /// <param name="digitTs"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="List{T}"/> of digit to upgrade.</param>
        /// <param name="semiVersions"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="List{T}"/> of semi version to use for upgrading the version.</param>
        /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
        static public Boolean UpdateFiles(List<List<I_VersionableFile>> vfToUpdateOrdered,
            List<List<List<UInt16>>> versionIndexes,
            List<List<List<BuildType>>> buildTs,
            List<List<List<DigitType>>> digitTs,
            List<List<List<UInt16>>> semiVersions)
        {
            if (vfToUpdateOrdered.Count != versionIndexes.Count
                    || vfToUpdateOrdered.Count != buildTs.Count
                    || vfToUpdateOrdered.Count != digitTs.Count
                    || vfToUpdateOrdered.Count != semiVersions.Count
                    )
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(UpdateFiles)}");
                String message = $"{nameof(versionIndexes)}, {nameof(buildTs)}, {nameof(digitTs)} and {nameof(semiVersions)} must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);
                return false;
            }

            Boolean success = true;
            for (int i = 0; i < vfToUpdateOrdered.Count; i++)
            {
                List<I_VersionableFile> filesToUpdateOrderedSub = vfToUpdateOrdered[i];
                List<List<UInt16>> versionIndexesSub = versionIndexes[i];
                List<List<BuildType>> buildTSub = buildTs[i];
                List<List<DigitType>> digitTSub = digitTs[i];
                List<List<UInt16>> semiVersionsSub = semiVersions[i];

                if (UpdateFiles(filesToUpdateOrderedSub, versionIndexesSub, buildTSub, digitTSub, semiVersionsSub) is false)
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
        private static String _className = nameof(UVMUpdater);

        #endregion DEBUG
    }
}
