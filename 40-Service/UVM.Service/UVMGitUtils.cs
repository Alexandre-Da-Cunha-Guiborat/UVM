using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UVM.Interface.Interfaces;
using UVM.Logging;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UVM.Service
{
    /// <summary>
    /// GitUtils class for easier usage of UVM.Engine.
    /// </summary>
    public static class UVMGitUtils
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Check if the given folder is a git directory.
        /// </summary>
        /// <param name="gitDirPath"><see cref="String"/> representation of the absolute path to the git directory.</param>
        /// <returns><see langword="true"/> => the given path leads to a git directory, <see langword="false"/> => otherwise.</returns>
        public static Boolean IsGitDirectory(String gitDirPath)
        {
            try
            {
                Repository repo = new Repository(gitDirPath);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check if a rebase is needed.
        /// </summary>
        /// <param name="gitDirPath"><see cref="String"/> representation of the absolute path to the git directory.</param>
        /// <param name="branchName"><see cref="String"/> representation of branch name.</param>
        /// <param name="commitIdRef"><see cref="String"/> representation of the reference's commitId.</param>
        /// <param name="commitId"><see cref="String"/> representation of the commitId.</param>
        /// <returns><see langword="true"/> => a rebase is needed, <see langword="false"/> => otherwise.</returns>
        public static Boolean IsRebaseNeeded(String gitDirPath, String branchName, String commitIdRef, String commitId)
        {
            Repository repo = new Repository(gitDirPath);
            Branch currentBranch = repo.Branches[branchName];

            // May make sens to extract that function. IsBranchInGitDir(string gitDirPath, string branchName)
            if (currentBranch is null)
            {
                String titleNoCurrentBranch = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(IsRebaseNeeded)}");
                String messageNoCurrentBranch = $"The current branch with name {branchName} does not exist in that git repo.";
                UVMLogger.AddLog(LogLevel.Error, titleNoCurrentBranch, messageNoCurrentBranch);

                //? Technically, this not true... 
                return true;
            }

            // Extract the commit form git using there Ids.
            ObjectId refCommitObjectId = new ObjectId(commitIdRef);
            ObjectId commitObjectId = new ObjectId(commitId);

            ICommitLog currentBranchCommits = currentBranch.Commits;
            Commit? refCommit = currentBranchCommits.Where(c => c.Id == refCommitObjectId).FirstOrDefault();
            Commit? commit = currentBranch.Commits.Where(c => c.Id == commitObjectId).FirstOrDefault();

            if (refCommit is null || commit is null)
            {
                string titleCommitNull = UVMLogger.CreateTitle(_asmName, _className, $"IsRebaseNeeded");
                string messageCommitNull = $"Either the ref commit or the commit to check for do not exist in that branch.";
                UVMLogger.AddLog(LogLevel.Error, titleCommitNull, messageCommitNull);

                //? Technically, this not true... 
                return true;
            }
            else
            {
                Commit commonAncestor = repo.ObjectDatabase.FindMergeBase(refCommit, commit);
                if (commonAncestor is null)
                {
                    string titleAncestor = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(IsRebaseNeeded)}");
                    string messageAncestor = $"The two given commits have no ancestor in common.";
                    UVMLogger.AddLog(LogLevel.Information, titleAncestor, messageAncestor);

                    return true;
                }

                string title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(IsRebaseNeeded)}");
                string message = $"The two given commits have an ancestor in common. ({commonAncestor.Id})";
                UVMLogger.AddLog(LogLevel.Information, title, message);

                return false;
            }
        }

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all modified files since a given commit in the given git directory.
        /// </summary>
        /// <param name="gitDirPaths"><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs"><see cref="List{T}"/> of <see cref="String"/> representing the commit Id we want to compare to.</param>
        /// <param name="commitIds"><see cref="List{T}"/> of <see cref="String"/> representing the commitId we want to compare.</param>
        /// <returns><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to all modified files, across all git directories.</returns>
        public static List<String> GetGitDiffs(List<String> gitDirPaths, List<String> commitIdRefs, List<String> commitIds)
        {
            if (gitDirPaths.Count != commitIdRefs.Count || gitDirPaths.Count != commitIds.Count)
            {
                return [];
            }

            List<String> modifiedFiles = [];
            for (int i = 0; i < gitDirPaths.Count; i++)
            {
                if (IsGitDirectory(gitDirPaths[i]))
                {
                    List<String> modifiedF = GetGitDiffCommitWithCommitRef(gitDirPaths[i], commitIdRefs[i], commitIds[i]);
                    modifiedFiles = modifiedFiles.Concat(modifiedF).ToList();
                }
            }

            return modifiedFiles;
        }

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all <see cref="I_VersionableFile"> that have a modified file (with matching extensions) in it.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that may need to be updated.</param>
        /// <param name="gitDirPaths"><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs"><see cref="List{T}"/> of <see cref="String"/> representing the commit Id we want to compare to.</param>
        /// <param name="commitIds"><see cref="List{T}"/> of <see cref="String"/> representing the commitId we want to compare.</param>
        /// <param name="fExtensions"><see cref="List{T}"/> of <see cref="String"/> representing the extensions to look for modified files.</param>
        /// <returns>A <see cref="List{T}"/> of reference to <see cref="I_VersionableFile"> that have a file that has been modified.</returns>
        public static List<I_VersionableFile> ComputeVFWithModifiedFiles(
            List<I_VersionableFile> vfPool,
            List<String> gitDirPaths,
            List<String> commitIdRefs,
            List<String> commitIds,
            List<String> fExtensions)
        {
            // Compute all modified files in the git directory since the previous nbCommit-th.
            List<String> modifiedFiles = GetGitDiffs(gitDirPaths, commitIdRefs, commitIds);

            // Compute all modified files matching extensions with one of the extensions given in parameter.
            List<String> modifiedFilesExts = [];
            foreach (String fExt in fExtensions)
            {
                List<String> modifiedFilesExt = modifiedFiles.Where(f => Path.GetExtension(f) == fExt).ToList();
                modifiedFilesExts = modifiedFilesExts.Concat(modifiedFilesExt).ToList();
            }

            // Compute the list of all VersionableFile, having a modified file within its children folders.
            List<I_VersionableFile> vfToUpdate = [];
            if (modifiedFilesExts.Any())
            {
                foreach (I_VersionableFile vf in vfPool)
                {
                    FileInfo vfFileInfo = new FileInfo(vf.VFPath);

                    if (vfFileInfo.DirectoryName is null)
                    {
                        // Should never happen but, who knows.
                        String errTitle = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeVFWithModifiedFiles)}");
                        String errLog = $"{vfFileInfo.FullName} is at the root";
                        UVMLogger.AddLog(LogLevel.Error, errTitle, errLog);

                        return [];
                    }

                    String parentDir = vfFileInfo.DirectoryName;
                    if (modifiedFilesExts.Where(mFExt => mFExt.Contains(parentDir)).Any())
                    {
                        vfToUpdate.Add(vf);
                    }
                }
            }

            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeVFWithModifiedFiles)}");
            String preface = $"{nameof(I_VersionableFile)} with modified files inside equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, preface, vfToUpdate);

            return vfToUpdate;
        }

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all <see cref="I_VersionableFile"> that hqs been modified.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that may need to be updated.</param>
        /// <param name="gitDirPaths"><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs"><see cref="List{T}"/> of <see cref="String"/> representing the commit Id we want to compare to.</param>
        /// <param name="commitIds"><see cref="List{T}"/> of <see cref="String"/> representing the commitId we want to compare.</param>
        /// <returns>A <see cref="List{T}"/> of reference to <see cref="I_VersionableFile"> that has been modified.</returns>
        public static List<I_VersionableFile> ComputeModifiedVF(
            List<I_VersionableFile> vfPool,
            List<String> gitDirPaths,
            List<String> commitIdRefs,
            List<String> commitIds)
        {
            // Compute files that are modified and are ahead of git.
            List<String> modifiedFiles = GetGitDiffs(gitDirPaths, commitIdRefs, commitIds);
            List<I_VersionableFile> vfAheadOfGit = vfPool.Where(vf => modifiedFiles.Where(mfPath => mfPath.Contains(vf.VFDirPath)).Any()).ToList();

            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeModifiedVF)}");
            String preface = $"{nameof(I_VersionableFile)} ahead of git equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, preface, vfAheadOfGit);

            return vfAheadOfGit;
        }

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all <see cref="I_VersionableFile"> that has been modified or that have a file in its children folder that have been modified and that its extensions match one of the givens extensions.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that may need to be updated.</param>
        /// <param name="gitDirPaths"><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs"><see cref="List{T}"/> of <see cref="String"/> representing the commit Id we want to compare to.</param>
        /// <param name="commitIds"><see cref="List{T}"/> of <see cref="String"/> representing the commitId we want to compare.</param>
        /// <param name="fExtensions"><see cref="List{T}"/> of <see cref="String"/> representing the extensions to look for modified files.</param>
        /// <returns>A <see cref="List{T}"/> of reference to <see cref="I_VersionableFile"> needing to be updated.</returns>
        public static List<I_VersionableFile> ComputeModifiedVFAndVFWithModifiedFiles(
            List<I_VersionableFile> vfPool,
            List<String> gitDirPaths,
            List<String> commitIdRefs,
            List<String> commitIds,
            List<String> fExtensions)
        {
            List<I_VersionableFile> vfWithModifiedFiles = ComputeVFWithModifiedFiles(vfPool, gitDirPaths, commitIdRefs, commitIds, fExtensions);
            List<I_VersionableFile> vfAheadOfGit = ComputeModifiedVF(vfPool, gitDirPaths, commitIdRefs, commitIds);
            List<I_VersionableFile> vfAllKind = vfWithModifiedFiles.Concat(vfAheadOfGit).Distinct().ToList();

            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeModifiedVFAndVFWithModifiedFiles)}");
            String preface = $"Modified {nameof(I_VersionableFile)} and {nameof(I_VersionableFile)} with modified files inside equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, preface, vfAllKind);

            return vfAllKind;
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all modified files since a given commit in the given git directory.
        /// </summary>
        /// <param name="gitDirPath"><see cref="String"/> representation of the absolute path to the git directory.</param>
        /// <param name="commitIdRef"><see cref="String"/> representation of the commit Id we want to compare to.</param>
        /// <param name="commitId"><see cref="String"/> representation of the commitId we want to compare.</param>
        /// <returns><see cref="List{T}"/> of <see cref="String"/> representing the absolute path to all modified files since the ref commit, in the actual branch.</returns>
        private static List<String> GetGitDiffCommitWithCommitRef(String gitDirPath, String commitIdRef, String commitId)
        {

            if (IsGitDirectory(gitDirPath) is false)
            {
                String titleNotGitRepo = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GetGitDiffCommitWithCommitRef)}");
                String messageNotGitRepo = $"The given path do not lead to a git directory. ({gitDirPath})";
                UVMLogger.AddLog(LogLevel.Information, titleNotGitRepo, messageNotGitRepo);

                return [];
            }

            Repository repo = new(gitDirPath);
            Commit currentCommit = repo.Lookup<Commit>(commitId);
            Commit refCommit = repo.Lookup<Commit>(commitIdRef);

            if (refCommit is null || currentCommit is null)
            {
                string titleCommitNull = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GetGitDiffCommitWithCommitRef)}");
                string messageCommitNull = $"Either the ref commit or the commit to check for do not exist in that branch.";
                UVMLogger.AddLog(LogLevel.Error, titleCommitNull, messageCommitNull);

                //? Technically, this not true... 
                return [];
            }

            TreeChanges changes = repo.Diff.Compare<TreeChanges>(refCommit.Tree, currentCommit.Tree);
            if (changes is not null)
            {
                List<string> modifiedFiles = changes.Where(f => f.Status is not ChangeKind.Unmodified)
                                                    .Select(f => (gitDirPath + "/" + f.Path).Replace("\\", "/"))
                                                    .ToList();

                string title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GetGitDiffCommitWithCommitRef)}");
                string preface = $"The list of modified files is equal to";
                UVMLogger.AddLogList(LogLevel.Trace, title, preface, modifiedFiles);

                return modifiedFiles;
            }

            string titleNoChange = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GetGitDiffCommitWithCommitRef)}");
            string message = $"The list of modified files is empty. There are no changes between the two given commits.";
            UVMLogger.AddLog(LogLevel.Information, titleNoChange, message);

            return [];
        }

        #endregion Private

        #region DEBUG

        /// <summary>
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        private static String _className = nameof(UVMGitUtils);

        #endregion DEBUG
    }
}
