using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using UVM.Interface;

namespace UVM.Logging
{
    /// <summary>
    /// Singleton used for logging in UVM.
    /// </summary>
    public sealed class UVMLogger
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly.
        /// </summary>
        private const string _asmName = "UVM.Logging";

        /// <summary>
        /// String representation of the class.
        /// </summary>
        private const string _className = "UVMLogger";

        #endregion DEBUG

        #region Singleton 

        /// <summary>
        /// (Lazy) Private instance of the logger.
        /// </summary>
        private static readonly Lazy<UVMLogger> lazy = new Lazy<UVMLogger>(() => new UVMLogger());

        /// <summary>
        /// Singleton access properties.
        /// </summary>
        public static UVMLogger Instance { get { return lazy.Value; } }

        /// <summary>
        /// Private constructor for Singleton instantiation.
        /// </summary>
        private UVMLogger()
        {
            if (Directory.Exists(UVMConstante.UVM_LOG_FOLDER_PATH) is false)
            {
                Directory.CreateDirectory(UVMConstante.UVM_LOG_FOLDER_PATH);
            }

        }

        #endregion Singleton

        #region Public

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties

        /// <summary>
        /// List of all registered logs.
        /// </summary>
        public List<UVMLog> Logs = new List<UVMLog>();

        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function

        /// <summary>
        /// Create a formated title using a assembly name, class name and function/method name.
        /// </summary>
        /// <param name="asmName">String representation of the assembly name to put in the title.</param>
        /// <param name="className">String representation of the class name to put in the title.</param>
        /// <param name="callerName">String representation of the function/method name to put in the title.</param>
        /// <returns>A formated title for logging.</returns>
        public static string CreateTitle(string asmName, string className, string callerName)
        {
            return $"{asmName} | {className} | {callerName}";
        }

        /// <summary>
        /// Add a log to the log list.
        /// </summary>
        /// <param name="level">Loglevel to apply to the log.</param>
        /// <param name="title">String representation of the log title.</param>
        /// <param name="message">String representation of the log message.</param>
        public static void AddLog(LogLevel level, string title, string message)
        {
            UVMLog newLog = new(level, title, message);
            Instance.Logs.Add(newLog);
        }

        /// <summary>
        /// Add a log for a list of IGenerableFile.
        /// </summary>
        /// <param name="level">Loglevel to apply to the log.</param>
        /// <param name="title">String representation of the log title.</param>
        /// <param name="preface">String representation of a small message to preface the log.</param>
        /// <param name="messageList">List of string to log.</param>
        public static void AddLogList(LogLevel level, string title, string preface, List<string> messageList)
        {
            string listOut = string.Empty;
            foreach (string str in messageList)
            {
                listOut += $"- {str}\n";
            }

            string log = $"{preface} :\n{listOut}";
            AddLog(level, title, log);
        }

        /// <summary>
        /// Add a log for a list of IGenerableFile.
        /// </summary>
        /// <param name="level">Loglevel to apply to the log.</param>
        /// <param name="title">String representation of the log title.</param>
        /// <param name="preface">String representation of a small message to preface the log.</param>
        /// <param name="vfs">List of <see cref="I_VersionnableFile"/> to log.</param>
        public static void AddLogListVF(LogLevel level, string title, string preface, List<I_VersionnableFile> vfs)
        {
            List<string> vfPaths = vfs.Select(vf => vf.VFPath).ToList();
            AddLogList(level, title, preface, vfPaths);
        }

        /// <summary>
        // Dump all logs to a given directory applying a filter.
        // </summary>
        /// <param name="outputPath">String representation of the absolute path to the direcotry to dump logs to.</param>
        /// <param name="filter">Filter to apply to the logs. Any logs having a loglevel lower won't appear in the logs.</param>
        public static void DumpLogs(string outputPath, LogLevel levelFilter)
        {
            string d = DateTime.Now.ToString("d");
            string t = DateTime.Now.ToString("t");
            string logName = $"{d.Replace("/", "-")}_{t.Replace(":", "-")}.log";
            string logPath = Path.Combine(outputPath, logName);

            List<UVMLog> logs = Instance.Logs.Where(log => log.Level >= levelFilter).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (UVMLog log in logs)
            {
                string header = $"{log.Date} | {log.Time} | {log.Level} | {log.Title} :";
                stringBuilder.AppendLine($"{header}\n{log.Message}\n");
            }

            FileStream Fs = File.Create(logPath);
            string logString = stringBuilder.ToString();
            byte[] info = new UTF8Encoding(true).GetBytes(logString);
            Fs.Write(info, 0, info.Length);
            Fs.Close();

            Instance.Logs.Clear();
        }

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


