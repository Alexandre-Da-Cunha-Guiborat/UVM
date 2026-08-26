using System;
using System.Collections.Generic;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing;

/// <summary>
/// Unit test class for <see cref="UVMUpgrader"/>.
/// </summary>
public class UT_UvmUpgrader
{
    #region Method

    /// <summary>
    /// Test method : public static Boolean UpgradeFile(in IVersionable vfToUpdate, in BuildType buildT, in DigitType digitT)
    /// </summary>
    [Fact]
    public void Test_UpgradeFile_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        String id = $"id";
        IVersion version = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies = [];
        VersionableMock vFMock = new VersionableMock(id, version, dependencies);

        BuildType buildType = BuildType.ALPHA;
        DigitType digitType = DigitType.SEMVER;

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = true;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmUpgrader.UpgradeFile(vFMock, buildType, digitType);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    /// <summary>
    /// Test method : public static Boolean UpgradeFiles(in IList<IVersionable> vfToUpdateOrdered, in IList<BuildType> buildTs, in IList<DigitType> digitTs)
    /// </summary>
    [Fact]
    public void Test_UpgradeFiles_1_0()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        String id = $"id";
        IVersion version = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies = [];
        VersionableMock vFMock = new VersionableMock(id, version, dependencies);

        BuildType buildType = BuildType.BETA;
        DigitType digitType = DigitType.SEMVER;

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = false;

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmUpgrader.UpgradeFiles([vFMock], [buildType], [digitType]);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
    }

    /// <summary>
    /// Test method : public static Boolean UpgradeFiles(in IList<IList<IVersionable>> vfToUpdateOrdered, in IList<IList<BuildType>> buildTs, in IList<IList<DigitType>> digitTs)
    /// </summary>
    [Fact]
    public void Test_UpgradeFiles_1_1()
    {
        // ==============================
        // ========== Inputs ==========
        // ==============================
        String id = $"id";
        IVersion version = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies = [];
        VersionableMock vFMock = new VersionableMock(id, version, dependencies);

        BuildType buildType = BuildType.ALPHA;
        DigitType digitType = DigitType.SEMVER;

        // ==============================
        // ========== Expected ==========
        // ==============================
        Boolean exp_boolean = true;
        IVersion exp_version = new VersionMock(1, 0, 1, BuildType.ALPHA, 1);

        // ==============================
        // ========== Workflow ==========
        // ==============================
        Boolean act_boolean = UvmUpgrader.UpgradeFiles([[vFMock]], [[buildType]], [[digitType]]);
        IVersion act_version = vFMock.Version;

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_boolean, act_boolean);
        Assert.Equal(exp_version, act_version);
    }

    #endregion Method
}
