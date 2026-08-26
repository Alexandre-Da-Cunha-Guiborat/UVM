namespace UVM.Interface.Enums;

/// <summary>
/// Enum used to specify the build of a version.
/// </summary>
public enum BuildType
{
    /// <summary>
    /// SHOULD NOT BE ENCOUNTER! (Can be used for initialization and error detection.)
    /// </summary>
    BuildType_NONE,

    /// <summary>
    /// Representation of an alpha version.
    /// </summary>
    ALPHA,

    /// <summary>
    /// Representation of a beta version.
    /// </summary>
    BETA,

    /// <summary>
    /// Representation of a release candidate version.
    /// </summary>
    RC,

    /// <summary>
    /// Representation of a stable/release version.
    /// </summary>
    RELEASE,
}