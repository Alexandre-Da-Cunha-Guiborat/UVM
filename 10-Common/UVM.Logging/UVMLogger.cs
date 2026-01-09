using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UVM.Interface;
using UVM.Interface.Interfaces;
using UVM.Logging.Enums;
using UVM.Logging.Interfaces;
using UVM.Logging.Models;

namespace UVM.Logging
{
    /// <summary>
    ///     Representation of a <see cref="UVMLogger"/>.
    /// </summary>
    /// <remarks>
    ///     A custom <see cref="UVMLogger"/> for unified logging across the whole project.
    /// </remarks>
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
        ///     <see cref="List{T}"/> of all registered <see cref="I_Log"/> .
        /// </summary>
        public static List<I_Log> Logs { get; set; } = new List<I_Log>();

        /// <summary>
        ///     Create a formatted title using a assembly name, class name and function/method name.
        /// </summary>
        /// <param name="asmName"><see cref="String"/> representation of the assembly name to put in the title.</param>
        /// <param name="className"><see cref="String"/> representation of the class name to put in the title.</param>
        /// <param name="callerName"><see cref="String"/> representation of the function/method name to put in the title.</param>
        /// <returns>
        ///     A formatted title for logging.
        /// </returns>
        public static String CreateTitle(String asmName, String className, String callerName)
        {
            return $"{asmName} | {className} | {callerName}";
        }

        /// <summary>
        ///     Add a log to the log list.
        /// </summary>
        /// <param name="log"><see cref="I_Log"/> to add.</param>
        public static void AddLog(I_Log log)
        {
            Logs.Add(log);

            PrintToConsole(log);
        }

        /// <summary>
        ///     Add a log to the log list.
        /// </summary>
        /// <param name="level"><see cref="E_LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="message"><see cref="String"/> representation of the log message.</param>
        public static void AddLog(E_LogLevel level, String title, String message)
        {
            M_Log newLog = new(level, title, message);
            Logs.Add(newLog);
        }

        /// <summary>
        ///     Add a log for a list of IGenerableFile.
        /// </summary>
        /// <param name="level"><see cref="E_LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
        /// <param name="messageList">List of <see cref="String"/> to log.</param>
        public static void AddLogList(E_LogLevel level, String title, String preface, List<String> messageList)
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
        /// <param name="level"><see cref="E_LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
        /// <param name="vfs"><see cref="List{T}"/> of <see cref="I_VersionableFile"/> to log.</param>
        public static void AddLogListVF(E_LogLevel level, String title, String preface, List<I_VersionableFile> vfs)
        {
            List<String> vfPaths = vfs.Select(vf => vf.VFPath).ToList();
            AddLogList(level, title, preface, vfPaths);
        }

        /// <summary>
        /// Dump all <see cref="I_Log"/> to a given directory applying a filter.
        /// </summary>
        /// <param name="outputPath"><see cref="String"/> representation of the absolute path to the directory to dump logs to.</param>
        /// <param name="levelFilter">Filter to apply to the logs. Any logs having a loglevel lower won't appear in the logs.</param>
        public static void DumpLogs(string outputPath, E_LogLevel levelFilter)
        {
            String d = DateTime.Now.ToString("d");
            String t = DateTime.Now.ToString("t");
            String logName = $"{d.Replace("/", "-")}_{t.Replace(":", "-")}.log".Replace(" ", "_");
            String logPath = Path.Combine(outputPath, logName);

            List<I_Log> logs = Logs.Where(log => log.Level >= levelFilter).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (I_Log log in logs)
            {
                String header = $"{log.Date} | {log.Time} | {log.Level} | {log.Title} :";
                stringBuilder.AppendLine($"{header}\n{log.Message}\n");
            }

            FileStream Fs = File.Create(logPath);
            String logString = stringBuilder.ToString();
            Byte[] info = new UTF8Encoding(true).GetBytes(logString);
            Fs.Write(info, 0, info.Length);
            Fs.Close();

            Logs.Clear();
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private

        /// <summary>
        ///     Prints to the console the log.
        /// </summary>
        /// <param name="log">The log to print to the console.</param>
        private static void PrintToConsole(I_Log log)
        {
            Dictionary<E_LogLevel, ConsoleColor> levelColor = [];
            levelColor.Add(E_LogLevel.TRACE, ConsoleColor.DarkGray);
            levelColor.Add(E_LogLevel.DEBUG, ConsoleColor.Gray);
            levelColor.Add(E_LogLevel.INFO, ConsoleColor.White);
            levelColor.Add(E_LogLevel.WARNING, ConsoleColor.Yellow);
            levelColor.Add(E_LogLevel.ERROR, ConsoleColor.Red);
            levelColor.Add(E_LogLevel.FATAL, ConsoleColor.DarkRed);

            if (levelColor.ContainsKey(log.Level))
            {
                Console.ForegroundColor = levelColor[log.Level];
            }

            Console.WriteLine(log.ToString());

            Console.ForegroundColor = levelColor[E_LogLevel.INFO];
        }

        /// <summary>
        ///     Prints to the console the logs.
        /// </summary>
        /// <param name="logs">The list of logs to print to the console.</param>
        private static void PrintToConsole(I_Log[] logs)
        {
            foreach (I_Log log in logs)
            {
                PrintToConsole(log);
            }
        }

        #endregion Private

        #region DEBUG

        /// <summary>
        ///     <see cref="String"> representation of the assembly name.
        /// </summary>
        public static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty;

        /// <summary>
        ///     <see cref="String"> representation of the class name.
        /// </summary>
        public static String _className = nameof(UVMLogger);

        #endregion DEBUG

    }
}
