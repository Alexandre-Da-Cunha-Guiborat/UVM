using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UVM.Interface.Interfaces;
using UVM.Logging;


namespace UVM.Engine;

/// <summary>
/// Library for <see cref="IVersionable"> management.
/// </summary>
public static class UvmManager
{
    #region Public

    /// <summary>
    /// Computes the tree as a <see cref="IEnumerable{T}" /> of <see cref="IEnumerable{T}" /> of all <see cref="IVersionable" /> in the vfPool that depend on any of the root.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="vfPool"><see cref="IEnumerable{T}" /> of all <see cref="IVersionable" /> that may need to be managed.</param>
    /// <param name="roots"><see cref="IEnumerable{T}" /> of all <see cref="IVersionable" /> that has been modified.</param>
    /// <returns>The <see cref="IEnumerable{T}" /> of <see cref="IEnumerable{T}" /> of all <see cref="IVersionable" /> depending on the modified ones.</returns>
    /// <remarks>
    /// We choose a list of list to represent the tree as retrieving a layer is the most interesting way of accessing the data.
    /// This is due to the fact that each layer must be processed after all layer with a lower depths.
    /// </remarks>
    public static IEnumerable<IEnumerable<T>> ComputeChildrenTree<T>(IEnumerable<T> vfPool, IEnumerable<T> roots) where T : IVersionable
    {
        IEnumerable<T> childrenTreeFlatten = ComputeChildrenTreeFlat(vfPool, roots);
        return _ComputeChildrenTreeRecursive([], childrenTreeFlatten, 0);
    }

    #endregion Public

    #region Private

    /// <summary>
    /// Maximum number of iteration for recursive function. 
    /// </summary>
    private const UInt32 _maxIter = 10_000;

    /// <summary>
    /// <see cref="ILogger"/> to use within that class.
    /// </summary>
    private static ILogger _logger = UvmLogger.Instance;

    /// <summary>
    /// Creates the children tree in its flatten form.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="childrenTree"><see cref="ICollection{T}"/> of <see cref="IEnumerable{T}"/> of <see cref="T"/> containing each upper layer of the tree already computed.</param>
    /// <param name="childrenTreeFlatten"><see cref="IEnumerable{T}"/> of <see cref="T"/> containing each the nodes that still need to be put on the tree.</param>
    /// <param name="nbIter">Actual iteration number.</param>
    /// <returns>The <see cref="IEnumerable{T}" /> of <see cref="IEnumerable{T}" /> of all <see cref="IVersionable" /> depending on the modified ones.</returns>
    private static IEnumerable<IEnumerable<T>> _ComputeChildrenTreeRecursive<T>(ICollection<IEnumerable<T>> childrenTree, IEnumerable<T> childrenTreeFlatten, UInt32 nbIter) where T : IVersionable
    {
        // Base case. (No more file to update)
        if (childrenTreeFlatten is null || !childrenTreeFlatten.Any())
        {
            return childrenTree;
        }

        // May have reached a circular dependency ...
        if (nbIter > _maxIter)
        {
            _logger.Log(LogLevel.Error, $"The max number of iteration has been reached, either the target is too big or it have a circular dependency. ({nameof(_maxIter)}={_maxIter})");
            return [];
        }

        IList<T> vfToUpdateCurrentLayer = [];
        foreach (T child in childrenTreeFlatten)
        {
            Boolean isIndependent = true;
            foreach (T c in childrenTreeFlatten)
            {
                if (child.Dependencies.Contains(c))
                {
                    isIndependent = false;
                }
            }

            if (isIndependent)
            {
                vfToUpdateCurrentLayer.Add(child);
            }
        }

        childrenTreeFlatten = childrenTreeFlatten.Where(vf => !vfToUpdateCurrentLayer.Contains(vf));

        _logger.Log(LogLevel.Trace, $"List of all csproj in the vfToUpdatePool that have no dependencie in the vfToUpdatePool", vfToUpdateCurrentLayer);

        childrenTree.Add(vfToUpdateCurrentLayer);
        return _ComputeChildrenTreeRecursive(childrenTree, childrenTreeFlatten, nbIter + 1);
    }

    /// <summary>
    /// Computes from a Pool of <see cref="T"/>, all those that depends on the any of the roots.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="vfPool"><see cref="IEnumerable{T}"/> of <see cref="T"/>, containing all <see cref="IVersionable"/> that may depend on a root.</param>
    /// <param name="roots"><see cref="IEnumerable{T}"/> of <see cref="T"/>, all roots.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="T"/>, containing all <see cref="IVersionable"/> that depends on any of the roots.</returns>
    private static IEnumerable<T> ComputeChildrenTreeFlat<T>(IEnumerable<T> vfPool, IEnumerable<T> roots) where T : IVersionable
    {
        // Compute recursively, all files that depend on the modified ones.
        IEnumerable<T> childrenTree = _ComputeChildrenTreeFlatRecursive(vfPool, roots, 0);
        IEnumerable<T> filesToUpdate = roots.Concat(childrenTree).DistinctBy(vf => $@"{vf.Id}");
        _logger.Log(LogLevel.Information, $"Files to update equals", filesToUpdate);

        return filesToUpdate;
    }

    /// <summary>
    /// Computes from a Pool of <see cref="T"/>, all those that depends on the any of the roots.
    /// </summary>
    /// <typeparam name="T">TBD.</typeparam>
    /// <param name="vfPool"><see cref="IEnumerable{T}"/> of <see cref="T"/>, containing all <see cref="IVersionable"/> that may depend on a root.</param>
    /// <param name="roots"><see cref="IEnumerable{T}"/> of <see cref="T"/>, all roots.</param>
    /// <param name="nbIter">Actual iteration number.</param>
    /// <returns><see cref="IEnumerable{T}"/> of <see cref="T"/>, containing all <see cref="IVersionable"/> that depends on any of the roots.</returns>
    private static IEnumerable<T> _ComputeChildrenTreeFlatRecursive<T>(IEnumerable<T> vfPool, IEnumerable<T> roots, UInt32 nbIter) where T : IVersionable
    {
        nbIter += 1;
        if (nbIter > _maxIter)
        {
            _logger.Log(LogLevel.Error, $"Maximum iteration reached ({_maxIter}). This may be a sign of a cyclic dependency.");
            return [];
        }

        // Compute the list of files directly dependent on the seeds.
        IList<T> filesToUpdate = [];
        foreach (T depToCheck in roots)
        {
            foreach (T vf in vfPool)
            {
                if (vf.Dependencies.Any(vfDep => vfDep.Id.Equals(depToCheck.Id) && !roots.Contains(vf)))
                {
                    filesToUpdate.Add(vf);
                }
            }
        }

        if (filesToUpdate.Any())
        {
            _logger.Log(LogLevel.Trace, $"Newly added files to update equals", filesToUpdate);

            roots = roots.Concat(filesToUpdate);
            return _ComputeChildrenTreeFlatRecursive(vfPool, roots, nbIter);
        }
        else
        {
            return roots;
        }
    }

    #endregion Private
}
