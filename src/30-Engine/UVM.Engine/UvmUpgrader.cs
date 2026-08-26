using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Logging;

namespace UVM.Engine;

/// <summary>
/// Library for <see cref="IVersionable" /> upgrading.
/// </summary>
public static class UvmUpgrader
{
    #region Public

    /// <summary>
    /// Upgrades the given <see cref="IVersionable"/>.
    /// </summary>
    /// <param name="vfToUpdate"><see cref="IVersionable"/> to upgrade.</param>
    /// <param name="buildT"><see cref="BuildType"/> to use.</param>
    /// <param name="digitT"><see cref="DigitType"/> to upgrade.</param>
    /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean UpgradeFile(IVersionable vfToUpdate, BuildType buildT, DigitType digitT)
    {
        return vfToUpdate.Upgrade(buildT, digitT);
    }

    /// <summary>
    /// Upgrades all <see cref="IVersionable"/>.
    /// </summary>
    /// <param name="vfToUpdateOrdered"><see cref="IEnumerable{T}"/> of <see cref="IVersionable"/> representation of the file to upgrade.</param>
    /// <param name="buildTs"><see cref="IEnumerable{T}"/> of build type to use.</param>
    /// <param name="digitTs"><see cref="IEnumerable{T}"/> of digit to upgrade.</param>
    /// <returns><see langword="true"/> => all upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean UpgradeFiles(IEnumerable<IVersionable> vfToUpdateOrdered, IEnumerable<BuildType> buildTs, IEnumerable<DigitType> digitTs)
    {
        IList<IVersionable> vfToUpdateOrderedList = vfToUpdateOrdered.ToList();
        IList<BuildType> buildTsList = buildTs.ToList();
        IList<DigitType> digitTsList = digitTs.ToList();

        if (vfToUpdateOrderedList.Count != buildTsList.Count || vfToUpdateOrderedList.Count != digitTsList.Count)
        {
            _logger.Log(LogLevel.Error, $"{nameof(vfToUpdateOrdered)}, {nameof(buildTs)} and {nameof(digitTs)} must be the same size.");
            return false;
        }

        for (Int32 i = 0; i < vfToUpdateOrderedList.Count; i++)
        {
            IVersionable filesToUpdate = vfToUpdateOrderedList[i];
            BuildType buildT = buildTsList[i];
            DigitType digitT = digitTsList[i];

            if (!UpgradeFile(filesToUpdate, buildT, digitT))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Upgrades all <see cref="IVersionable"/>.
    /// </summary>
    /// <param name="vfToUpdateOrdered"><see cref="IEnumerable{T}"/> of<see cref="IEnumerable{T}"/> of <see cref="IVersionable"/> representation of the files to upgrade.</param>
    /// <param name="buildTs"><see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> of build type to use.</param>
    /// <param name="digitTs"><see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> of digit to upgrade.</param>
    /// <returns><see langword="true"/> => all upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean UpgradeFiles(IEnumerable<IEnumerable<IVersionable>> vfToUpdateOrdered, IEnumerable<IEnumerable<BuildType>> buildTs, IEnumerable<IEnumerable<DigitType>> digitTs)
    {
        IList<IEnumerable<IVersionable>> vfToUpdateOrderedList = vfToUpdateOrdered.ToList();
        IList<IEnumerable<BuildType>> buildTsList = buildTs.ToList();
        IList<IEnumerable<DigitType>> digitTsList = digitTs.ToList();

        if (vfToUpdateOrderedList.Count != buildTsList.Count || vfToUpdateOrderedList.Count != digitTsList.Count)
        {
            _logger.Log(LogLevel.Error, $"{nameof(vfToUpdateOrdered)}, {nameof(buildTs)} and {nameof(digitTs)} must be the same size.");
            return false;
        }

        for (Int32 i = 0; i < vfToUpdateOrderedList.Count; i++)
        {
            IEnumerable<IVersionable> filesToUpdate = vfToUpdateOrderedList[i];
            IEnumerable<BuildType> buildT = buildTsList[i];
            IEnumerable<DigitType> digitT = digitTsList[i];

            if (!UpgradeFiles(filesToUpdate, buildT, digitT))
            {
                return false;
            }
        }

        return true;
    }

    #endregion Public

    #region Private

    /// <summary>
    /// <see cref="ILogger"/> to use within that class.
    /// </summary>
    private static ILogger _logger = UvmLogger.Instance;

    #endregion Private
}
