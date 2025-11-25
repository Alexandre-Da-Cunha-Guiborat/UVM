using System;
using System.Collections.Generic;
using UVM.Interface.Enums;

namespace UVM.Interface.Interfaces
{
    /// <summary>
    /// Interface for files with versions needing to be managed.
    /// </summary>
    public interface I_VersionableFile
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// <see cref="String"/> representation of an unique Id used to identify the file.
        /// </summary>
        public String VFId { get; }

        /// <summary>
        /// <see cref="String"/> representation of the absolute path to the file directory.
        /// </summary>
        public String VFDirPath { get; }

        /// <summary>
        /// <see cref="String"/> representation of the absolute path to the file.
        /// </summary>
        public String VFPath { get; }

        /// <summary>
        /// <see cref="String"/> representation of file's name.
        /// </summary>
        public String VFName { get; }

        /// <summary>
        /// <see cref="String"/> representation of file's extension.
        /// </summary>
        public String VFExtension { get; }

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="I_Version"/> representing all versions described in the file.
        /// </summary>
        public List<I_Version> VFVersions { get; }

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="I_VersionableFile"/> representing all file's dependencies to other <see cref="I_VersionableFile"/>.
        /// </summary>
        public List<I_VersionableFile> VFDependencies { get; }

        /// <summary>
        /// Compute the dependencies of the file.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionableFile"/> that could be a dependence of this <see cref="I_VersionableFile"/>.</param>
        /// <returns>The list of all <see cref="I_VersionableFile"/> in the versionnableFilePool that this <see cref="I_VersionableFile"/> depend on.</returns>
        public List<I_VersionableFile> ComputeDependencies(List<I_VersionableFile> vfPool);

        /// <summary>
        /// Dump the file to the file system.
        /// </summary>
        /// <param name="outputPath"><see cref="String"/> representation of the absolute path to use for dumping the file.</param>
        public bool DumpFile(String outputPath);

        /// <summary>
        /// Upgrade all versions using the specified builds, digits and semiVersions. (Given positionally.)
        /// </summary>
        /// <param name="versionIndexes"><see cref="List{T}"/> of all versions that should be upgrade.</param>
        /// <param name="buildTs"><see cref="List{T}"/> of <see cref="BuildType"/> to use when upgrading the version at the same index in versionIndexes.</param>
        /// <param name="digitTs"><see cref="List{T}"/> of <see cref="DigitType"/> to upgrade the version at the same index in versionIndexes.</param>
        /// <param name="semiVersions"><see cref="List{T}"/> of semiVersions to use for the version at the same index in versionIndexes.</param>
        public bool Update(List<UInt16> versionIndexes, List<BuildType> buildTs, List<DigitType> digitTs, List<UInt16> semiVersions);



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
