using System;
using UVM.Interface.Enums;

namespace UVM.Interface.Interfaces
{
    /// <summary>
    /// Interface for version.
    /// </summary>
    public interface I_Version
    {
        #region Singleton
        // TBD
        #endregion Singleton

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
        /// <see cref="UInt16"/> representing the value of the fourth digit of a version. (X.X.X.Y)/(X.X.X-alpha.Y)/(X.X.X-beta.Y)
        /// </summary>
        public UInt16 SemVer { get; }

        /// <summary>
        /// <see cref="UInt64"/> representing the value of the version. ([Major, Minor, Path, SemVer])
        /// </summary>
        public UInt64 Version { get; }

        /// <summary>
        /// <see cref="BuildType"/> of the version.
        /// </summary>
        public BuildType BuildT { get; }

        /// <summary>
        /// Method that compute and upgrade the version.
        /// </summary>
        /// <param name="buildT"><see cref="BuildType"/> representing build type to use for upgrading.</param>
        /// <param name="digitT"><see cref="DigitType"/> representing the digit to modify.</param>
        /// <param name="semiVersion"><see cref="UInt16"/> representing the semi version to use.</param>
        public void Upgrade(BuildType buildT, DigitType digitT, UInt16 semiVersion);

        /// <summary>
        /// Turn the version into its string form.
        /// </summary>
        /// <returns><see cref"String"/> form of the version.</returns>
        public String ToString();

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private
        // TBD
        #endregion Private

        #region DEBUG
        // TBD
        #endregion DEBUG
    }
}
