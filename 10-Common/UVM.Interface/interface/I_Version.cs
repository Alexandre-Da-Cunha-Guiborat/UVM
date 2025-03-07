using System;

namespace UVM.Interface
{
    /// <summary>
    /// Interface for version.
    /// </summary>
    public interface I_Version
    {
        #region DEBUG
        // TBD
        #endregion DEBUG

        #region Public

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties

        /// <summary>
        /// <see cref="UInt16"> representing the value of the first digit of a version. (Y.X.X.X)
        /// </summary>
        public UInt16 Major { get; }

        /// <summary>
        /// <see cref="UInt16"> representing the value of the second digit of a version. (X.Y.X.X)
        /// </summary>
        public UInt16 Minor { get; }

        /// <summary>
        /// <see cref="UInt16"> representing the value of the third digit of a version. (X.X.Y.X)
        /// </summary>
        public UInt16 Patch { get; }

        /// <summary>
        /// <see cref="UInt16"> representing the value of the fourth digit of a version. (X.X.X.Y)/(X.X.X-alpha.Y)/(X.X.X-beta.Y)
        /// </summary>
        public UInt16 SemVer { get; }

        /// <summary>
        /// <see cref="UInt64"> representing the value of the version. ([Major, Minor, Path, SemVer])
        /// </summary>
        public UInt64 Version { get; }

        /// <summary>
        /// BuildType of the version.
        /// </summary>
        public BuildType BuildT { get; }

        #endregion Properties

        #region Method

        /// <summary>
        /// Method that compute and upgrade the version.
        /// </summary>
        /// <param name="buildT"><see cref="BuildType"/> representing build type to use for upgrading.</param>
        /// <param name="digitT"><see cref="DigitType"/> representing the digit to modify.</param>
        /// <param name="semiver"><see cref="UInt16"/> representing the semiver to use.</param>
        public abstract void Upgrade(BuildType buildT, DigitType digitT, UInt16 semiver);

        /// <summary>
        /// Turn the version into its string form.
        /// </summary>
        /// <returns>String form of the version.</returns>
        public abstract string ToString();

        #endregion Method

        #region Function
        // TBD
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
