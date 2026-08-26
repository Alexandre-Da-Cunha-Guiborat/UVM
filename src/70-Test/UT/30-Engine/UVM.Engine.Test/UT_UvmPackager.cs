using System;
using System.Collections.Generic;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing;

/// <summary>
/// Unit test class for <see cref="UVMPackager"/>.
/// </summary>
public class UT_UvmPackager
{
    #region Constructor
    // TBD
    #endregion Constructor

    #region Method

    /// <summary>
    /// Test method : public static Boolean GenerateFile(in IGenerable gfToGenerate, in String outputPath, in IDictionary<String, String> args)
    /// </summary>
    [Fact]
    public void Test_GenerateFile_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        GenerableMock mockedGenerableFile_1 = new GenerableMock();

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = true;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmPackager.GenerateFile(mockedGenerableFile_1, new Dictionary<String, String>() { { "true", "true" } });

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    /// <summary>
    /// Test method : public static Boolean GenerateFiles(in IList<IGenerable> gfToGenerateOrdered, in IList<String> outputPaths, in IList<IDictionary<String, String>> args)
    /// </summary>
    [Fact]
    public void Test_GenerateFiles_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        GenerableMock mockedGenerableFile_1 = new GenerableMock();

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = false;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmPackager.GenerateFiles([mockedGenerableFile_1], [new Dictionary<String, String>()]);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    /// <summary>
    /// Test method : public static Boolean GenerateFiles(in IList<IList<IGenerable>> gfToGenerateOrdered, in IList<IList<string>> outputPaths, in IList<IList<IDictionary<String, String>>> args)
    /// </summary>
    [Fact]
    public void Test_GenerateFiles_1_1()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        GenerableMock mockedGenerableFile_1 = new GenerableMock();

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = true;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmPackager.GenerateFiles([[mockedGenerableFile_1]], [[new Dictionary<String, String>() { { "true", "true" } }]]);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    #endregion Method
}
