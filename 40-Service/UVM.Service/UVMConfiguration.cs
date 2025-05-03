using System;
using System.Collections.Generic;
using UVM.Interface;

namespace UVM.Service
{
    /// <summary>
    /// Configuration class for UVM runs.
    /// </summary>
    public class UVMConfiguration
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly.
        /// </summary>
        private const string _asmName = "UVM.Service";

        /// <summary>
        /// String representation of the class.
        /// </summary>
        private const string _className = "UVMConfiguration";

        #endregion DEBUG

        #region Public

        #region Constructor

        /// <summary>
        /// UVMConfiguration constructor.
        /// Notes : 
        /// All list must be the same length and Positionnaly organized, i.e gitDirPaths[i] => commitIdsRef[i] => ...
        /// </summary>
        /// <param name="gitDirPaths">List of string representating the absolute path to all git directories to consider. (Positionnaly organized)</param>
        /// <param name="commitIdsRef">List of string representating the commits' id we want to compare to. (Positionnaly organized)</param>
        /// <param name="commitIds">List of string representating the commits' id we want to compare. (Positionnaly organized)</param>
        /// <param name="buildModes">List of <see cref="BuildType"/> to use for when managing versions. (Positionnaly organized)</param>
        /// <param name="digitModes">List of <see cref="DigitType"/> to modify when managing versions. (Positionnaly organized)</param>
        public UVMConfiguration(List<string> gitDirPaths, List<string> commitIdsRef, List<string> commitIds, List<BuildType> buildModes, List<DigitType> digitModes)
        {
            GitDirectories = gitDirPaths;
            CommitIdsRef = commitIdsRef;
            CommitIds = commitIds;
            BuildModes = buildModes;
            DigitModes = digitModes;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// String representation of the absolute path to the git Directory to consider when running UVM. (Positionnaly organized)
        /// </summary>
        public List<string> GitDirectories { get; private set; } = [];

        /// <summary>
        /// String representation of the commit Id we want to compare. (Positionnaly organized)
        /// </summary>
        public List<string> CommitIds { get; private set; } = [];

        /// <summary>
        /// String representation of the commit Id we want to compare to. (Positionnaly organized)
        /// </summary>
        public List<string> CommitIdsRef { get; private set; } = [];

        /// <summary>
        /// List of <see cref="BuildType"/> used to manage the project. (Positionnaly organized)
        /// </summary>
        public List<BuildType> BuildModes { get; private set; } = [];

        /// <summary>
        /// List of <see cref="DigitType"/> to modify when managing the project. (Positionnaly organized)
        /// </summary>
        public List<DigitType> DigitModes { get; private set; } = [];

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
