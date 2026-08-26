using Microsoft.Extensions.Logging;
using System;

namespace UVM.Logging;

/// <summary>
/// Representation of a Log entry.
/// </summary>
internal sealed class UvmLogEntry
{
    #region Public

    /// <summary>
    /// <see cref="DateTime"/> representation of the log's date.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// <see cref="LogLevel"/> of this Log.
    /// </summary>
    public LogLevel Level { get; set; }

    /// <summary>
    /// <see cref="String"/> representation of the log's title.
    /// </summary>
    public String Title { get; set; }

    /// <summary>
    /// <see cref="String"/> representation of the log's message.
    /// </summary>
    public String Message { get; set; }

    /// <summary>
    /// <see cref="UvmLogEntry"/>'s constructor.
    /// </summary>
    /// <param name="logLevel"><see cref="LogLevel"/> of the log.</param>
    /// <param name="title"><see cref="String"/> representation of the log's title.</param>
    /// <param name="message"><see cref="String"/> representation of the log's message.</param>
    public UvmLogEntry(LogLevel logLevel, String title, String message)
    {
        Date = DateTime.Now;
        Level = logLevel;
        Title = title;
        Message = message;
    }

    /// <summary>
    /// Computes the <see cref="String"/> representation of the <see cref="UvmLogEntry"/>. 
    /// </summary>
    /// <returns>The <see cref="String"/> representation of the <see cref="UvmLogEntry"/>.</returns>
    public override String ToString()
    {
        String separator = $":{Environment.NewLine}\t";

        return $"{Date.ToString("s")} | {Level} | {Title}{separator}{Message}";
    }

    #endregion Public
}
