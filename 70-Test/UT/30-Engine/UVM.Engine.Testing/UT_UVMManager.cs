using System.Collections.Generic;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMManager"/>.
    /// </summary>
    public class UT_UVMManager
    {
        #region Constructor
        // TBD
        #endregion Constructor

        #region Method

        /// <summary>
        /// Test method : 
        /// public static List<I_VersionableFile> ComputeChildrenTree(
        ///     List<I_VersionableFile> vfPool, 
        ///     List<I_VersionableFile> modifiedFiles)
        /// </summary>
        [Fact]
        public void Test_ComputeChildrenTree_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };
            MockedVersionableFile mockedVersionableFile_2 = new MockedVersionableFile()
            {
                VFId = "VF_2"
            };
            MockedVersionableFile mockedVersionableFile_3 = new MockedVersionableFile()
            {
                VFId = "VF_3"
            };

            mockedVersionableFile_1.VFDependencies = [];
            mockedVersionableFile_2.VFDependencies = [mockedVersionableFile_1];
            mockedVersionableFile_3.VFDependencies = [mockedVersionableFile_2];

            List<I_VersionableFile> vfPool = [mockedVersionableFile_1, mockedVersionableFile_2, mockedVersionableFile_3];
            List<I_VersionableFile> modifiedFiles = [mockedVersionableFile_2];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<I_VersionableFile> exp_childrenTree = [mockedVersionableFile_2, mockedVersionableFile_3];

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<I_VersionableFile> act_childrenTree = UVMManager.ComputeChildrenTree(vfPool, modifiedFiles);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_childrenTree, act_childrenTree);
        }

        /// <summary>
        /// Test method : 
        /// public static List<I_VersionableFile> ComputeParentTree(
        ///     List<I_VersionableFile> vfPool, 
        ///     List<I_VersionableFile> vfLeafs)
        /// </summary>
        [Fact]
        public void Test_ComputeParentTree_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };
            MockedVersionableFile mockedVersionableFile_2 = new MockedVersionableFile()
            {
                VFId = "VF_2"
            };
            MockedVersionableFile mockedVersionableFile_3 = new MockedVersionableFile()
            {
                VFId = "VF_3"
            };

            mockedVersionableFile_1.VFDependencies = [];
            mockedVersionableFile_2.VFDependencies = [mockedVersionableFile_1];
            mockedVersionableFile_3.VFDependencies = [mockedVersionableFile_2];

            List<I_VersionableFile> vfPool = [mockedVersionableFile_1, mockedVersionableFile_2, mockedVersionableFile_3];
            List<I_VersionableFile> vfLeafs = [mockedVersionableFile_2];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<I_VersionableFile> exp_parentTree = [mockedVersionableFile_2, mockedVersionableFile_1];

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<I_VersionableFile> act_parentTree = UVMManager.ComputeParentTree(vfPool, vfLeafs);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_parentTree, act_parentTree);
        }

        #endregion Method
    }
}
