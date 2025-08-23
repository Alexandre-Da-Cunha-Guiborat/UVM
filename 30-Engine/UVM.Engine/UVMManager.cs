using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using UVM.Interface.Interfaces;
using UVM.Logging;

namespace UVM.Engine
{
    /// <summary>
    /// Library for <see cref="I_VersionableFile"> management.
    /// </summary>
    public static class UVMManager
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Compute the <see cref="List{T}"> of all <see cref="I_VersionableFile"> in the vfPool that depend on the modified ones.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"> of all <see cref="I_VersionableFile"> that may need to be managed.</param>
        /// <param name="modifiedFiles"><see cref="List{T}"> of all <see cref="I_VersionableFile"> that has been modified.</param>
        /// <returns>The <see cref="List{T}"> of <see cref="I_VersionableFile"> depending on the modified ones. (Unordered)</returns>
        public static List<I_VersionableFile> ComputeChildrenTree(
                List<I_VersionableFile> vfPool,
                List<I_VersionableFile> modifiedFiles)
        {
            // Compute the dependencies.
            foreach (I_VersionableFile vf in vfPool)
            {
                vf.ComputeDependencies(vfPool);
            }

            // Compute recursively, all files that depend on the modified ones.
            List<I_VersionableFile> childrenTree = _ComputeChildrenTreeRecursive(vfPool, modifiedFiles, 0, _maxIter);
            if (childrenTree is null)
            {
                String titleNoFilesToUpdate = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeChildrenTree)}");
                String messageNoFilesToUpdate = "There is no file depending on the modified ones.";
                UVMLogger.AddLog(LogLevel.Information, titleNoFilesToUpdate, messageNoFilesToUpdate);

