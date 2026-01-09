namespace UVM.Logging.Enums
{
    /// <summary>
    ///     A custom <see cref="E_LogLevel"/> enum for unified logging across the whole project.
    /// </summary>
    public enum E_LogLevel
    {
        /// <summary>
        ///     SHOULD NOT BE ENCOUNTER. ONLY USE FOR INITIALIZATION OR DEFAULT/ERROR VALUE.
        /// </summary>
        E_LogLevel_NONE,

        /// <summary>
        ///     Lower log level possible. To be used for very thin logging.
        /// </summary>
        TRACE,

        /// <summary>
        ///     Dev only log level. To be used for temporary logging in a debug environment.
        /// </summary>
        DEBUG,

        /// <summary>
        ///     Lower user log level. To be used for user information over program state.
        /// </summary>
        INFO,

        /// <summary>
        ///     Warning log level. To be used for user information over some minor issue encounter during runtime.
        /// </summary>
        WARNING,

        /// <summary>
        ///     Error log level. To be used for user information over some major issue encounter during runtime.
        /// </summary>
        ERROR,

        /// <summary>
        ///     Fatal log level. To be used for user information over some major issue encounter during runtime that had the program or part of it crashing.
        /// </summary>
        FATAL,

        /// <summary>
        ///     SHOULD NOT BE ENCOUNTER. ONLY TO INDICATE HOW MANY LEVELS EXISTS. (USE CASE : FOR LOOPS OVER ALL LEVELS)
        /// </summary>
        E_LogLevel_SIZE,
    }
}
