using System;
using System.Collections.Generic;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing;

/// <summary>
/// Unit test class for <see cref="UvmDumper"/>.
/// </summary>
public class UT_UvmWriter
{
    #region Method

    /// <summary>
    /// Test method : public static Boolean DumpFile(IVersionable vfToDump, String outputPath)
    /// </summary>
    [Fact]
    public void Test_DumpFile_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        String id = $"id";
        IVersion version = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies = [];
        VersionableMock vFMock = new VersionableMock(id, version, dependencies);

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = false;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmDumper.DumpFile(vFMock);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    /// <summary>
    /// Test method : public static Boolean DumpFiles(IList<IVersionable> vfsToDump, IList<String> outputPaths)
    /// </summary>
    [Fact]
    public void Test_DumpFiles_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        String id = $"dump";
        IVersion version = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies = [];
        VersionableMock vFMock = new VersionableMock(id, version, dependencies);

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = true;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmDumper.DumpFiles([vFMock]);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    /// <summary>
    /// Test method : public static Boolean DumpFiles(IList<IList<IVersionable>> vfsToDump, IList<IList<String>> outputPaths)
    /// </summary>
    [Fact]
    public void Test_DumpFiles_1_1()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        String id = $"id";
        IVersion version = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies = [];
        VersionableMock vFMock = new VersionableMock(id, version, dependencies);

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = false;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmDumper.DumpFiles([[vFMock]]);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    #endregion Method
}