                childrenTree = [];
            }

            // ? May be able to remove the .Distinct();
            List<I_VersionableFile> filesToUpdate = modifiedFiles.Concat(childrenTree)
                                                                               .Distinct()
                                                                               .ToList();

            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeChildrenTree)}");
            String message = "Files to update equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, message, filesToUpdate);

            return filesToUpdate;
        }

        /// <summary>
        /// Compute the <see cref="List{T}"> of all <see cref="I_VersionableFile"> in the vfPool that the given list of <see cref="I_VersionableFile"> (vfLeafs) depends on in the vfPool.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"> of all <see cref="I_VersionableFile"> that the vfLeaf may depend on.</param>
        /// <param name="vfLeafs"><see cref="List{T}"> of <see cref="I_VersionableFile"> that we want to construct the parents trees.</param>
        /// <returns>The <see cref="List{T}"> of all <see cref="I_VersionableFile"> that the given list of <see cref="I_VersionableFile"> depends on. (Unordered)</returns>
        public static List<I_VersionableFile> ComputeParentTree(
            List<I_VersionableFile> vfPool,
            List<I_VersionableFile> vfLeafs)
        {
            // Compute the dependencies.
            foreach (I_VersionableFile vf in vfPool)
            {
                vf.ComputeDependencies(vfPool);
            }

            foreach (I_VersionableFile vf in vfLeafs)
            {
                vf.ComputeDependencies(vfPool);
            }

            // Compute recursively, all files that the vfLeafs depends on.
            List<I_VersionableFile> parentTree = _ComputeParentTreeRecursive(vfPool, vfLeafs, 0, _maxIter).Distinct().ToList();
            if (parentTree is null)
            {
                String titleNoFilesToUpdate = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeParentTree)}");
                String messageNoFilesToUpdate = "The given file depend on no file in the vfpool.";
                UVMLogger.AddLog(LogLevel.Information, titleNoFilesToUpdate, messageNoFilesToUpdate);

                parentTree = [];
            }

            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(ComputeParentTree)}");
            String message = "Parent tree equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, message, parentTree);

            return parentTree;
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private

        /// <summary>
        /// Maximum number of iteration for recursive function. 
        /// </summary>
        private const UInt32 _maxIter = 10_000;

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all <see cref="I_VersionableFile"/> that depend on the seeds.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that may depend on a file in seeds.</param>
        /// <param name="seeds"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that are used to look for files depending on them.</param>
        /// <param name="nbIter">Number of Iteration done yet.</param>
        /// <param name="maxIter">Maximum number of iteration to do.</param>
        /// <returns>The <see cref="List{T}"/> of <see cref="I_VersionableFile">, that depends on the seeds.</returns>
        private static List<I_VersionableFile> _ComputeChildrenTreeRecursive(
            List<I_VersionableFile> vfPool,
            List<I_VersionableFile> seeds,
            UInt32 nbIter,
            UInt32 maxIter)
        {
            nbIter += 1;
            if (nbIter > maxIter)
            {
                // ? TODO : May want to throw an exception there. And make it critical.
                String errTitle = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(_ComputeChildrenTreeRecursive)}");
                String errMessage = $"Maximum iteration reached ({maxIter}). This may be a sign of a cyclic dependency.";
                UVMLogger.AddLog(LogLevel.Error, errTitle, errMessage);
                return [];
            }

            // Compute the list of files directly dependent on the seeds.
            List<I_VersionableFile> filesToUpdate = [];
            foreach (I_VersionableFile depToCheck in seeds)
            {
                foreach (I_VersionableFile vf in vfPool)
                {
                    foreach (I_VersionableFile vfDep in vf.VFDependencies)
                    {
                        if (vfDep.VFId.Equals(depToCheck.VFId) && vfDep.VFExtension == depToCheck.VFExtension && !seeds.Contains(vf))
                        {
                            filesToUpdate.Add(vf);
                        }
                    }
                }
            }

            if (filesToUpdate.Any())
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(_ComputeChildrenTreeRecursive)}");
                String message = "Newly added files to update equals";
                UVMLogger.AddLogListVF(LogLevel.Trace, title, message, filesToUpdate);

                seeds = seeds.Concat(filesToUpdate).ToList();
                return _ComputeChildrenTreeRecursive(vfPool, seeds, nbIter, maxIter);
            }
            else
            {
                return seeds;
            }
        }

        /// <summary>
        /// Compute the <see cref="List{T}"/> of all files that any vfLeaf depend on.
        /// </summary>
        /// <param name="vfPool"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that may be a parent of any vfLeaf.</param>
        /// <param name="vfLeafs"><see cref="List{T}"/> of all <see cref="I_VersionableFile"> that we want to compute the parent tree.</param>
        /// <param name="nbIter">Number of Iteration done yet.</param>
        /// <param name="maxIter">Maximum number of iteration to do.</param>
        /// <returns>The <see cref="List{T}"/> of <see cref="I_VersionableFile">, representing all files that any vfLeaf depend on.</returns>
        private static List<I_VersionableFile> _ComputeParentTreeRecursive(
            List<I_VersionableFile> vfPool,
            List<I_VersionableFile> vfLeafs,
            UInt32 nbIter,
            UInt32 maxIter)
        {
            nbIter += 1;
            if (nbIter > maxIter)
            {
                // ? TODO : May want to throw an exception there. And make it critical.
                String errTitle = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(_ComputeParentTreeRecursive)}");
                String errMessage = $"Maximum iteration reached ({maxIter}). This may be a sign of a cyclic dependency.";
                UVMLogger.AddLog(LogLevel.Error, errTitle, errMessage);

                return [];
            }

            // Compute the list of files directly dependent on the vfLeafs.
            List<I_VersionableFile> parentFiles = [];
            foreach (I_VersionableFile vfLeaf in vfLeafs)
            {
                foreach (I_VersionableFile vfDep in vfLeaf.VFDependencies)
                {
                    foreach (I_VersionableFile vf in vfPool)
                    {
                        if (vfDep.VFId.Equals(vf.VFId) && vfDep.VFExtension == vf.VFExtension && !vfLeafs.Contains(vf))
                        {
                            parentFiles.Add(vf);
                        }
                    }
                }
            }

            if (parentFiles.Any())
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(_ComputeParentTreeRecursive)}");
                String message = "Newly added files to update equals";
                UVMLogger.AddLogListVF(LogLevel.Trace, title, message, parentFiles);

                vfLeafs = vfLeafs.Concat(parentFiles).ToList();
                return _ComputeParentTreeRecursive(vfPool, vfLeafs, nbIter, maxIter);
            }
            else
            {
                return vfLeafs;
            }
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
        private static String _className = nameof(UVMManager);

        #endregion DEBUG
    }
}
