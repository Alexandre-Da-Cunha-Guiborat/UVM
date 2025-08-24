using System;
using System.Collections.Generic;
using System.Reflection;
using UVM.Interface.Enums;
using UVM.Interface.Interfaces;
using UVM.Service;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Service.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMGitUtils"/>.
    /// </summary>
    public class UT_UVMGitUtils
    {

        /// <summary>
        /// <see cref="String"/> representation of the absolute path to the testing assembly dlls.
        /// </summary>
        private static String _asmPath = Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// <see cref="String"/> representation of the absolute path to the git directory of UVM.
        /// </summary>
        private static String _gitDirPath = $"{_asmPath}/../../../../../../../..";

        /// <summary>
        /// <see cref="String"> representation of the name of the root branch of the git tree used ofr UVM testing.
        /// </summary>
        private const String _testingBranchRootName = $"TESTING_ROOT";

        #region Constructor
        // TBD
        #endregion Constructor

        #region Method

        /// <summary>
        /// Test method : public static Boolean IsGitDirectory(String gitDirPath)
        /// </summary>
        [Fact]
        public void Test_IsGitDirectory_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            String gitDirPath = String.Empty;

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = false;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMGitUtils.IsGitDirectory(gitDirPath);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : public static Boolean IsGitDirectory(String gitDirPath)
        /// </summary>
        [Fact]
        public void Test_IsGitDirectory_1_1()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            String gitDirPath = _gitDirPath;

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMGitUtils.IsGitDirectory(gitDirPath);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean IsRebaseNeeded(String gitDirPath, String branchName, String commitIdRef, String commitId)
        /// </summary>
        [Fact]
        public void Test_IsRebaseNeeded_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            String gitDirPath = _gitDirPath;
            String branchName = $"";
            String commitIdRef = $"";
            String commitId = $"";

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = false;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMGitUtils.IsRebaseNeeded(gitDirPath, branchName, commitIdRef, commitId);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean IsRebaseNeeded(String gitDirPath, String branchName, String commitIdRef, String commitId)
        /// </summary>
        [Fact]
        public void Test_IsRebaseNeeded_1_1()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            String gitDirPath = _gitDirPath;
            String branchName = $"";
            String commitIdRef = $"";
            String commitId = $"";

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMGitUtils.IsRebaseNeeded(gitDirPath, branchName, commitIdRef, commitId);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static List<String> GetGitDiffs(List<String> gitDirPaths, List<String> commitIdRefs, List<String> commitIds)
        /// </summary>
        [Fact]
        public void Test_GetGitDiffs_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$""];
            List<String> commitIds = [$""];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<String> exp_gitDiff = [$"{_gitDirPath}/60-Test/Resources/VF_1/src/vf_1_file.txt", $"{_gitDirPath}/60-Test/Resources/VF_2/vf_2.txt"];

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<String> act_gitDiff = UVMGitUtils.GetGitDiffs(gitDirPaths, commitIdRefs, commitIds);


            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_gitDiff, act_gitDiff);
        }

        /// <summary>
        /// Test method : 
        /// public static List<I_VersionableFile> ComputeVFWithModifiedFiles(
        ///     List<I_VersionableFile> vfPool,
        ///     List<String> gitDirPaths,
        ///     List<String> commitIdRefs,
        ///     List<String> commitIds,
        ///     List<String> fExtensions)
        /// </summary>
        [Fact]
        public void Test_ComputeVFWithModifiedFiles_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile vf_1 = new MockedVersionableFile()
            {
                VFDirPath = $"{_gitDirPath}/60-Test/Resources/VF_1"
            };
            MockedVersionableFile vf_2 = new MockedVersionableFile()
            {
                VFDirPath = $"{_gitDirPath}/60-Test/Resources/VF_2"
            };
            List<I_VersionableFile> vfPool = [vf_1, vf_2];
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$""];
            List<String> commitIds = [$""];
            List<String> fExtensions = [];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<I_VersionableFile> exp_VFWithModifiedFiles = [vf_1];

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<I_VersionableFile> act_VFWithModifiedFiles = UVMGitUtils.ComputeVFWithModifiedFiles(vfPool, gitDirPaths, commitIdRefs, commitIds, fExtensions);


            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_VFWithModifiedFiles, act_VFWithModifiedFiles);
        }

        /// <summary>
        /// Test method : 
        /// public static List<I_VersionableFile> ComputeModifiedVF(
        ///             List<I_VersionableFile> vfPool,
        ///             List<String> gitDirPaths,
        ///             List<String> commitIdRefs,
        ///             List<String> commitIds)
        /// </summary>
        [Fact]
        public void Test_ComputeModifiedVF_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile vf_1 = new MockedVersionableFile()
            {
                VFDirPath = $"{_gitDirPath}/60-Test/Resources/VF_1"
            };
            MockedVersionableFile vf_2 = new MockedVersionableFile()
            {
                VFDirPath = $"{_gitDirPath}/60-Test/Resources/VF_2"
            };
            List<I_VersionableFile> vfPool = [vf_1, vf_2];
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$""];
            List<String> commitIds = [$""];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<I_VersionableFile> exp_ModifiedVF = [vf_2];

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<I_VersionableFile> act_ModifiedVF = UVMGitUtils.ComputeModifiedVF(vfPool, gitDirPaths, commitIdRefs, commitIds);


            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_ModifiedVF, act_ModifiedVF);
        }

        /// <summary>
        /// Test method : 
        /// public static List<I_VersionableFile> ComputeModifiedVFAndVFWithModifiedFiles(
        ///             List<I_VersionableFile> vfPool,
        ///             List<String> gitDirPaths,
        ///             List<String> commitIdRefs,
        ///             List<String> commitIds,
        ///             List<String> fExtensions)
        /// </summary>
        [Fact]
        public void Test_ComputeModifiedVFAndVFWithModifiedFiles_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile vf_1 = new MockedVersionableFile()
            {
                VFDirPath = $"{_gitDirPath}/60-Test/Resources/VF_1"
            };
            MockedVersionableFile vf_2 = new MockedVersionableFile()
            {
                VFDirPath = $"{_gitDirPath}/60-Test/Resources/VF_2"
            };
            List<I_VersionableFile> vfPool = [vf_1, vf_2];
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$""];
            List<String> commitIds = [$""];
            List<String> fExtensions = [];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<I_VersionableFile> exp_ModifiedVF = [vf_1, vf_2];

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<I_VersionableFile> act_ModifiedVF = UVMGitUtils.ComputeModifiedVFAndVFWithModifiedFiles(vfPool, gitDirPaths, commitIdRefs, commitIds, fExtensions);


            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_ModifiedVF, act_ModifiedVF);
        }

        #endregion Method
    }
}
