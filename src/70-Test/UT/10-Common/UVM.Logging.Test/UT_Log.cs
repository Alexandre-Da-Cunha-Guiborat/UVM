using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace UVM.Logging.Testing;

/// <summary>
/// Unit test class for <see cref="UVMLog"/>.
/// </summary>
public class UT_Log
{
    #region Constructor

    /// <summary>
    /// Test constructor : public UVMLog(LogLevel logLevel, String title, String message)
    /// </summary>
    [Fact]
    public void Test_Constructor_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        LogLevel logLevel = LogLevel.Critical;
        String title = "My log title";
        String message = "My log message";

        // ==============================
        // ========== Expected ==========
        // ==============================
        LogLevel exp_level = logLevel;
        String exp_title = title;
        String exp_message = message;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        UvmLogEntry act_log = new UvmLogEntry(logLevel, title, message);
        LogLevel act_level = act_log.Level;
        String act_title = act_log.Title;
        String act_message = act_log.Message;

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_level, act_level);
        Assert.Equal(exp_title, act_title);
        Assert.Equal(exp_message, act_message);
    }

    #endregion Constructor

    #region Method
    // TBD
    #endregion Method
}
