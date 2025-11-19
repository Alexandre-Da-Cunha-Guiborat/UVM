using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ADCG.DevTools.Logging;
using ADCG.DevTools.Logging.Enum;
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
        /// Create a formatted title using a assembly name, class name and function/method name.
        /// </summary>
        /// <param name="asmName"><see cref="String"/> representation of the assembly name to put in the title.</param>
        /// <param name="className"><see cref="String"/> representation of the class name to put in the title.</param>
        /// <param name="callerName"><see cref="String"/> representation of the function/method name to put in the title.</param>
        /// <returns>A formatted title for logging.</returns>
        public static string CreateTitle(String asmName, String className, String callerName)
        {
            return _logger.CreateTitle(asmName, className, callerName);
        }

        /// <summary>
        /// Add a log to the log list.
        /// </summary>
        /// <param name="level"><see cref="String"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="message"><see cref="String"/> representation of the log message.</param>
        public static void AddLog(ADCGLogLevelType level, String title, String message)
        {
            _logger.AddLog(level, title, message);
        }

        /// <summary>
        /// Add a log for a list of messages.
        /// </summary>
        /// <param name="level"><see cref="LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
        /// <param name="messageList"><see cref="List{T}"/> of <see cref="String"/> to log.</param>
        public static void AddLogList(ADCGLogLevelType level, String title, String preface, List<string> messageList)
        {
            _logger.AddLogList(level, title, preface, messageList);
        }

        /// <summary>
        /// Add a log for a list of <see cref="I_VersionableFile">.
        /// </summary>
        /// <param name="level"><see cref="LogLevel"/> to apply to the log.</param>
        /// <param name="title"><see cref="String"/> representation of the log title.</param>
        /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
        /// <param name="vfs"><see cref="List{T}"/> of <see cref="I_VersionableFile"/> to log.</param>
        public static void AddLogListVF(ADCGLogLevelType level, String title, String preface, List<I_VersionableFile> vfs)
        {
            List<String> vfPaths = vfs.Select(vf => vf.VFPath).ToList();
            _logger.AddLogList(level, title, preface, vfPaths);
        }

        /// <summary>
        // Dump all logs to a given directory applying a filter.
        // </summary>
        /// <param name="outputPath"><see cref="String"/> representation of the absolute path to the directory to dump logs to.</param>
        /// <param name="filter">Filter to apply to the logs. Any logs having a loglevel lower won't appear in the logs.</param>
        public static void DumpLogs(String outputPath, ADCGLogLevelType filter)
        {
            _logger.DumpLogs(outputPath, filter);
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private

        /// <summary>
        /// Inner instance of a <see cref="ADCGLogger"/> for easy and formatted logging.
        /// </summary>
        private static ADCGLogger _logger = new ADCGLogger();

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