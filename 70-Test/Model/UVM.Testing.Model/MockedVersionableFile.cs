
using System;
using System.Collections.Generic;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;

namespace UVM.Testing.Models
{
    /// <summary>
    /// Mocked implementation of a <see cref="I_VersionableFile"/> used for testing purposes. 
    /// </summary>
    public class MockedVersionableFile : I_VersionableFile
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// <see cref="String"/> representation of an unique Id used to identify the file.
        /// </summary>
        public String VFId { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="String"/> representation of the absolute path to the file directory.
        /// </summary>
        public String VFDirPath { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="String"/> representation of the absolute path to the file.
        /// </summary>
        public String VFPath { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="String"/> representation of file's name.
        /// </summary>
        public String VFName { get; set; } = String.Empty;

        /// <summary>
        /// see cref="String"/> representation of file's extension.
        /// </summary>
        public String VFExtension { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="I_Version"/> representing all versions described in the file.
        /// </summary>
        public List<I_Version> VFVersions { get; set; } = [];

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="I_VersionableFile"/> representing all file's dependencies to other <see cref="I_VersionableFile"/>.
        /// </summary>
        public List<I_VersionableFile> VFDependencies { get; set; } = [];

        /// <summary>
        /// Compute the dependencies of the file.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionableFile"/> that could be a dependence of this <see cref="I_VersionableFile"/>.</param>
        /// <returns>The list of all <see cref="I_VersionableFile"/> in the versionnableFilePool that this <see cref="I_VersionableFile"/> depend on.</returns>
        public List<I_VersionableFile> ComputeDependencies(List<I_VersionableFile> vfPool)
        {
            return VFDependencies;
        }

        /// <summary>
        /// Dump the file to the file system.
        /// </summary>
        /// <param name="outputPath"><see cref="String"/> representation of the absolute path to use for dumping the file.</param>
        public Boolean DumpFile(String outputPath)
        {
            return true;
        }

        /// <summary>
        /// Upgrade all versions using the specified builds, digits and semiVersions. (Given positionally.)
        /// </summary>
        /// <param name="versionIndexes"><see cref="List{T}"/> of all versions that should be upgrade.</param>
        /// <param name="buildTs"><see cref="List{T}"/> of <see cref="BuildType"/> to use when upgrading the version at the same index in versionIndexes.</param>
        /// <param name="digitTs"><see cref="List{T}"/> of <see cref="DigitType"/> to upgrade the version at the same index in versionIndexes.</param>
        /// <param name="semiVersions"><see cref="List{T}"/> of semiVersions to use for the version at the same index in versionIndexes.</param>
        public Boolean Update(List<UInt16> versionIndexes, List<BuildType> buildTs, List<DigitType> digitTs, List<UInt16> semiVersions)
        {
            return true;
        }

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
