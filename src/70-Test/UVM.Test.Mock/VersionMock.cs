using System;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;

namespace UVM.Testing.Models;

/// <summary>
/// Mocked implementation of a <see cref="IVersion"/> used for testing purposes. 
/// </summary>
public sealed record class VersionMock : IVersion
{
    #region Public

    /// <summary>
    /// <see cref="UInt16"/> representing the value of the first digit of a version. (Y.X.X.X)
    /// </summary>
    public UInt16 Major { get; private set; }

    /// <summary>
    /// <see cref="UInt16"/> representing the value of the second digit of a version. (X.Y.X.X)
    /// </summary>
    public UInt16 Minor { get; private set; }

    /// <summary>
    /// <see cref="UInt16"/> representing the value of the third digit of a version. (X.X.Y.X)
    /// </summary>
    public UInt16 Patch { get; private set; }

    /// <summary>
    /// <see cref="BuildType"/> of the version. (alpha, beta, rc, release)
    /// </summary>
    public BuildType BuildT { get; private set; }

    /// <summary>
    /// <see cref="Byte"/> representing the value of the fourth digit of a version. (X.X.X.Y)/(X.X.X-alpha.Y)/(X.X.X-beta.Y)/(X.X.X-rc.Y)
    /// </summary>
    public Byte SemVer { get; private set; }

    /// <summary>
    /// <see cref="UInt64"/> representing the value of the version. ([Major, Minor, Path, BuildT, SemVer])
    /// </summary>
    public UInt64 Version => (UInt64)Major << 48 | (UInt64)Minor << 32 | (UInt64)Patch << 16 | (UInt64)(Byte)BuildT << 8 | SemVer;

    /// <summary>
    /// <see cref="VersionMock"/>'s constructor.
    /// </summary>
    /// <param name="major"><see cref="UInt16"/> representing the value of the first digit of a version. (Y.X.X.X)</param>
    /// <param name="minor"><see cref="UInt16"/> representing the value of the second digit of a version. (X.Y.X.X)</param>
    /// <param name="patch"><see cref="UInt16"/> representing the value of the third digit of a version. (X.X.Y.X)</param>
    /// <param name="buildT"><see cref="BuildType"/> of the version. (alpha, beta, rc, release)</param>
    /// <param name="semver"><see cref="Byte"/> representing the value of the fourth digit of a version. (X.X.X.Y)/(X.X.X-alpha.Y)/(X.X.X-beta.Y)/(X.X.X-rc.Y)</param>
    public VersionMock(UInt16 major, UInt16 minor, UInt16 patch, BuildType buildT, Byte semver)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
        BuildT = buildT;
        SemVer = semver;
    }

    /// <summary>
    /// Upgrades the version.
    /// </summary>
    /// <param name="buildT"><see cref="BuildType"/> representing build type to use for upgrading.</param>
    /// <param name="digitT"><see cref="DigitType"/> representing the digit to modify.</param>
    /// <returns><see langword="true"/> => upgrade succeed, <see langword="false"/> => otherwise.</returns>
    public Boolean Upgrade(BuildType buildT, DigitType digitT)
    {
        if (buildT.Equals(BuildType.ALPHA))
        {
            Patch += 1;
            BuildT = BuildType.ALPHA;
            SemVer += 1;
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public Int32 CompareTo(IVersion? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (Version < other.Version)
        {
            return -1;
        }
        else if (Version > other.Version)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <inheritdoc />
    public Boolean Equals(IVersion? other)
    {
        if (other is null)
        {
            return false;
        }

        return Version.Equals(other.Version);
    }

    /// <summary>
    /// Computes the <see cref="String"/> representation of the version.
    /// </summary>
    /// <returns>The <see cref"String"/> representation of the version.</returns>
    public override String ToString()
    {
        return $"{nameof(Version)}={Major}.{Minor}.{Patch}.{BuildT}.{SemVer}";
    }

    #endregion Public
}