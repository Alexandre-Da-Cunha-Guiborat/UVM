using System;
using System.Collections.Generic;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMPackager"/>.
    /// </summary>
    public class UT_UVMPackager
    {
        #region Constructor
        // TBD
        #endregion Constructor

        #region Method

        /// <summary>
        /// Test method : public static Boolean GenerateFile(I_GenerableFile gfToGenerate)
        /// </summary>
        [Fact]
        public void Test_GenerateFile_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFile(mockedGenerableFile_1);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean GenerateFile(I_GenerableFile gfToGenerate, string outputPath, List<string> args)
        /// </summary>
        [Fact]
        public void Test_GenerateFile_2_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFile(mockedGenerableFile_1, "", [""]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : public static Boolean GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered)
        /// </summary>
        [Fact]
        public void Test_GenerateFiles_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFiles([mockedGenerableFile_1]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered, List<String> outputPaths, List<List<String>> args)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_GenerateFiles_2_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = false;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFiles([mockedGenerableFile_1], ["", ""], [[]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered, List<String> outputPaths, List<List<String>> args)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_GenerateFiles_2_1()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFiles([mockedGenerableFile_1], [""], [[]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : public static Boolean GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered)
        /// </summary>
        [Fact]
        public void Test_GenerateFiles_3_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFiles([[mockedGenerableFile_1]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered, List<List<string>> outputPaths, List<List<List<string>>> args)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_GenerateFiles_4_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = false;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFiles([[mockedGenerableFile_1]], [[""], [""]], [[[]]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// public static Boolean GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered, List<List<string>> outputPaths, List<List<List<string>>> args)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_GenerateFiles_4_1()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            MockedGenerableFile mockedGenerableFile_1 = new MockedGenerableFile();

            // ==============================
            // ========== Expected ==========
            // ==============================
            Boolean exp_boolean = true;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            Boolean act_boolean = UVMPackager.GenerateFiles([[mockedGenerableFile_1]], [[""]], [[[]]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        #endregion Method
    }
}
