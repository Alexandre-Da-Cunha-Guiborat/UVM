using System;
using System.Collections.Generic;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;

namespace UVM.Testing.Models;

/// <summary>
/// Mocked implementation of a <see cref="IVersionable"/> used for testing purposes. 
/// </summary>
public sealed record class VersionableMock : IVersionable
{
    #region Public

    /// <summary>
    /// <see cref="String"/> representation of an unique Id used to identify the file.
    /// </summary>
    public String Id { get; set; } = String.Empty;

    /// <summary>
    /// <see cref="IVersion"/> representing the version of the file.
    /// </summary>
    public IVersion Version { get; set; }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> of <see cref="IVersionable"/> representing all file's dependencies to other <see cref="IVersionable"/>.
    /// </summary>
    public IEnumerable<IVersionable> Dependencies { get; set; } = [];

    /// <summary>
    /// <see cref="VersionableMock"/>'s constructor.
    /// </summary>
    /// <param name="id"><see cref="String"/> representation of an unique Id used to identify the file.</param>
    /// <param name="version"><see cref="IVersion"/> representing the version of the file.</param>
    /// <param name="dependencies"><see cref="IEnumerable{T}"/> of <see cref="IVersionable"/> representing all file's dependencies to other <see cref="IVersionable"/>.</param>
    public VersionableMock(String id, IVersion version, IEnumerable<IVersionable> dependencies)
    {
        Id = id;
        Version = version;
        Dependencies = dependencies;
    }

    /// <summary>
    /// Compute the dependencies of the file.
    /// </summary>
    /// <param name="vfPool">List of all <see cref="IVersionable"/> that could be a dependence of this <see cref="IVersionable"/>.</param>
    public void ComputeDependencies(IEnumerable<IVersionable> vfPool)
    {

    }

    /// <summary>
    /// Update the version of the <see cref="IVersionable"/>
    /// </summary>
    /// <param name="buildT">The <see cref="BuildType"/> to use when upgrading the version.</param>
    /// <param name="digitT">The <see cref="DigitType"/> to use when upgrading the version.</param>
    /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public Boolean Upgrade(BuildType buildT, DigitType digitT)
    {
        return Version.Upgrade(buildT, digitT);
    }

    /// <summary>
    /// Dump the file to the file system.
    /// </summary>
    /// <param name="outputPath"><see cref="String"/> representation of the absolute path to the output.</param>
    /// <returns><see langword="true"/> => Id equals "dump", <see langword="false"/> => otherwise.</returns>
    public Boolean DumpFile()
    {
        if (Id.Equals("dump"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the <see cref="String"/> representation of the version.
    /// </summary>
    /// <returns>The <see cref"String"/> representation of the version.</returns>
    public override String ToString()
    {
        return $"{nameof(Id)}={Id}, {nameof(Version)}={Version}, {nameof(Dependencies)}=[{String.Join(",", Dependencies)}]";
    }

    #endregion Public
}
