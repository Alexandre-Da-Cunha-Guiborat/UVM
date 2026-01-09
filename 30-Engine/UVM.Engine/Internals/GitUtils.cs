using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LibGit2Sharp;
using UVM.Logging;
using UVM.Logging.Enums;

namespace UVM.Engine
{
    /// <summary>
    /// Library of method used internaly in the engine for git manipulation.
    /// </summary>
    internal static class GitUtils
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

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
                UVMLogger.AddLogList(E_LogLevel.TRACE, title, preface, modifiedFiles);

                return modifiedFiles;
            }

            return [];
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private
        // TBD
        #endregion Private

        #region DEBUG
        // TBD
        #endregion DEBUG

        #region DEBUG

        /// <summary>
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        private static String _className = nameof(GitUtils);

        #endregion DEBUG
    }
}


