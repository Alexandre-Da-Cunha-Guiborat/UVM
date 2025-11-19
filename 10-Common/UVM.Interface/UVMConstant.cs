using System;

namespace UVM.Interface
{
    /// <summary>
    /// Static class containing constants used by UVM.
    /// </summary>
    public static class UVMConstant
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// String representation of an invalid version.
        /// </summary>
        public const String BAD_VERSION_STR = "0.0.0";

        /// <summary>
        /// String representation of the absolute path to the local folder where UVM temporary files and logs are exported.
        /// </summary>
        public const String UVM_FOLDER_PATH = $@"/UVM";

        /// <summary>
        /// String representation of the absolute path to the logs folder.
        /// </summary>
        public const String UVM_LOG_FOLDER_PATH = $@"{UVM_FOLDER_PATH}/Logs";

        /// <summary>
        /// String representation of the absolute path to the package folder.
        /// Default folder for package generation and restore.
        /// </summary>
        public const String UVM_PACKAGE_FOLDER_PATH_DEFAULT = $@"{UVM_FOLDER_PATH}/Packages";

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
