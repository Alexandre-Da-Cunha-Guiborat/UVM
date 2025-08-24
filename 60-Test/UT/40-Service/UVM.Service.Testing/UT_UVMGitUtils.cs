using System;
using System.Collections.Generic;
using System.Reflection;
using UVM.Interface.Enums;
using UVM.Service;
using Xunit;

namespace UVM.Service.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMGitUtils"/>.
    /// </summary>
    public class UT_UVMGitUtils
    {
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
            String asmPath = Assembly.GetExecutingAssembly().Location; // bin/Debug of the assembly
            String gitDirPath = $"{asmPath}/../../../../../../../..";  // Relative path to the git directory. 

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

        #endregion Method
    }
}
