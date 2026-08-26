using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UVM.Logging;

/// <summary>
/// Extension class for the <see cref="ILogger"/>.
/// </summary>
public static class LoggerExtension
{
    #region Public

    /// <summary>
    /// Add a log for a <see cref="IEnumerable{T}"/> of <see cref="T"/> using the ToString.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="logLevel"><see cref="LogLevel"/> to apply to the log.</param>
    /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
    /// <param name="items"><see cref="T[]"/> to log.</param>
    public static void Log<T>(this ILogger logger, LogLevel logLevel, String preface, T[] items)
    {
        String message = $"{preface} :{_separator}{String.Join(_separator, items)}";
        logger.Log(logLevel, message);
    }

    /// <summary>
    /// Add a log for a <see cref="IEnumerable{T}"/> of <see cref="T"/> using the ToString.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="logLevel"><see cref="LogLevel"/> to apply to the log.</param>
    /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
    /// <param name="items"><see cref="T[]"/> to log.</param>
    /// <param name="formatter">The function formatting a <see cref="T" /> item to a string.</param>
    public static void Log<T>(this ILogger logger, LogLevel logLevel, String preface, T[] items, Func<T, String> formatter)
    {
        String message = $"{preface} :{_separator}{String.Join(_separator, items.Select(l => formatter(l)))}";
        logger.Log(logLevel, message);
    }

    /// <summary>
    /// Add a log for a <see cref="IEnumerable{T}"/> of <see cref="T"/> using the ToString.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="logLevel"><see cref="LogLevel"/> to apply to the log.</param>
    /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
    /// <param name="items"><see cref="IEnumerable{T}"/> of <see cref="T"/> to log.</param>
    public static void Log<T>(this ILogger logger, LogLevel logLevel, String preface, IEnumerable<T> items)
    {
        String message = $"{preface} :{_separator}{String.Join(_separator, items)}";
        logger.Log(logLevel, message);
    }

    /// <summary>
    /// Add a log for a <see cref="IEnumerable{T}"/> of <see cref="T"/> using the ToString.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="logLevel"><see cref="LogLevel"/> to apply to the log.</param>
    /// <param name="preface"><see cref="String"/> representation of a small message to preface the log.</param>
    /// <param name="items"><see cref="IEnumerable{T}"/> of <see cref="T"/> to log.</param>
    /// <param name="formatter">The function formatting a <see cref="T" /> item to a string.</param>
    public static void Log<T>(this ILogger logger, LogLevel logLevel, String preface, IEnumerable<T> items, Func<T, String> formatter)
    {
        String message = $"{preface} :{_separator}{String.Join(_separator, items.Select(l => formatter(l)))}";
        logger.Log(logLevel, message);
    }

    #endregion Public

    #region Private

    private static readonly String _separator = $"{Environment.NewLine}\t\t-";

    #endregion Private
}
