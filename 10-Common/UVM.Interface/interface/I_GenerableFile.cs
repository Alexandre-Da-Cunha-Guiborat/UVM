using System.Collections.Generic;

namespace UVM.Interface
{
    /// <summary>
    /// Interface for files that can generate output files.
    /// </summary>
    public interface I_GenerableFile : I_VersionnableFile
    {
        #region DEBUG
        // TBD
        #endregion DEBUG

        #region Public

        #region Constructor
        // TBD
        #endregion Constructor
        
        #region Properties
        // TBD
        #endregion Properties

        #region Method

        /// <summary>
        /// Generate the ouput file.
        /// </summary>
        public abstract void Generate();

        /// <summary>
        /// Generate the ouput file.
        /// </summary>
        /// <param name="outputDirPath">String representation of the absolute path to the output directory.</param>
        /// <param name="args">List of string for specific arguments for generation. (Must be handled internaly.)</param>
        public abstract void Generate(string outputDirPath, List<string> args);

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
