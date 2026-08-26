using LibGit2Sharp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UVM.Logging;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace UVM.Service;

/// <summary>
/// Git utility library for easier usage of UVM.
/// </summary>
public static class UvmGitUtils
{
    #region Public

    /// <summary>
    /// Computes the <see cref="IEnumerable{T}"/> <see cref="String"/> representation of all modified files' path between a given commit in the given git directory.
    /// </summary>
    /// <param name="gitDirPath"><see cref="String"/> representation of the path to the git directory.</param>
    /// <param name="commitId"><see cref="String"/> representing the commit Id we want to compare to.</param>
    /// <param name="commitIdRef"><see cref="String"/> representing the commitId we want to compare.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="String"/> representing the path to all modified files' path.</returns>
    public static IEnumerable<String> GetGitDiff(String gitDirPath, String commitId, String commitIdRef)
    {
        if (!IsGitDirectory(gitDirPath))
        {
            _logger.Log(LogLevel.Error, $"The path to the given git directory is not leading to an actual git directory. ({gitDirPath})");
            return [];
        }

        Repository gitRepo = new Repository(gitDirPath);

        if (IsRebaseNeeded(gitRepo, commitId, commitIdRef))
        {
            return [];
        }

        Commit? currentCommit = gitRepo.Lookup<Commit>(commitId);
        Commit? refCommit = gitRepo.Lookup<Commit>(commitIdRef);
        if (refCommit is null || currentCommit is null)
        {
            _logger.Log(LogLevel.Error, $"Either the ref commit or the commit to check for do not exist in that branch.");
            return [];
        }

        TreeChanges? changes = gitRepo.Diff.Compare<TreeChanges>(refCommit.Tree, currentCommit.Tree);
        if (changes is not null)
        {
            IEnumerable<string> modifiedFiles = changes.Where(f => f.Status is not ChangeKind.Unmodified).Select(f => (gitDirPath + "/" + f.Path).Replace("\\", "/")).ToList();
            IEnumerable<string> result = modifiedFiles.Select(path => Path.GetFullPath(path).Replace("\\", "/"));

            _logger.Log(LogLevel.Trace, $"The list of modified files is equal to", result);
            return result;
        }

        _logger.Log(LogLevel.Trace, $"The list of modified files is empty. There are no changes between the two given commits.");
        return [];
    }

    #endregion Public

    #region Private

    /// <summary>
    /// <see cref="ILogger"/> to use within that class.
    /// </summary>
    private static ILogger _logger = UvmLogger.Instance;

    /// <summary>
    /// Checks if the given folder is a git directory.
    /// </summary>
    /// <param name="gitDirPath"><see cref="String"/> representation of the path to the git directory.</param>
    /// <returns><see langword="true"/> => the given path leads to a git directory, <see langword="false"/> => otherwise.</returns>
    private static Boolean IsGitDirectory(String gitDirPath)
    {
        try
        {
            _ = new Repository(gitDirPath);
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if a rebase is needed.
    /// </summary>
    /// <param name="gitRepo"><see cref="Repository"/> representation of git repository.</param>
    /// <param name="commitIdRef"><see cref="String"/> representation of the reference commitId.</param>
    /// <param name="commitId"><see cref="String"/> representation of the commitId.</param>
    /// <returns><see langword="true"/> => a rebase is needed, <see langword="false"/> => otherwise.</returns>
    private static Boolean IsRebaseNeeded(Repository gitRepo, String commitIdRef, String commitId)
    {
        ICommitLog commits = gitRepo.Commits;

        // Extract the commit form git using there Ids.
        ObjectId refCommitObjectId = new ObjectId(commitIdRef);
        ObjectId commitObjectId = new ObjectId(commitId);

        Commit? refCommit = commits.FirstOrDefault(c => c.Id == refCommitObjectId);
        Commit? commit = commits.FirstOrDefault(c => c.Id == commitObjectId);
        if (refCommit is null || commit is null)
        {
            _logger.Log(LogLevel.Error, $"Either the ref commit or the commit to check for do not exist in that branch.");
            return true;
        }

        Commit? commonAncestor = gitRepo.ObjectDatabase.FindMergeBase(refCommit, commit);
        if (commonAncestor is null)
        {
            _logger.Log(LogLevel.Error, $"The two given commits have no ancestor in common.");
            return true;
        }

        _logger.Log(LogLevel.Error, $"The two given commits have an ancestor in common. ({commonAncestor.Id})");
        return false;
    }

    #endregion Private
}
