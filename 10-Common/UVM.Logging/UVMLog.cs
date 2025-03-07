using System;
using Microsoft.Extensions.Logging;

namespace UVM.Logging
{
    /// <summary>
    /// UVM representation of a Log.
    /// </summary>
    public class UVMLog
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly.
        /// </summary>
        private const string _asmName = "UVM.Logging";

        /// <summary>
        /// String representation of the class.
        /// </summary>
        private const string _className = "UVMLog";

        #endregion DEBUG

        #region Public

        #region Constructor

        /// <summary>
        /// UVMLog constructor.
        /// </summary>
        /// <param name="logLevel">LogLevel of the log.</param>
        /// <param name="title">String representation of the tile for the log.</param>
        /// <param name="log">String representation of the log.</param>
        public UVMLog(LogLevel logLevel, string title, string log)
        {
            Time = DateTime.Now.ToString("T");
            Date = DateTime.Now.ToString("d");
            LogLevel = logLevel;
            Title = title;

            Log = log;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// String representation of the log date.
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// String representation of the Timestamp of the log.
        /// </summary>
        public string Time { get; set; } = string.Empty;

        /// <summary>
        /// Loglevel of this Log.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// String representation of the title of the log.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// String representation of the Log.
        /// </summary>
        public string Log { get; set; } = string.Empty;

        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Public

        #region Protected

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Protected

        #region Private

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Private

    }
}


