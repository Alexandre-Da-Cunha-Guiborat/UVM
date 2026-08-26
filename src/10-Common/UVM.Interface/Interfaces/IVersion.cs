using System;
using UVM.Interface.Enums;

namespace UVM.Interface.Interfaces;

/// <summary>
/// Interface for versions.
/// </summary>
public interface IVersion : IComparable<IVersion>, IEquatable<IVersion>
{
    #region Public

    /// <summary>
    /// <see cref="UInt16"/> representing the value of the first digit of a version. (Y.X.X.X)
    /// </summary>
    public UInt16 Major { get; }

    /// <summary>
    /// <see cref="UInt16"/> representing the value of the second digit of a version. (X.Y.X.X)
    /// </summary>
    public UInt16 Minor { get; }

    /// <summary>
    /// <see cref="UInt16"/> representing the value of the third digit of a version. (X.X.Y.X)
    /// </summary>
    public UInt16 Patch { get; }

    /// <summary>
    /// <see cref="BuildType"/> of the version. (alpha, beta, rc, release)
    /// </summary>
    public BuildType BuildT { get; }

    /// <summary>
    /// <see cref="Byte"/> representing the value of the fourth digit of a version. (X.X.X.Y)/(X.X.X-alpha.Y)/(X.X.X-beta.Y)/(X.X.X-rc.Y)
    /// </summary>
    public Byte SemVer { get; }

    /// <summary>
    /// <see cref="UInt64"/> representing the value of the version.
    /// </summary>
    public UInt64 Version { get; }

    /// <summary>
    /// Upgrade the version.
    /// </summary>
    /// <param name="buildT"><see cref="BuildType"/> representing build type to use for upgrading.</param>
    /// <param name="digitT"><see cref="DigitType"/> representing the digit to modify.</param>
    /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public Boolean Upgrade(BuildType buildT, DigitType digitT);

    #endregion Public
}