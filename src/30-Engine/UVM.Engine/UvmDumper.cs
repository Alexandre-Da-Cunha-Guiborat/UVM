using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using UVM.Interface.Interfaces;
using UVM.Logging;

namespace UVM.Engine;

/// <summary>
/// Library for <see cref="IVersionable"> dumping.
/// </summary>
public static class UvmDumper
{
    #region Public

    /// <summary>
    /// Dumps the <see cref="IVersionable"> to the file system.
    /// </summary>
    /// <param name="vfToDump"><see cref="IVersionable"> to dump to the filesystem.</param>
    /// <returns><see langword="true"/> => dump succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean DumpFile(IVersionable vfToDump)
    {
        return vfToDump.DumpFile();
    }

    /// <summary>
    /// Dumps a <see cref="IEnumerable{T}"> of <see cref="IVersionable"> to the file system.
    /// </summary>
    /// <param name="vfsToDump"><see cref="IEnumerable{T}"> of <see cref="IVersionable"> to dump to the filesystem.</param>
    /// <returns><see langword="true"/> => all dump succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean DumpFiles(IEnumerable<IVersionable> vfsToDump)
    {
        foreach (IVersionable vfToDump in vfsToDump)
        {
            if (!DumpFile(vfToDump))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Dumps a <see cref="IEnumerable{T}"> of <see cref="IEnumerable{T}"> of <see cref="IVersionable"> to the file system at the given paths.
    /// </summary>
    /// <param name="vfsToDump"><see cref="IEnumerable{T}"> of <see cref="IEnumerable{T}"> of <see cref="IVersionable"> to dump to the filesystem.</param>
    /// <returns><see langword="true"/> => all dump succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean DumpFiles(IEnumerable<IEnumerable<IVersionable>> vfsToDump)
    {
        foreach (IEnumerable<IVersionable> vfsToDumpSub in vfsToDump)
        {
            if (!DumpFiles(vfsToDumpSub))
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
