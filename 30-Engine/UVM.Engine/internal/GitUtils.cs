using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using UVM.Logging;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UVM.Engine
{
    /// <summary>
    /// Library of method used internaly in the engine for git manipulation.
    /// </summary>
    internal static class GitUtils
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly.
        /// </summary>
        private const string _asmName = "UVM.Engine";

        /// <summary>
        /// String representation of the class.
        /// </summary>
        private const string _className = "GitUtils";

        #endregion DEBUG

        #region Public

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function

        /// <summary>Compute the list of all modified files since a given commit in the given git directory.</summary>
        /// <param name="gitDirPath">String representation of the absolute path to the git directory.</param>
        /// <param name="commitId">String representation of the commitId we want to compare.</param>
        /// <param name="commitIdRef">String representaiton of the commit Id we want to compare to.</param>
        /// <returns>List of string representing the absolute path to all modified files since the ref commit, in the actual branch.</returns>
        static public List<string> GetGitDiffCommitWithCommitRef(string gitDirPath, string commitId, string commitIdRef)
        {
            Repository repo = new(gitDirPath);
            Commit currentCommit = repo.Lookup<Commit>(commitId);
            Commit refCommit = repo.Lookup<Commit>(commitIdRef);

            TreeChanges changes = repo.Diff.Compare<TreeChanges>(currentCommit.Tree, refCommit.Tree);

            if (changes is not null)
            {
                List<string> modifiedFiles = changes.Where(e => e.Status is not ChangeKind.Unmodified)
                                                .Select(e => (gitDirPath + "\\" + e.Path)
                                                .Replace("/", "\\")
                                                .Replace("\\\\", "\\"))
                                                .ToList();

                string title = UVMLogger.CreateTitle(_asmName, _className, "GetGitDiffCommitWithCommitRef");
                string preface = $"List of all modified files between the two given commits. ({commitIdRef}..{commitId})";
                UVMLogger.AddLogList(LogLevel.Trace, title, preface, modifiedFiles);

                return modifiedFiles;
            }

            return [];
        }

        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Public

        #region Protected

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Protected

        #region Private

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Private
    }
}


