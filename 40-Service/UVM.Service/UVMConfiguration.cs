using System;
using System.Collections.Generic;
using System.Reflection;
using UVM.Interface.Enums;

namespace UVM.Service
{
    /// <summary>
    /// Configuration class for UVM runs.
    /// </summary>
    public class UVMConfiguration
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="String"/> representation of the absolute path to the git Directory to consider when running UVM. (Positionally organized)
        /// </summary>
        public List<String> GitDirectories { get; private set; } = [];

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="String"/> representation of the commit Id we want to compare. (Positionally organized)
        /// </summary>
        public List<String> CommitIds { get; private set; } = [];

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="String"/> representation of the commit Id we want to compare to. (Positionally organized)
        /// </summary>
        public List<String> CommitIdsRef { get; private set; } = [];

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="BuildType"/> used to manage the project. (Positionally organized)
        /// </summary>
        public List<BuildType> BuildModes { get; private set; } = [];

        /// <summary>
        /// <see cref="List{T}"/> of <see cref="DigitType"/> to modify when managing the project. (Positionally organized)
        /// </summary>
        public List<DigitType> DigitModes { get; private set; } = [];

        /// <summary>
        /// UVMConfiguration constructor.
        /// Notes : 
        /// All <see cref="List{T}"/> must be the same length and Positionally organized, i.e gitDirPaths[i] => commitIdsRef[i] => ...
        /// </summary>
        /// <param name="gitDirPaths"><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to all git directories to consider. (Positionally organized)</param>
        /// <param name="commitIdsRef"><see cref="List{T}"/> of <see cref="String"/> representing the commits' id we want to compare to. (Positionally organized)</param>
        /// <param name="commitIds"><see cref="List{T}"/> of <see cref="String"/> representing the commits' id we want to compare. (Positionally organized)</param>
        /// <param name="buildModes"><see cref="List{T}"/> of <see cref="BuildType"/> to use for when managing versions. (Positionally organized)</param>
        /// <param name="digitModes"><see cref="List{T}"/> of <see cref="DigitType"/> to modify when managing versions. (Positionally organized)</param>
        public UVMConfiguration(List<String> gitDirPaths, List<String> commitIdsRef, List<String> commitIds, List<BuildType> buildModes, List<DigitType> digitModes)
        {
            GitDirectories = gitDirPaths;
            CommitIdsRef = commitIdsRef;
            CommitIds = commitIds;
            BuildModes = buildModes;
            DigitModes = digitModes;
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private
        // TBD
        #endregion Private

        #region DEBUG

        /// <summary>
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        // private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        // private static String _className = nameof(UVMConfiguration);

        #endregion DEBUG
    }
}
