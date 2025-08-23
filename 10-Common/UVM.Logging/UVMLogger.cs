using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using UVM.Interface;
using UVM.Interface.Interfaces;

namespace UVM.Logging
{
    /// <summary>
    /// Singleton used for logging in UVM.
    /// </summary>
    public sealed class UVMLogger
    {
        #region Singleton

        /// <summary>
        /// (Lazy) Private instance of the logger.
        /// </summary>
        private static readonly Lazy<UVMLogger> _lazyInstance = new Lazy<UVMLogger>(() => new UVMLogger());

        /// <summary>
        /// Singleton access properties.
        /// </summary>
        public static UVMLogger Instance { get { return _lazyInstance.Value; } }

        /// <summary>
        /// Private constructor for Singleton instantiation.
        /// </summary>
        private UVMLogger()
        {
            if (Directory.Exists(UVMConstant.UVM_LOG_FOLDER_PATH) is false)
            {
                Directory.CreateDirectory(UVMConstant.UVM_LOG_FOLDER_PATH);
            }

        }

        #endregion Singleton

        #region Public

        /// <summary>
        /// <see cref="List{T}"/> of all registered <see cref="UVMLog"/>.
        /// </summary>
        public List<UVMLog> Logs = [];

        /// <summary>
        /// Create a formatted title using a assembly name, class name and function/method name.
        /// </summary>
        /// <param name="asmName"><see cref="String"/> representation of the assembly name to put in the title.</param>
        /// <param name="className"><see cref="String"/> representation of the class name to put in the title.</param>
        /// <param name="callerName"><see cref="String"/> representation of the function/method name to put in the title.</param>
        /// <returns>A formatted title for logging.</returns>
        public static string CreateTitle(String asmName, String className, String callerName)
        {
            return $"{asmName} | {className} | {callerName}";
        }

        /// <summary>
        /// Add a log to the log list.
        /// </summary>
        /// <param name="level"><see cref="String"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="message"><see cref="String"/> representation of the log message.</param>
        public static void AddLog(LogLevel level, String title, String message)
        {
            UVMLog newLog = new(level, title, message);
            Instance.Logs.Add(newLog);
        }

        /// <summary>
        /// Add a log for a list of messages.
        /// </summary>
        /// <param name="level"><see cref="LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
        /// <param name="messageList"><see cref="List{T}"/> of <see cref="String"/> to log.</param>
        public static void AddLogList(LogLevel level, String title, String preface, List<string> messageList)
        {
            String listOut = String.Empty;
            foreach (String str in messageList)
            {
                listOut += $"- {str}\n";
            }

            String log = $"{preface} :\n{listOut}";
            AddLog(level, title, log);
        }

        /// <summary>
        /// Add a log for a list of <see cref="I_VersionableFile">.
        /// </summary>
        /// <param name="level"><see cref="LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
        /// <param name="vfs"><see cref="List{T}"/> of <see cref="I_VersionableFile"/> to log.</param>
        public static void AddLogListVF(LogLevel level, String title, String preface, List<I_VersionableFile> vfs)
        {
            List<String> vfPaths = vfs.Select(vf => vf.VFPath).ToList();
            AddLogList(level, title, preface, vfPaths);
        }

        /// <summary>
        // Dump all logs to a given directory applying a filter.
        // </summary>
        /// <param name="outputPath"><see cref="String"/> representation of the absolute path to the directory to dump logs to.</param>
        /// <param name="filter">Filter to apply to the logs. Any logs having a loglevel lower won't appear in the logs.</param>
        public static void DumpLogs(String outputPath, LogLevel filter)
        {
            String d = DateTime.Now.ToString("d");
            String t = DateTime.Now.ToString("t");
            String logName = $"{d.Replace("/", "-")}_{t.Replace(":", "-")}.log";
            String logPath = Path.Combine(outputPath, logName);

            List<UVMLog> logs = Instance.Logs.Where(log => log.Level >= filter).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (UVMLog log in logs)
            {
                String header = $"{log.Date} | {log.Time} | {log.Level} | {log.Title} :";
                stringBuilder.AppendLine($"{header}\n{log.Message}\n");
            }

            FileStream fileStream = File.Create(logPath);
            String logString = stringBuilder.ToString();
            Byte[] info = new UTF8Encoding(true).GetBytes(logString);
            fileStream.Write(info, 0, info.Length);
            fileStream.Close();

            Instance.Logs.Clear();
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
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        // private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        // private static String _className = nameof(UVMLogger);

        #endregion DEBUG
    }
}
