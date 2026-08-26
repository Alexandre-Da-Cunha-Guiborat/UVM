using System;
using System.Collections.Generic;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing;

/// <summary>
/// Unit test class for <see cref="UVMManager"/>.
/// </summary>
public class UT_UvmManager
{
    #region Method

    /// <summary>
    /// Test method : public static IEnumerable<IEnumerable<T>> ComputeChildrenTree<T>(IEnumerable<T> vfPool, IEnumerable<T> modifiedFiles) where T : IVersionable
    /// </summary>
    [Fact]
    public void Test_ComputeChildrenTree_1_0()
    {
        // ==============================
        // =========== Inputs ===========
        // ==============================
        String id_1 = $"id_1";
        IVersion version_1 = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_1 = [];
        VersionableMock vFMock_1 = new VersionableMock(id_1, version_1, dependencies_1);

        String id_2_1 = $"id_2_1";
        IVersion version_2_1 = new VersionMock(2, 1, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_1 = [vFMock_1];
        VersionableMock vFMock_2_1 = new VersionableMock(id_2_1, version_2_1, dependencies_2_1);

        String id_2_2 = $"id_2_2";
        IVersion version_2_2 = new VersionMock(2, 2, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_2 = [vFMock_1];
        VersionableMock vFMock_2_2 = new VersionableMock(id_2_2, version_2_2, dependencies_2_2);

        String id_3 = $"id_3";
        IVersion version_3 = new VersionMock(3, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_3 = [vFMock_2_1, vFMock_2_2];
        VersionableMock vFMock_3 = new VersionableMock(id_3, version_3, dependencies_3);

        IEnumerable<VersionableMock> vfPool = [vFMock_1, vFMock_2_1, vFMock_2_2, vFMock_3];
        IEnumerable<VersionableMock> modifiedFiles = [vFMock_2_1];

        // ==============================
        // ========== Expected ==========
        // ==============================
        IEnumerable<IEnumerable<VersionableMock>> exp_childrenTree = [[vFMock_2_1], [vFMock_3]];

        // ==============================
        // ========== Workflow ==========
        // ==============================
        foreach (VersionableMock vf in vfPool)
        {
            vf.ComputeDependencies(vfPool);
        }

        IEnumerable<IEnumerable<VersionableMock>> act_childrenTree = UvmManager.ComputeChildrenTree(vfPool, modifiedFiles);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_childrenTree, act_childrenTree);
    }

    /// <summary>
    /// Test method : public static IEnumerable<IEnumerable<T>> ComputeChildrenTree<T>(IEnumerable<T> vfPool, IEnumerable<T> modifiedFiles) where T : IVersionable
    /// </summary>
    [Fact]
    public void Test_ComputeChildrenTree_1_1()
    {
        // ==============================
        // =========== Inputs ===========
        // ==============================
        String id_1 = $"id_1";
        IVersion version_1 = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_1 = [];
        VersionableMock vFMock_1 = new VersionableMock(id_1, version_1, dependencies_1);

        String id_2_1 = $"id_2_1";
        IVersion version_2_1 = new VersionMock(2, 1, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_1 = [vFMock_1];
        VersionableMock vFMock_2_1 = new VersionableMock(id_2_1, version_2_1, dependencies_2_1);

        String id_2_2 = $"id_2_2";
        IVersion version_2_2 = new VersionMock(2, 2, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_2 = [vFMock_1];
        VersionableMock vFMock_2_2 = new VersionableMock(id_2_2, version_2_2, dependencies_2_2);

        String id_3 = $"id_3";
        IVersion version_3 = new VersionMock(3, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_3 = [vFMock_2_1, vFMock_2_2];
        VersionableMock vFMock_3 = new VersionableMock(id_3, version_3, dependencies_3);

        IEnumerable<VersionableMock> vfPool = [vFMock_1, vFMock_2_1, vFMock_2_2, vFMock_3];
        IEnumerable<VersionableMock> modifiedFiles = [vFMock_1];

        // ==============================
        // ========== Expected ==========
        // ==============================
        IEnumerable<IEnumerable<VersionableMock>> exp_childrenTree = [[vFMock_1], [vFMock_2_1, vFMock_2_2], [vFMock_3]];

        // ==============================
        // ========== Workflow ==========
        // ==============================
        foreach (VersionableMock vf in vfPool)
        {
            vf.ComputeDependencies(vfPool);
        }

        IEnumerable<IEnumerable<VersionableMock>> act_childrenTree = UvmManager.ComputeChildrenTree(vfPool, modifiedFiles);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_childrenTree, act_childrenTree);
    }

    /// <summary>
    /// Test method : public static IEnumerable<IEnumerable<T>> ComputeChildrenTree<T>(IEnumerable<T> vfPool, IEnumerable<T> modifiedFiles) where T : IVersionable
    /// </summary>
    [Fact]
    public void Test_ComputeChildrenTree_1_2()
    {
        // ==============================
        // =========== Inputs ===========
        // ==============================
        String id_1 = $"id_1";
        IVersion version_1 = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_1 = [];
        VersionableMock vFMock_1 = new VersionableMock(id_1, version_1, dependencies_1);

        String id_2_1 = $"id_2_1";
        IVersion version_2_1 = new VersionMock(2, 1, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_1 = [vFMock_1];
        VersionableMock vFMock_2_1 = new VersionableMock(id_2_1, version_2_1, dependencies_2_1);

        String id_2_2 = $"id_2_2";
        IVersion version_2_2 = new VersionMock(2, 2, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_2 = [vFMock_1];
        VersionableMock vFMock_2_2 = new VersionableMock(id_2_2, version_2_2, dependencies_2_2);

        String id_3 = $"id_3";
        IVersion version_3 = new VersionMock(3, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_3 = [vFMock_2_1, vFMock_2_2];
        VersionableMock vFMock_3 = new VersionableMock(id_3, version_3, dependencies_3);

        IEnumerable<VersionableMock> vfPool = [vFMock_1, vFMock_2_1, vFMock_2_2, vFMock_3];
        IEnumerable<VersionableMock> modifiedFiles = [vFMock_3];

        // ==============================
        // ========== Expected ==========
        // ==============================
        IEnumerable<IEnumerable<VersionableMock>> exp_childrenTree = [[vFMock_3]];

        // ==============================
        // ========== Workflow ==========
        // ==============================
        foreach (VersionableMock vf in vfPool)
        {
            vf.ComputeDependencies(vfPool);
        }

        IEnumerable<IEnumerable<VersionableMock>> act_childrenTree = UvmManager.ComputeChildrenTree(vfPool, modifiedFiles);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_childrenTree, act_childrenTree);
    }

    /// <summary>
    /// Test method : public static IEnumerable<IEnumerable<T>> ComputeChildrenTree<T>(IEnumerable<T> vfPool, IEnumerable<T> modifiedFiles) where T : IVersionable
    /// </summary>
    [Fact]
    public void Test_ComputeChildrenTree_1_3()
    {
        // ==============================
        // =========== Inputs ===========
        // ==============================
        String id_1 = $"id_1";
        IVersion version_1 = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_1 = [];
        VersionableMock vFMock_1 = new VersionableMock(id_1, version_1, dependencies_1);

        String id_2 = $"id_2";
        IVersion version_2 = new VersionMock(2, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2 = [];
        VersionableMock vFMock_2 = new VersionableMock(id_2, version_2, dependencies_2);

        IEnumerable<VersionableMock> vfPool = [vFMock_1, vFMock_2];
        IEnumerable<VersionableMock> modifiedFiles = [vFMock_1];

        // ==============================
        // ========== Expected ==========
        // ==============================
        IEnumerable<IEnumerable<VersionableMock>> exp_childrenTree = [[vFMock_1]];

        // ==============================
        // ========== Workflow ==========
        // ==============================
        foreach (VersionableMock vf in vfPool)
        {
            vf.ComputeDependencies(vfPool);
        }

        IEnumerable<IEnumerable<VersionableMock>> act_childrenTree = UvmManager.ComputeChildrenTree(vfPool, modifiedFiles);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_childrenTree, act_childrenTree);
    }

    /// <summary>
    /// Test method : public static IEnumerable<IEnumerable<T>> ComputeChildrenTree<T>(IEnumerable<T> vfPool, IEnumerable<T> modifiedFiles) where T : IVersionable
    /// </summary>
    [Fact]
    public void Test_ComputeChildrenTree_1_4()
    {
        // ==============================
        // =========== Inputs ===========
        // ==============================
        String id_1 = $"id_1";
        IVersion version_1 = new VersionMock(1, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_1 = [];
        VersionableMock vFMock_1 = new VersionableMock(id_1, version_1, dependencies_1);

        String id_2_1 = $"id_2_1";
        IVersion version_2_1 = new VersionMock(2, 1, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_1 = [vFMock_1];
        VersionableMock vFMock_2_1 = new VersionableMock(id_2_1, version_2_1, dependencies_2_1);

        String id_2_2 = $"id_2_2";
        IVersion version_2_2 = new VersionMock(2, 2, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_2_2 = [vFMock_1];
        VersionableMock vFMock_2_2 = new VersionableMock(id_2_2, version_2_2, dependencies_2_2);

        String id_3 = $"id_3";
        IVersion version_3 = new VersionMock(3, 0, 0, BuildType.RELEASE, 0);
        IList<IVersionable> dependencies_3 = [vFMock_2_1, vFMock_2_2];
        VersionableMock vFMock_3 = new VersionableMock(id_3, version_3, dependencies_3);

        IEnumerable<VersionableMock> vfPool = [vFMock_1, vFMock_2_1, vFMock_2_2, vFMock_3];
        IEnumerable<VersionableMock> modifiedFiles = [vFMock_1, vFMock_2_1, vFMock_2_2, vFMock_3];

        // ==============================
        // ========== Expected ==========
        // ==============================
        IEnumerable<IEnumerable<VersionableMock>> exp_childrenTree = [[vFMock_1], [vFMock_2_1, vFMock_2_2], [vFMock_3]];

        // ==============================
        // ========== Workflow ==========
        // ==============================
        foreach (VersionableMock vf in vfPool)
        {
            vf.ComputeDependencies(vfPool);
        }

        IEnumerable<IEnumerable<VersionableMock>> act_childrenTree = UvmManager.ComputeChildrenTree(vfPool, vfPool);

        // ==============================
        // ========== Asserts ==========
        // ==============================
        Assert.Equal(exp_childrenTree, act_childrenTree);
    }

    #endregion Method
}
