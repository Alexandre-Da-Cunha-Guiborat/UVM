namespace UVM.Interface.Enums
{
    /// <summary>
    /// Enum used to specifiy the digit of a version.
    /// </summary>
    public enum DigitType
    {
        /// <summary>
        /// SHOULD NOT BE ENCOUNTER! (Can be used for initialization and error detection.)
        /// </summary>
        DigitType_NONE,

        /// <summary>
        /// Represent the first digit of a version. (Y.X.X.X)
        /// </summary>
        MAJOR,

        /// <summary>
        /// Represent the second digit of a version. (X.Y.X.X)
        /// </summary>
        MINOR,

        /// <summary>
        /// Represent the third digit of a version. (X.X.Y.X)
        /// </summary>
        PATCH,

        /// <summary>
        /// Represent the fourth digit of a version. (X.X.X.Y)/(X.X.X-alpha.Y)/(X.X.X-beta.Y)
        /// </summary>
        SEMI_VERSION,

        /// <summary>
        /// SHOULD NOT BE ENCOUNTER! (Can be used to know the number of types.)
        /// </summary>
        DigitType_SIZE,
    }
}
