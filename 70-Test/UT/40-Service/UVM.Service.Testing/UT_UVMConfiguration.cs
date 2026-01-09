using System;
using System.Collections.Generic;
using UVM.Interface.Enums;
using Xunit;

namespace UVM.Service.Testing
{
    /// <summary>
    /// Unit test class for <see cref="UVMConfiguration"/>.
    /// </summary>
    public class UT_UVMConfiguration
    {
        #region Constructor

        /// <summary>
        /// Test constructor : 
        /// public UVMConfiguration(List<String> gitDirPaths, List<String> commitIdsRef, List<String> commitIds, List<BuildType> buildModes, List<DigitType> digitModes)
        /// </summary>
        [Fact]
        public void Test_Constructor_1_0()
        {
            // ==============================
            // ========== Inputs ==========
            // ==============================
            List<String> gitDirPaths = [$"My {nameof(gitDirPaths)}"];
            List<String> commitIdsRef = [$"My {nameof(commitIdsRef)}"];
            List<String> commitIds = [$"My {nameof(commitIds)}"];
            List<BuildType> buildModes = [BuildType.BuildType_NONE];
            List<DigitType> digitModes = [DigitType.DigitType_NONE];

            // ==============================
            // ========== Expected ==========
            // ==============================
            List<String> exp_gitDirPaths = gitDirPaths;
            List<String> exp_commitIdsRef = commitIdsRef;
            List<String> exp_commitIds = commitIds;
            List<BuildType> exp_buildModes = buildModes;
            List<DigitType> exp_digitModes = digitModes;

            // ==============================
            // ========== Workflow ==========
            // ==============================
            UVMConfiguration act_configuration = new UVMConfiguration(gitDirPaths, commitIdsRef, commitIds, buildModes, digitModes);
            List<String> act_gitDirPaths = act_configuration.GitDirectories;
            List<String> act_commitIdsRef = act_configuration.CommitIdsRef;
            List<String> act_commitIds = act_configuration.CommitIds;
            List<BuildType> act_buildModes = act_configuration.BuildModes;
            List<DigitType> act_digitModes = act_configuration.DigitModes;

            // ==============================
            // ========== Asserts ==========
            // ==============================
            Assert.Equal(exp_gitDirPaths, act_gitDirPaths);
            Assert.Equal(exp_commitIdsRef, act_commitIdsRef);
            Assert.Equal(exp_commitIds, act_commitIds);
            Assert.Equal(exp_buildModes, act_buildModes);
            Assert.Equal(exp_digitModes, act_digitModes);
        }

        #endregion Constructor

        #region Method
        // TBD
        #endregion Method
    }
}
