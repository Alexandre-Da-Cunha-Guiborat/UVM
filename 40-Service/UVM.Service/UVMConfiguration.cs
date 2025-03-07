using System;
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
        /// </summary>
        /// <param name="gitDirPath">String representation of the absolute path to the git directory.</param>
        /// <param name="commitId">String representation of the commit id we want to.</param>
        /// <param name="commitRefId">String representation of the commit id we want to compare to.</param>
        /// <param name="buildMode">BuildType to use for when managing versions.</param>
        /// <param name="digitMode">DigitType to modify when managing versions.</param>
        public UVMConfiguration(string gitDirPath, string commitId, string commitRefId, BuildType buildMode, DigitType digitMode)
        {
            GitDirectory = gitDirPath;
            CommitId = commitId;
            CommitRefId = commitRefId;
            BuildMode = buildMode;
            DigitMode = digitMode;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// String representation of the absolute path to the git Directory to consider when running UVM.
        /// </summary>
        public string GitDirectory { get; private set; } = string.Empty;

        /// <summary>
        /// String representation of the commit Id we want to compare.
        /// </summary>
        public string CommitId { get; private set; } = string.Empty;

        /// <summary>
        /// String representation of the commit Id we want to compare to.
        /// </summary>
        public string CommitRefId { get; private set; } = string.Empty;

        /// <summary>
        /// BuildType used to manage the project.
        /// </summary>
        public BuildType BuildMode { get; private set; } = BuildType.NONE;

        /// <summary>
        /// DigitType used to manage the project.
        /// </summary>
        public DigitType DigitMode { get; private set; } = DigitType.NONE;

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
