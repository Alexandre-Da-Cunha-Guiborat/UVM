using LibGit2Sharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UVM.Interface;
using UVM.Logging;
using Microsoft.Extensions.Logging;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UVM.Service
{
    /// <summary>
    /// GitUtils class for easier usage of UVM.Engine.
    /// </summary>
    public static class UVMGitUtils
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly.
        /// </summary>
        private const string _asmName = "UVM.Service";

        /// <summary>
        /// String representation of the class.
        /// </summary>
        private const string _className = "UVMGitUtils";

        #endregion DEBUG

        #region Public

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function

        /// <summary>
        /// Check if the given folder is a git directory.
        /// </summary>
        /// <param name="gitDirPath">String representation of the absolute path to the git directory.</param>
        /// <returns>true => the given path leads to a git directory, false => otherwise.</returns>
        public static bool IsGitDirectory(string gitDirPath)
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
        /// <param name="gitDirPath">String representation of the absolute path to the git directory.</param>
        /// <param name="branchName">String representation of branch name.</param>
        /// <param name="commitIdRef">String representation of the reference's commitId.</param>
        /// <param name="commitId">String representation of the commitId.</param>
        /// <returns>true => a rebase is needed, false => otherwise.</returns>
        public static bool IsRebaseNeeded(string gitDirPath, string branchName, string commitIdRef, string commitId)
        {
            Repository repo = new Repository(gitDirPath);
            Branch currentBranch = repo.Branches[branchName];

            // May make sens to extract that function. IsBranchInGitDir(string gitDirPath, string branchName)
            if (currentBranch is null)
            {
                string titleNoCurrentBranch = UVMLogger.CreateTitle(_asmName, _className, $"IsRebaseNeeded");
                string messageNoCurrentBranch = $"The current branch with name {branchName} does not exist in that git repo.";
                UVMLogger.AddLog(LogLevel.Error, titleNoCurrentBranch, messageNoCurrentBranch);

                //? Technicaly, this not true... 
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
                string messageCommitNull = $"Either the ref commit or the commit to check for do not exist in that git repository.";
                UVMLogger.AddLog(LogLevel.Error, titleCommitNull, messageCommitNull);

                //? Technicaly, this not true... 
                return true;
            }
            else
            {
                Commit commonAncestor = repo.ObjectDatabase.FindMergeBase(refCommit, commit);
                if (commonAncestor is null)
                {
                    string titleAncestor = UVMLogger.CreateTitle(_asmName, _className, $"IsRebaseNeeded");
                    string messageAncestor = $"The two given commits have no ancestor in common.";
                    UVMLogger.AddLog(LogLevel.Information, titleAncestor, messageAncestor);
                    return true;
                }

                string title = UVMLogger.CreateTitle(_asmName, _className, $"IsRebaseNeeded");
                string message = $"The two given commits have an ancestor in common. ({commonAncestor.Id})";
                UVMLogger.AddLog(LogLevel.Information, title, message);
                return false;
            }
        }

        /// <summary>
        /// Compute the list of all modified files since a given commit in the given git directory.
        /// </summary>
        /// <param name="gitDirPaths">List of string representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs">List of string representing the commit Id we want to compare to.</param>
        /// <param name="commitIds">List of string representing the commitId we want to compare.</param>
        /// <returns>List of string representing the absolute path to all modified files, accross all git directories.</returns>
        public static List<string> GetGitDiffs(List<string> gitDirPaths, List<string> commitIdRefs, List<string> commitIds)
        {
            if (gitDirPaths.Count != commitIdRefs.Count || gitDirPaths.Count != commitIds.Count)
            {
                return [];
            }

            List<string> modifiedFiles = [];
            for (int i = 0; i < gitDirPaths.Count; i++)
            {
                if (IsGitDirectory(gitDirPaths[i]))
                {
                    List<string> modifiedF = GetGitDiffCommitWithCommitRef(gitDirPaths[i], commitIdRefs[i], commitIds[i]);
                    modifiedFiles = modifiedFiles.Concat(modifiedF).ToList();
                }
            }

            return modifiedFiles;
        }

        /// <summary>
        /// Compute the list of all <see cref="I_VersionnableFile"> that have a modified file (with matching extensions) in it.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that may need to be updated.</param>
        /// <param name="gitDirPaths">List of string representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs">List of string representing the commit Id we want to compare to.</param>
        /// <param name="commitIds">List of string representing the commitId we want to compare.</param>
        /// <param name="fExtensions">List of string representing the extensions to look for modified files.</param>
        /// <returns>A list of reference to <see cref="I_VersionnableFile"> that have a file that has been modified.</returns>
        public static List<I_VersionnableFile> ComputeVFWithModifiedFiles(
            List<I_VersionnableFile> vfPool,
            List<string> gitDirPaths,
            List<string> commitIdRefs,
            List<string> commitIds,
            List<string> fExtensions)
        {
            // Compute all modified files in the git direcotry since the previous nbCommit-th.
            List<string> modifiedFiles = GetGitDiffs(gitDirPaths, commitIdRefs, commitIds);

            // Compute all modified files matching extensions with one of the extensions given in parameter.
            List<string> modifiedFilesExts = [];
            foreach (string fExt in fExtensions)
            {
                List<string> modifiedFilesExt = modifiedFiles.Where(f => Path.GetExtension(f) == fExt).ToList();
                modifiedFilesExts = modifiedFilesExts.Concat(modifiedFilesExt).ToList();
            }

            // Compute the list of all VersionnableFile, having a modified file within its children folders.
            List<I_VersionnableFile> vfToUpdate = [];
            if (modifiedFilesExts.Any())
            {
                foreach (I_VersionnableFile vf in vfPool)
                {
                    FileInfo vfFinfo = new FileInfo(vf.VFPath);

                    if (vfFinfo.DirectoryName is null)
                    {
                        // Should never happen but, who knows.
                        string errTitle = UVMLogger.CreateTitle(_asmName, _className, $"ComputeVFWithModifiedFiles");
                        string errLog = $"{vfFinfo.FullName} is at the root";
                        UVMLogger.AddLog(LogLevel.Error, errTitle, errLog);
                        return [];
                    }

                    string parentDir = vfFinfo.DirectoryName;
                    if (modifiedFilesExts.Where(mFExt => mFExt.Contains(parentDir)).Any())
                    {
                        vfToUpdate.Add(vf);
                    }
                }
            }

            string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeVFWithModifiedFiles");
            string preface = "VersionnableFile with modified files inside equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, preface, vfToUpdate);
            return vfToUpdate;
        }

        /// <summary>
        /// Compute the list of all <see cref="I_VersionnableFile"> that hqs been modified.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that may need to be updated.</param>
        /// <param name="gitDirPaths">List of string representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs">List of string representing the commit Id we want to compare to.</param>
        /// <param name="commitIds">List of string representing the commitId we want to compare.</param>
        /// <returns>A list of reference to <see cref="I_VersionnableFile"> that has been modified.</returns>
        public static List<I_VersionnableFile> ComputeModifiedVF(
            List<I_VersionnableFile> vfPool,
            List<string> gitDirPaths,
            List<string> commitIdRefs,
            List<string> commitIds)
        {
            // Compute files that are modified and are ahead of git.
            List<string> modifiedFiles = GetGitDiffs(gitDirPaths, commitIdRefs, commitIds);
            List<I_VersionnableFile> vfAheadOfGit = vfPool.Where(vf => modifiedFiles.Where(mfPath => mfPath.Contains(vf.VFDirPath)).Any()).ToList();

            string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeModifiedVF");
            string preface = "VersionnableFile ahead of git equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, preface, vfAheadOfGit);
            return vfAheadOfGit;
        }

        /// <summary>
        /// Compute the list of all <see cref="I_VersionnableFile"> that has been modified or that have a file in its children forlder that have been modified and that its extensions match one of the givens extensions.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that may need to be updated.</param>
        /// <param name="gitDirPaths">List of string representing the absolute path to a git directory.</param>
        /// <param name="commitIdRefs">List of string representing the commit Id we want to compare to.</param>
        /// <param name="commitIds">List of string representing the commitId we want to compare.</param>
        /// <param name="fExtensions">List of string representing the extensions to look for modified files.</param>
        /// <returns>A list of reference to <see cref="I_VersionnableFile"> needing to be updated.</returns>
        public static List<I_VersionnableFile> ComputeModifiedVFAndVFWithModifiedFiles(
            List<I_VersionnableFile> vfPool,
            List<string> gitDirPaths,
            List<string> commitIdRefs,
            List<string> commitIds,
            List<string> fExtensions)
        {
            List<I_VersionnableFile> vfWithModifiedFiles = ComputeVFWithModifiedFiles(vfPool, gitDirPaths, commitIdRefs, commitIds, fExtensions);
            List<I_VersionnableFile> vfAheadOfGit = ComputeModifiedVF(vfPool, gitDirPaths, commitIdRefs, commitIds);

            List<I_VersionnableFile> vfAllKind = vfWithModifiedFiles.Concat(vfAheadOfGit).Distinct().ToList();

            string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeVFWithModifiedFiles");
            string preface = "Modified VersionnableFiles and versionnableFile with modified files inside equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, preface, vfAllKind);
            return vfAllKind;
        }


        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Public

        #region Protected

        #region Constructor
        // TBD
        #endregion Constructor

        #region Properties
        // TBD
        #endregion Properties

        #region Method
        // TBD
        #endregion Method

        #region Function

        /// <summary>
        /// Compute the list of all modified files since a given commit in the given git directory.
        /// </summary>
        /// <param name="gitDirPath">String representation of the absolute path to the git directory.</param>
        /// <param name="commitIdRef">String representation of the commit Id we want to compare to.</param>
        /// <param name="commitId">String representation of the commitId we want to compare.</param>
        /// <returns>List of string representing the absolute path to all modified files since the ref commit, in the actual branch.</returns>
        private static List<string> GetGitDiffCommitWithCommitRef(string gitDirPath, string commitIdRef, string commitId)
        {

            if (IsGitDirectory(gitDirPath) is false)
            {
                string titleNotGitRepo = UVMLogger.CreateTitle(_asmName, _className, $"GetGitDiffCommitWithCommitRef");
                string messageNotGitRepo = $"The given path do not lead to a git directory. ({gitDirPath})";
                UVMLogger.AddLog(LogLevel.Information, titleNotGitRepo, messageNotGitRepo);
                return [];
            }

            Repository repo = new(gitDirPath);
            Commit currentCommit = repo.Lookup<Commit>(commitId);
            Commit refCommit = repo.Lookup<Commit>(commitIdRef);

            if (refCommit is null || currentCommit is null)
            {
                string titleCommitNull = UVMLogger.CreateTitle(_asmName, _className, $"IsRebaseNeeded");
                string messageCommitNull = $"Either the ref commit or the commit to check for do not exist in that git repository.";
                UVMLogger.AddLog(LogLevel.Error, titleCommitNull, messageCommitNull);

                //? Technicaly, this not true... 
                return [];
            }

            TreeChanges changes = repo.Diff.Compare<TreeChanges>(currentCommit.Tree, refCommit.Tree);
            if (changes is not null)
            {
                List<string> modifiedFiles = changes.Where(f => f.Status is not ChangeKind.Unmodified)
                                                    .Select(f => (gitDirPath + "/" + f.Path).Replace("\\", "/"))
                                                    .ToList();

                string title = UVMLogger.CreateTitle(_asmName, _className, $"GetGitDiffCommitWithCommitRef");
                string preface = $"The list of modified files is equal to";
                UVMLogger.AddLogList(LogLevel.Trace, title, preface, modifiedFiles);
                return modifiedFiles;
            }

            string titleNoChange = UVMLogger.CreateTitle(_asmName, _className, $"GetGitDiffCommitWithCommitRef");
            string message = $"The list of modified files is empty. There are no changes between the two given commits.";
            UVMLogger.AddLog(LogLevel.Information, titleNoChange, message);
            return [];
        }

        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Protected

        #region Private

        #region Constructor
        // TBD
        #endregion Constructor

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
