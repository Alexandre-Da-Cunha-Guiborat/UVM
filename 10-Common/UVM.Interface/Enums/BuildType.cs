namespace UVM.Interface.Enums
{
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
        /// Representation of a stable/release version.
        /// </summary>
        RELEASE,

        /// <summary>
        /// Representation of a custom version (i.e for custom handling of version such a RC version and stuff like that.)
        /// </summary>
        CUSTOM,

        /// <summary>
        /// SHOULD NOT BE ENCOUNTER! (Can be used to know the number of types.)
        /// </summary>
        BuildType_SIZE
    }
}
