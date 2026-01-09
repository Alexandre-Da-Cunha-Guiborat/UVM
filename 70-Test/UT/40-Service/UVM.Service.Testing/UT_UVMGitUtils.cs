using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UVM.Interface.Interfaces;
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
            String branchName = $"TESTING_V1";
            String commitIdRef = $"f842f8856f15c383c86e2285fb7fd3f9fe106096";
            String commitId = $"904a53c617410fd616469cbd3bd7078d78ca507b";

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
            String branchName = $"TESTING_ROOT";
            String commitIdRef = $"904a53c617410fd616469cbd3bd7078d78ca507b";
            String commitId = $"f842f8856f15c383c86e2285fb7fd3f9fe106096";

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
            List<String> commitIdRefs = [$"f842f8856f15c383c86e2285fb7fd3f9fe106096"];
            List<String> commitIds = [$"904a53c617410fd616469cbd3bd7078d78ca507b"];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<String> exp_gitDiff = [$"{_gitDirPath}/60-Test/Resources/VF_1/src/vf_1_file.txt", $"{_gitDirPath}/60-Test/Resources/VF_2/vf_2.txt"];
            IEnumerable<String> exp_gitDiffPath = exp_gitDiff.Select(path => Path.GetFullPath(path.Replace("\\", "/")));

            // ==============================
            // ========== Workflow ==========
            // ==============================
            List<String> act_gitDiff = UVMGitUtils.GetGitDiffs(gitDirPaths, commitIdRefs, commitIds);
            IEnumerable<String> act_gitDiffPath = act_gitDiff.Select(path => Path.GetFullPath(path.Replace("\\", "/")));


            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_gitDiffPath, act_gitDiffPath);
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
                VFDirPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_1").Replace("\\", "/"),
                VFPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_1").Replace("\\", "/") + $"/vf_1.txt"
            };
            MockedVersionableFile vf_2 = new MockedVersionableFile()
            {
                VFDirPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_2").Replace("\\", "/"),
                VFPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_2").Replace("\\", "/") + $"/vf_2.txt"
            };
            List<I_VersionableFile> vfPool = [vf_1, vf_2];
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$"f842f8856f15c383c86e2285fb7fd3f9fe106096"];
            List<String> commitIds = [$"904a53c617410fd616469cbd3bd7078d78ca507b"];
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
                VFDirPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_1").Replace("\\", "/"),
                VFPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_1").Replace("\\", "/") + $"/vf_1.txt"
            };
            MockedVersionableFile vf_2 = new MockedVersionableFile()
            {
                VFDirPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_2").Replace("\\", "/"),
                VFPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_2").Replace("\\", "/") + $"/vf_2.txt"
            };
            List<I_VersionableFile> vfPool = [vf_1, vf_2];
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$"f842f8856f15c383c86e2285fb7fd3f9fe106096"];
            List<String> commitIds = [$"904a53c617410fd616469cbd3bd7078d78ca507b"];

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
                VFDirPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_1").Replace("\\", "/"),
                VFPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_1").Replace("\\", "/") + $"/vf_1.txt"
            };
            MockedVersionableFile vf_2 = new MockedVersionableFile()
            {
                VFDirPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_2").Replace("\\", "/"),
                VFPath = Path.GetFullPath($"{_gitDirPath}/60-Test/Resources/VF_2").Replace("\\", "/") + $"/vf_2.txt"
            };
            List<I_VersionableFile> vfPool = [vf_1, vf_2];
            List<String> gitDirPaths = [_gitDirPath];
            List<String> commitIdRefs = [$"f842f8856f15c383c86e2285fb7fd3f9fe106096"];
            List<String> commitIds = [$"904a53c617410fd616469cbd3bd7078d78ca507b"];
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
