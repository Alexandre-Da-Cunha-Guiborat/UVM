
using System;
using System.Collections.Generic;
using UVM.Interface.Interfaces;

namespace UVM.Testing.Models
{
    /// <summary>
    /// Mocked implementation of a <see cref="I_GenerableFile"/> used for testing purposes. 
    /// </summary>
    public class MockedGenerableFile : I_GenerableFile
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Generate the output file.
        /// </summary>
        public Boolean Generate()
        {
            return true;
        }

        /// <summary>
        /// Generate the output file.
        /// </summary>
        /// <param name="outputDirPath"><see cref="String"/> representation of the absolute path to the output directory.</param>
        /// <param name="args"><see cref="List{T}"/> of <see cref="String"/> for specific arguments for generation. (Must be handled internally.)</param>
        public Boolean Generate(String outputDirPath, List<String> args)
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
