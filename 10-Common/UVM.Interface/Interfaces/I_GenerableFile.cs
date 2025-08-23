using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UVM.Interface.Interfaces
{
    /// <summary>
    /// Interface for files that can generate output files.
    /// </summary>
    public interface I_GenerableFile
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Generate the output file.
        /// </summary>
        public Boolean Generate();

        /// <summary>
        /// Generate the output file.
        /// </summary>
        /// <param name="outputDirPath"><see cref="String"/> representation of the absolute path to the output directory.</param>
        /// <param name="args"><see cref="List{T}"/> of <see cref="String"/> for specific arguments for generation. (Must be handled internally.)</param>
        public Boolean Generate(String outputDirPath, List<String> args);

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

        #region DEBUG
        // TBD
        #endregion DEBUG
    }
}
