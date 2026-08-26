using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using UVM.Interface;

namespace UVM.Logging;

/// <summary>
/// A custom logger for unified logging across the whole project.
/// </summary>
public sealed class UvmLogger : ILogger
{
    #region Singleton

    /// <summary>
    /// (Lazy) Private instance of the logger.
    /// </summary>
    private static readonly Lazy<UvmLogger> _lazyInstance = new Lazy<UvmLogger>(() => new UvmLogger());

    /// <summary>
    /// Singleton access properties.
    /// </summary>
    public static UvmLogger Instance => _lazyInstance.Value;

    /// <summary>
    /// Private constructor for Singleton instantiation.
    /// </summary>
    private UvmLogger()
    {
        if (!Directory.Exists(UvmConstant.UVM_LOG_FOLDER_PATH))
        {
            Directory.CreateDirectory(UvmConstant.UVM_LOG_FOLDER_PATH);
        }
    }

    #endregion Singleton

    #region Public

    /// <summary>
    /// Filters log lower than the filter.
    /// </summary>
    public LogLevel Filter { get; set; } = LogLevel.Information;

    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, String> formatter)
    {
        if (formatter is not null && IsEnabled(logLevel))
        {
            (String Assembly, String Type, String Method) callerInfo = FindCaller();

            String title = $"{callerInfo.Assembly} | {callerInfo.Type} | {callerInfo.Method}";
            String message = formatter(state, exception);
            UvmLogEntry log = new UvmLogEntry(logLevel, title, message);

            LogToConsole(log);
        }
    }

    /// <inheritdoc />
    public Boolean IsEnabled(LogLevel logLevel)
    {
        return logLevel >= Filter;
    }

    /// <inheritdoc />
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException($"The {nameof(BeginScope)} method is not implemented yet.");
    }

    #endregion Public

    #region Private

    /// <summary>
    /// <see cref="Lock"/> used for async logging handling.
    /// </summary>
    private static readonly Lock logLock = new Lock();

    /// <summary>
    /// Prints to the console the log.
    /// </summary>
    /// <param name="log">The log to print to the console.</param>
    private static void LogToConsole(UvmLogEntry log)
    {
        ConsoleColor color = log.Level switch
        {
            LogLevel.None => ConsoleColor.White,
            LogLevel.Trace => ConsoleColor.DarkGray,
            LogLevel.Debug => ConsoleColor.Gray,
            LogLevel.Information => ConsoleColor.White,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.DarkRed,
            _ => ConsoleColor.White,
        };

        ConsoleColor previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;

        Console.WriteLine(log.ToString());

        Console.ForegroundColor = previousColor;
    }

    /// <summary>
    /// Appends to the file the given log.
    /// </summary>
    /// <param name="logFilePath"><see cref="String"/> representation of the absolute path to the log file to append the log to.</param>
    /// <param name="log">The log to print to the console.</param>
    private static void LogToFile(String logFilePath, UvmLogEntry log)
    {
        if (!File.Exists(logFilePath))
        {
            return;
        }

        lock (logLock)
        {
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLineAsync(log.ToString());
            }
        }
    }

    /// <summary>
    /// Finds the caller's assembly, type and method
    /// </summary>
    /// <returns>Caller's assembly, type and method name.</returns>
    private static (String Assembly, String Type, String Method) FindCaller()
    {
        StackTrace trace = new StackTrace();

        for (Int32 i = 0; i < trace.FrameCount; i++)
        {
            MethodBase? method = trace.GetFrame(i)?.GetMethod();

            if (method?.DeclaringType == null)
            {
                continue;
            }

            Type type = method.DeclaringType;

            if (typeof(ILogger).IsAssignableFrom(type) || type == typeof(UvmLogger) || type == typeof(LoggerExtensions))
            {
                continue;
            }

            return (type.Assembly.GetName().Name ?? "UnknownAssembly", type.FullName ?? type.Name, method.Name);
        }

        return ("UnknownAssembly", "UnknownType", "UnknownMethod");
    }

    #endregion Private
}
