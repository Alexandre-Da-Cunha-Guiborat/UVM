namespace UVM.Interface
{
    /// <summary>
    /// Enum used to specify the build of a version.
    /// </summary>
    public enum BuildType
    {
        /// <summary>
        /// SHOULD NOT BE ENCOUNTER! (Can be used for initialization and error detection.)
        /// </summary>
        NONE,

        /// <summary>
        /// Reprensentation of an alpha version.
        /// </summary>
        ALPHA,

        /// <summary>
        /// Reprensentation of a beta version.
        /// </summary>
        BETA,

        /// <summary>
        /// Reprensentation of a stable/release version.
        /// </summary>
        RELEASE,

        /// <summary>
        /// Representation of a custom version (i.e for custom handling of version such a RC version and stuff like that.)
        /// </summary>
        CUSTOM
    }
}
