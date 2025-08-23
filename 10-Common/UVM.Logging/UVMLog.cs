using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace UVM.Logging
{
    /// <summary>
    /// UVM representation of a Log.
    /// </summary>
    public class UVMLog
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// <see cref="String"> representation of the log date.
        /// </summary>
        public String Date { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the Timestamp of the log.
        /// </summary>
        public String Time { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="LogLevel"> of this Log.
        /// </summary>
        public LogLevel Level { get; set; } = LogLevel.Trace;

        /// <summary>
        /// <see cref="String"> representation of the title of the log.
        /// </summary>
        public String Title { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the Log message.
        /// </summary>
        public String Message { get; set; } = String.Empty;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logLevel"><see cref="LogLevel"> of the log.</param>
        /// <param name="title"><see cref="String"> representation of the tile for the log.</param>
        /// <param name="message"><see cref="String"> representation of the log message.</param>
        public UVMLog(LogLevel logLevel, String title, String message)
        {
            Time = DateTime.Now.ToString("T");
            Date = DateTime.Now.ToString("d");
            Level = logLevel;
            Title = title;
            Message = message;
        }

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

        #region DEBUG

        /// <summary>
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        // private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        // private static String _className = nameof(UVMLog);

        #endregion DEBUG
    }
}
