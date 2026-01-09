using System;
using System.Reflection;
using UVM.Logging.Enums;

namespace UVM.Logging.Interfaces
{
    /// <summary>
    ///     Interface for logs.
    /// </summary>
    public interface I_Log
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        ///     <see cref="String"> representation of the log date.
        /// </summary>
        public String Date { get; set; }

        /// <summary>
        ///     <see cref="String"> representation of the Timestamp of the log.
        /// </summary>
        public String Time { get; set; }

        /// <summary>
        ///     <see cref="E_LogLevel"> of this Log.
        /// </summary>
        public E_LogLevel Level { get; set; }

        /// <summary>
        /// <see cref="String"> representation of the title of the log.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// <see cref="String"> representation of the Log message.
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        ///     Compute the <see cref="String"/> representation of the <see cref="I_Log"/>. 
        /// </summary>
        /// <returns>
        ///     The <see cref="String"/> representation of the <see cref="I_Log"/>.
        /// </returns>
        public String ToString();

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
        public static String _className = nameof(I_Log);

        #endregion DEBUG

    }
}
