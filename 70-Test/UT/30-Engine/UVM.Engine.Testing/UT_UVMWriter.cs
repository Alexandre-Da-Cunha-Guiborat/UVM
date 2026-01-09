using System;
using System.Collections.Generic;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMWriter"/>.
    /// </summary>
    public class UT_UVMWriter
    {
        #region Constructor
        // TBD
        #endregion Constructor

        #region Method

        /// <summary>
        /// Test method : public static Boolean DumpFile(I_VersionableFile vfToDump, String outputPath)
        /// </summary>
        [Fact]
        public void Test_DumpFile_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMWriter.DumpFile(mockedVersionableFile_1, "");

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : public static Boolean DumpFiles(List<I_VersionableFile> vfsToDump, List<string> outputPaths)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_DumpFiles_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = false;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMWriter.DumpFiles([mockedVersionableFile_1], []);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : public static Boolean DumpFiles(List<I_VersionableFile> vfsToDump, List<string> outputPaths)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_DumpFiles_1_1()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMWriter.DumpFiles([mockedVersionableFile_1], [""]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean DumpFiles(List<List<I_VersionableFile>> vfsToDump, List<List<string>> outputPaths)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_DumpFiles_2_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = false;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMWriter.DumpFiles([[mockedVersionableFile_1]], [["", ""]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean DumpFiles(List<List<I_VersionableFile>> vfsToDump, List<List<string>> outputPaths)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_DumpFiles_2_1()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedVersionableFile mockedVersionableFile_1 = new MockedVersionableFile()
            {
                VFId = "VF_1"
            };

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMWriter.DumpFiles([[mockedVersionableFile_1]], [[""]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }


        #endregion Method
    }
}
