using System;
using System.Collections.Generic;
using UVM.Interface.Enums;

namespace UVM.Interface.Interfaces;

/// <summary>
/// Interface for files with a version and dependencies.
/// </summary>
public interface IVersionable
{
    #region Public

    /// <summary>
    /// <see cref="String"/> representation of an unique Id used to identify the file.
    /// </summary>
    public String Id { get; }

    /// <summary>
    /// <see cref="IVersion"/> representing the version of the file.
    /// </summary>
    public IVersion Version { get; }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> of <see cref="IVersionable"/> representing all file's dependencies.
    /// </summary>
    public IEnumerable<IVersionable> Dependencies { get; }

    /// <summary>
    /// Computes the dependencies of the file.
    /// </summary>
    /// <param name="vfPool"><see cref="IEnumerable"/> of all <see cref="IVersionable"/> that could be a dependence of this <see cref="IVersionable"/>.</param>
    public void ComputeDependencies(IEnumerable<IVersionable> vfPool);

    /// <summary>
    /// Upgrades the version of the <see cref="IVersionable"/>
    /// </summary>
    /// <param name="buildT">The <see cref="BuildType"/> to use when upgrading the version.</param>
    /// <param name="digitT">The <see cref="DigitType"/> to use when upgrading the version.</param>
    /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public Boolean Upgrade(BuildType buildT, DigitType digitT);

    /// <summary>
    /// Dumps the file to the file system.
    /// </summary>
    /// <returns><see langword="true"/> => dump succeed, <see langword="false"/> => otherwise.</returns>
    public Boolean DumpFile();

    #endregion Public
}
