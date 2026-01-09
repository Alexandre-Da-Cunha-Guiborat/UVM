using System;
using System.Reflection;
using UVM.Logging.Enums;
using UVM.Logging.Interfaces;

namespace UVM.Logging.Models
{
    /// <summary>
    ///     Representation of a <see cref="M_Log"/>, implementing the interface <see cref="I_Log"/>.
    /// </summary>
    /// <remarks>
    ///     A custom <see cref="M_Log"/> for unified logging across the whole project.
    /// </remarks>
    public sealed class M_Log : I_Log
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        ///     <see cref="String"/> representation of the log date.
        /// </summary>
        public String Date { get; set; } = String.Empty;

        /// <summary>
        ///     <see cref="String"/> representation of the Timestamp of the log.
        /// </summary>
        public String Time { get; set; } = String.Empty;

        /// <summary>
        ///     <see cref="E_LogLevel"/> of this Log.
        /// </summary>
        public E_LogLevel Level { get; set; } = E_LogLevel.TRACE;

        /// <summary>
        ///     <see cref="String"/> representation of the title of the log.
        /// </summary>
        public String Title { get; set; } = String.Empty;

        /// <summary>
        ///     <see cref="String"/> representation of the Log message.
        /// </summary>
        public String Message { get; set; } = String.Empty;

        /// <summary>
        ///     <see cref="M_Log"/> constructor.
        /// </summary>
        /// <param name="logLevel"><see cref="E_LogLevel"/> of the log.</param>
        /// <param name="title"><see cref="String"/> representation of the tile for the log.</param>
        /// <param name="message"><see cref="String"/> representation of the log message.</param>
        public M_Log(E_LogLevel logLevel, String title, String message)
        {
            Time = DateTime.Now.ToString("T");
            Date = DateTime.Now.ToString("d");
            Level = logLevel;
            Title = title;

            Message = message;
        }

        /// <summary>
        ///     Compute the <see cref="String"/> representation of the <see cref="M_Log"/>. 
        /// </summary>
        /// <returns>
        ///     The <see cref="String"/> representation of the <see cref="M_Log"/>.
        /// </returns>
        public override string ToString()
        {
            return $"{Date} | {Time} | {Level} | {Title}:\n{Message}";
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private
        // TBD
        #endregion Private

        #region DEBUG

        /// <summary>
        ///     <see cref="String"> representation of the assembly name.
        /// </summary>
        public static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty;

        /// <summary>
        ///     <see cref="String"> representation of the class name.
        /// </summary>
        public static String _className = nameof(M_Log);

        #endregion DEBUG

    }
}
