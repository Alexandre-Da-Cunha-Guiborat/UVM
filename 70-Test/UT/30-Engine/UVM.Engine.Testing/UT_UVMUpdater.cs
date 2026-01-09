using System;
using System.Collections.Generic;
using UVM.Interface.Interfaces;
using UVM.Testing.Models;
using Xunit;

namespace UVM.Engine.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMUpdater"/>.
    /// </summary>
    public class UT_UVMUpdater
    {
        #region Constructor
        // TBD
        #endregion Constructor

        #region Method

        /// <summary>
        /// Test method : 
        /// static public Boolean UpdateFile(I_VersionableFile vfToUpdate,
        ///     List<UInt16> versionIndexes,
        ///     List<BuildType> buildTs,
        ///     List<DigitType> digitTs,
        ///     List<UInt16> semiVersions)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_UpdateFile_1_0()
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
            Boolean act_boolean = UVMUpdater.UpdateFile(mockedVersionableFile_1, [0, 1], [], [], []);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// static public Boolean UpdateFile(I_VersionableFile vfToUpdate,
        ///     List<UInt16> versionIndexes,
        ///     List<BuildType> buildTs,
        ///     List<DigitType> digitTs,
        ///     List<UInt16> semiVersions)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_UpdateFile_1_1()
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
            Boolean act_boolean = UVMUpdater.UpdateFile(mockedVersionableFile_1, [], [], [], []);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// static public Boolean UpdateFiles(List<I_VersionableFile> vfToUpdateOrdered,
        ///     List<List<UInt16>> versionIndexes,
        ///     List<List<BuildType>> buildTs,
        ///     List<List<DigitType>> digitTs,
        ///     List<List<UInt16>> semiVersions)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_UpdateFiles_1_0()
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
            Boolean act_boolean = UVMUpdater.UpdateFiles([mockedVersionableFile_1], [[], []], [[]], [[]], [[]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// static public Boolean UpdateFile(I_VersionableFile vfToUpdate,
        ///     List<UInt16> versionIndexes,
        ///     List<BuildType> buildTs,
        ///     List<DigitType> digitTs,
        ///     List<UInt16> semiVersions)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_UpdateFiles_1_1()
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
            Boolean act_boolean = UVMUpdater.UpdateFiles([mockedVersionableFile_1], [[]], [[]], [[]], [[]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// static public Boolean UpdateFiles(List<I_VersionableFile> vfToUpdateOrdered,
        ///     List<List<UInt16>> versionIndexes,
        ///     List<List<BuildType>> buildTs,
        ///     List<List<DigitType>> digitTs,
        ///     List<List<UInt16>> semiVersions)
        /// </summary>
        /// <remarks>
        /// This test is supposed to fail as the size of the arguments are not matching.
        /// </remarks>
        [Fact]
        public void Test_UpdateFiles_2_0()
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
            Boolean act_boolean = UVMUpdater.UpdateFiles([[mockedVersionableFile_1]], [[[], []]], [[[]]], [[[]]], [[[]]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        /// <summary>
        /// Test method : 
        /// static public Boolean UpdateFiles(List<List<I_VersionableFile>> vfToUpdateOrdered,
        ///     List<List<List<UInt16>>> versionIndexes,
        ///     List<List<List<BuildType>>> buildTs,
        ///     List<List<List<DigitType>>> digitTs,
        ///     List<List<List<UInt16>>> semiVersions)
        /// </summary>
        /// <remarks>
        /// This test is supposed to succeed as the size of the arguments are matching.
        /// </remarks>
        [Fact]
        public void Test_UpdateFiles_2_1()
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
            Boolean act_boolean = UVMUpdater.UpdateFiles([[mockedVersionableFile_1]], [[[]]], [[[]]], [[[]]], [[[]]]);

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_boolean, act_boolean);
        }

        #endregion Method
    }
}
