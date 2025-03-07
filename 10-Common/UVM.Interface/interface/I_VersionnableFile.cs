using System;
using System.Collections.Generic;

namespace UVM.Interface
{
    /// <summary>
    /// Interface for files with versions needing to be managed.
    /// </summary>
    public interface I_VersionnableFile
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
        /// String reprensentation of an unique Id used to identify the file.
        /// </summary>
        public string VFId { get; }

        /// <summary>
        /// String representation of the absolute path to the file.
        /// </summary>
        public string VFPath { get; }

        /// <summary>
        /// String representation of file's name.
        /// </summary>
        public string VFName { get; }

        /// <summary>
        /// String representation of file's extension.
        /// </summary>
        public string VFExtension { get; }

        /// <summary>
        /// List of <see cref="I_Version"/> reprensenting all versions described in the file.
        /// </summary>
        public List<I_Version> VFVersions { get; }

        /// <summary>
        /// List of <see cref="I_VersionnableFile"/> reprensenting all file's dependencies to other <see cref="I_VersionnableFile"/>.
        /// </summary>
        public List<I_VersionnableFile> VFDependencies { get; }

        #endregion Properties

        #region Method

        /// <summary>
        /// Compute the dependencies of the file.
        /// </summary>
        /// <param name="versionnableFilePool">List of all <see cref="I_VersionnableFile"/> that could be a dependence of this <see cref="I_VersionnableFile"/>.</param>
        /// <returns>The list of all <see cref="I_VersionnableFile"/> in the versionnableFilePool that this <see cref="I_VersionnableFile"/> depend on.</returns>
        public abstract List<I_VersionnableFile> ComputeDependencies(List<I_VersionnableFile> versionnableFilePool);

        /// <summary>
        /// Upgrade all versions using the specified builds, digits and semiver. (Given positionnaly.)
        /// </summary>
        /// <param name="vIdxs">List of all versions that should be upgrade.</param>
        /// <param name="buildTs">List of <see cref="BuildType"/> to use when upgrading the version at the same index in vIdxs.</param>
        /// <param name="digitTs">List of <see cref="DigitType"/> to upgrade the version at the same index in vIdxs.</param>
        /// <param name="semvers">List of semiver to use for the version at the same index in vIdxs.</param>
        public abstract bool Update(List<UInt16> vIdxs, List<BuildType> buildTs, List<DigitType> digitTs, List<UInt16> semvers);

        /// <summary>
        /// Dump the file to the file system.
        /// </summary>
        /// <param name="outputFilePath">String representation of the absolute path to use for dumping the file.</param>
        public abstract bool Dump(string outputFilePath);

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
