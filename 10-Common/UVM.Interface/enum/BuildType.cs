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
        /// Reprensetation of an alpha version.
        /// </summary>
        ALPHA,

        /// <summary>
        /// Reprensetation of a beta version.
        /// </summary>
        BETA,

        /// <summary>
        /// Reprensetation of a stable/release version.
        /// </summary>
        RELEASE
    }
}
