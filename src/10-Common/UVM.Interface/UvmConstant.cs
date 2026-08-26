using System;

namespace UVM.Interface;

/// <summary>
/// Static class containing constants used by UVM.
/// </summary>
public static class UvmConstant
{
    #region Public

    /// <summary>
    /// <see cref="String"/> representation of the absolute path to the local folder where UVM temporary files and logs are exported.
    /// </summary>
    public const String UVM_FOLDER_PATH = $@"/UVM";

    /// <summary>
    /// <see cref="String"/> representation of the absolute path to the logs folder.
    /// </summary>
    public const String UVM_LOG_FOLDER_PATH = $@"{UVM_FOLDER_PATH}/Logs";

    /// <summary>
    /// <see cref="String"/> representation of the absolute path to the package folder. (Default folder for package generation and restore.)
    /// </summary>
    public const String UVM_PACKAGE_FOLDER_PATH_DEFAULT = $@"{UVM_FOLDER_PATH}/Packages";

    #endregion Public
}
