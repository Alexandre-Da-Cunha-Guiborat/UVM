namespace UVM.Interface
{
    /// <summary>
    /// Static class containing constants used by UVM.
    /// </summary>
    public static class UVMConstante
    {

        /// <summary>
        /// String reprensentation of an invalid version.
        /// </summary>
        public const string BAD_VERSION_STR = "0.0.0";

        /// <summary>
        /// String representation of the absolute path to the local folder where UVM temporary files and logs are exported.
        /// </summary>
        public const string UVM_FOLDER_PATH = @"/UVM";

        /// <summary>
        /// String representation of the absolute path to the logs folder.
        /// </summary>
        public const string UVM_LOG_FOLDER_PATH = $"{UVM_FOLDER_PATH}/Log";

        /// <summary>
        /// String representation of the absolute path to the package folder.
        /// Default folder for package generation and restore.
        /// </summary>
        public const string UVM_PACKAGE_FOLDER_PATH = $"{UVM_FOLDER_PATH}/Package";

    }
}
