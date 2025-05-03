using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using UVM.Interface;
using UVM.Logging;

namespace UVM.Engine
{
    public class UVMUpdater
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly name.
        /// </summary>
        private const string _asmName = "UVM.Engine";

        /// <summary>
        /// String representation of the class name.
        /// </summary>
        private const string _className = "UVMUpdater";

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
        /// Update the given file. (pass it the arguments.)
        /// </summary>
        /// <param name="vfToUpdate"><see cref="I_VersionnableFile"/> representation of the file to update.</param>
        /// <param name="vIdxs">List of version indexes to update.</param>
        /// <param name="buildTs">List of build type to use.</param>
        /// <param name="digitTs">List of digit to upgrade.</param>
        /// <param name="semvers">List of semi version to use for upgrading the version.</param>
        /// <returns>true => upgrade successed, false => otherwise.</returns>
        static public bool UpdateFile(I_VersionnableFile vfToUpdate,
             List<UInt16> vIdxs,
             List<BuildType> buildTs,
             List<DigitType> digitTs,
             List<UInt16> semvers)
        {
            if (vIdxs.Count != buildTs.Count || vIdxs.Count != digitTs.Count || vIdxs.Count != semvers.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"UpdateFile");
                string message = $"vIdxs, buildTs, digitTs and semivers must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            vfToUpdate.Update(vIdxs, buildTs, digitTs, semvers);
            return true;
        }

        /// <summary>
        /// Update all files. (pass it the arguments.) (use positioning. filesToUpdateOrdered[i], vIdxs[i], buildTs[i], digitTs[i], semivers[i])).
        /// </summary>
        /// <param name="vfToUpdateOrdered">List of <see cref="I_VersionnableFile"/> representation of the file to update. They must be ordered in the chronological way.</param>
        /// <param name="vIdxs">List of List of version indexes to update.</param>
        /// <param name="buildTs">List of List of build type to use.</param>
        /// <param name="digitTs">List of List of digit to upgrade.</param>
        /// <param name="semvers">List of List of semi version to use for upgrading the version.</param>
        /// <returns>true => all upgrade successed, false => otherwise.</returns>
        static public bool UpdateFiles(List<I_VersionnableFile> vfToUpdateOrdered,
            List<List<UInt16>> vIdxs,
            List<List<BuildType>> buildTs,
            List<List<DigitType>> digitTs,
            List<List<UInt16>> semvers)
        {
            if (vfToUpdateOrdered.Count != vIdxs.Count
                    || vfToUpdateOrdered.Count != buildTs.Count
                    || vfToUpdateOrdered.Count != digitTs.Count
                    || vfToUpdateOrdered.Count != semvers.Count
                    )
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"UpdateFiles");
                string message = $"filesToUpdateOrdered, vIdxs, buildTs, digitTs and semivers must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < vfToUpdateOrdered.Count; i++)
            {
                I_VersionnableFile filesToUpdateOrderedSub = vfToUpdateOrdered[i];
                List<UInt16> vIdxSub = vIdxs[i];
                List<BuildType> buildTSub = buildTs[i];
                List<DigitType> digitTSub = digitTs[i];
                List<UInt16> semiverSub = semvers[i];

                UpdateFile(filesToUpdateOrderedSub, vIdxSub, buildTSub, digitTSub, semiverSub);
            }

            return true;
        }

        /// <summary>
        /// Update all files. (pass it the arguments.) (use positioning. filesToUpdateOrdered[i], vIdxs[i], buildTs[i], digitTs[i], semivers[i])).
        /// </summary>
        /// <param name="vfToUpdateOrdered">List of List of <see cref="I_VersionnableFile"/> representation of the file to update. They must be ordered in the chronological way.</param>
        /// <param name="vIdxs">List of List of List of version indexes to update.</param>
        /// <param name="buildTs">List of List of List of build type to use.</param>
        /// <param name="digitTs">List of List of List of digit to upgrade.</param>
        /// <param name="semvers">List of List of List of semi version to use for upgrading the version.</param>
        /// <returns>true => all upgrade successed, false => otherwise.</returns>
        static public bool UpdateFiles(List<List<I_VersionnableFile>> vfToUpdateOrdered,
            List<List<List<UInt16>>> vIdxs,
            List<List<List<BuildType>>> buildTs,
            List<List<List<DigitType>>> digitTs,
            List<List<List<UInt16>>> semvers)
        {
            if (vfToUpdateOrdered.Count != vIdxs.Count
                    || vfToUpdateOrdered.Count != buildTs.Count
                    || vfToUpdateOrdered.Count != digitTs.Count
                    || vfToUpdateOrdered.Count != semvers.Count
                    )
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"UpdateFile");
                string message = $"filesToUpdateOrdered, vIdxs, buildTs, digitTs and semivers must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < vfToUpdateOrdered.Count; i++)
            {
                List<I_VersionnableFile> filesToUpdateOrderedSub = vfToUpdateOrdered[i];
                List<List<UInt16>> vIdxSub = vIdxs[i];
                List<List<BuildType>> buildTSub = buildTs[i];
                List<List<DigitType>> digitTSub = digitTs[i];
                List<List<UInt16>> semiverSub = semvers[i];

                UpdateFiles(filesToUpdateOrderedSub, vIdxSub, buildTSub, digitTSub, semiverSub);
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


