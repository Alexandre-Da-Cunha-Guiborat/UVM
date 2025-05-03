using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using UVM.Interface;
using UVM.Logging;

namespace UVM.Engine
{
    /// <summary>
    /// Class for VersionnableFile Management.
    /// </summary>
    public static class UVMManager
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly name.
        /// </summary>
        private const string _asmName = "UVM.Engine";

        /// <summary>
        /// String representation of the class name.
        /// </summary>
        private const string _className = "UVMManager";

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
        /// Compute the list of all <see cref="I_VersionnableFile"> in the vfPool that depend on the modified ones.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that may need to be managed.</param>
        /// <param name="modifiedFiles">List of all <see cref="I_VersionnableFile"> that has been modified.</param>
        /// <returns>A list of <see cref="I_VersionnableFile"> needing to be updated.</returns>
        public static List<I_VersionnableFile> ComputeChildrenTree(
            List<I_VersionnableFile> vfPool,
            List<I_VersionnableFile> modifiedFiles)
        {
            // Compute and link the dependencies of each VersionnableObj.
            foreach (I_VersionnableFile vf in vfPool)
            {
                vf.ComputeDependencies(vfPool);
            }

            // Compute recursively, all files that depend on the modified ones.
            List<I_VersionnableFile> childrenTree = _ComputeChildrenTreeRecursive(vfPool, modifiedFiles, 0, _maxIter);
            if (childrenTree is null)
            {
                string titleNoFilesToUpdate = UVMLogger.CreateTitle(_asmName, _className, $"ComputeDependencyTree");
                string messageNoFilesToUpdate = "There is no file dependending on the modified ones.";
                UVMLogger.AddLog(LogLevel.Information, titleNoFilesToUpdate, messageNoFilesToUpdate);
                childrenTree = [];
            }

            // May be able to remove the .Distinc();
            List<I_VersionnableFile> filesToUpdate = modifiedFiles.Concat(childrenTree)
                                                                               .Distinct()
                                                                               .ToList();

            string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeDependencyTree");
            string message = "Files to update equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, message, filesToUpdate);
            return filesToUpdate;
        }

        /// <summary>
        /// Compute the list of all <see cref="I_VersionnableFile"> in the vfPool that the given list of <see cref="I_VersionnableFile"> (vfLeafs) depends on in the vfPool.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that the vfLeaf may depend on.</param>
        /// <param name="vfLeafs">List of <see cref="I_VersionnableFile"> that we want to construct the parents trees.</param>
        /// <returns>List of all <see cref="I_VersionnableFile"> that the given list of <see cref="I_VersionnableFile"> depends on. (Unordered)</returns>
        public static List<I_VersionnableFile> ComputeParentTree(
            List<I_VersionnableFile> vfPool,
            List<I_VersionnableFile> vfLeafs)
        {
            // Compute and link the dependencies of each VersionnableObj.
            foreach (I_VersionnableFile vf in vfPool)
            {
                vf.ComputeDependencies(vfPool);
            }

            foreach (I_VersionnableFile vf in vfLeafs)
            {
                vf.ComputeDependencies(vfPool);
            }


            List<I_VersionnableFile> parentTree = _ComputeParentTreeRecursive(vfPool, vfLeafs, 0, _maxIter).Distinct().ToList();

            if (parentTree is null)
            {
                string titleNoFilesToUpdate = UVMLogger.CreateTitle(_asmName, _className, $"ComputeParentTree");
                string messageNoFilesToUpdate = "The given file depend on no file in the vfpool.";
                UVMLogger.AddLog(LogLevel.Information, titleNoFilesToUpdate, messageNoFilesToUpdate);
                parentTree = [];
            }

            string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeParentTree");
            string message = "Parent tree equals";
            UVMLogger.AddLogListVF(LogLevel.Information, title, message, parentTree);
            return parentTree;
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
        // TBD
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

        /// <summary>
        /// Compute the list of all files that depend on the seeds.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that may depend on a file in seeds.</param>
        /// <param name="seeds">List of all <see cref="I_VersionnableFile"> that are used to look for files depending on them.</param>
        /// <param name="nbIter">Number of Iteration done yet.</param>
        /// <param name="maxIter">Maximum number of iteration to do.</param>
        /// <returns>A list of <see cref="I_VersionnableFile">, dependend on the seeds.</returns>
        private static List<I_VersionnableFile> _ComputeChildrenTreeRecursive(
            List<I_VersionnableFile> vfPool,
            List<I_VersionnableFile> seeds,
            int nbIter,
            uint maxIter)
        {
            nbIter++;
            if (nbIter > maxIter)
            {
                // ? TODO : May want to throw an exception there. And make it critical.
                string errTitle = UVMLogger.CreateTitle(_asmName, _className, $"ComputeDependencyTreeRecursive");
                string errMessage = $"Maximum iteration reachead ({maxIter}). This may be a sign of a cyclic dependencie.";
                UVMLogger.AddLog(LogLevel.Error, errTitle, errMessage);
                return [];
            }

            // Compute the list of files directly dependent on the seeds.
            List<I_VersionnableFile> filesToUpdate = new();
            foreach (I_VersionnableFile depToCheck in seeds)
            {
                foreach (I_VersionnableFile vf in vfPool)
                {
                    foreach (I_VersionnableFile vfDep in vf.VFDependencies)
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
                string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeDependencyTreeRecursive");
                string message = "Newly added files to update equals";
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
        /// Compute the list of all files that any vfLeaf depend on.
        /// </summary>
        /// <param name="vfPool">List of all <see cref="I_VersionnableFile"> that may be a parent of any vfLeaf.</param>
        /// <param name="vfLeafs">List of all <see cref="I_VersionnableFile"> that we want to compute the parent tree.</param>
        /// <param name="nbIter">Number of Iteration done yet.</param>
        /// <param name="maxIter">Maximum number of iteration to do.</param>
        /// <returns>A list of <see cref="I_VersionnableFile">, reprensenting all files that any vfLeaf depend on.</returns>
        private static List<I_VersionnableFile> _ComputeParentTreeRecursive(
            List<I_VersionnableFile> vfPool,
            List<I_VersionnableFile> vfLeafs,
            int nbIter,
            uint maxIter)
        {
            nbIter++;
            if (nbIter > maxIter)
            {
                // ? TODO : May want to throw an exception there. And make it critical.
                string errTitle = UVMLogger.CreateTitle(_asmName, _className, $"ComputeParentTreeRecursive");
                string errMessage = $"Maximum iteration reachead ({maxIter}). This may be a sign of a cyclic dependencie.";
                UVMLogger.AddLog(LogLevel.Error, errTitle, errMessage);
                return [];
            }

            // Compute the list of files directly dependent on the seeds.
            List<I_VersionnableFile> parentFiles = new();
            foreach (I_VersionnableFile vfLeaf in vfLeafs)
            {
                foreach (I_VersionnableFile vf in vfPool)
                {
                    foreach (I_VersionnableFile vfDep in vfLeaf.VFDependencies)
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
                string title = UVMLogger.CreateTitle(_asmName, _className, $"ComputeParentTreeRecursive");
                string message = "Newly added files to update equals";
                UVMLogger.AddLogListVF(LogLevel.Trace, title, message, parentFiles);

                vfLeafs = vfLeafs.Concat(parentFiles).ToList();
                return _ComputeParentTreeRecursive(vfPool, vfLeafs, nbIter, maxIter);
            }
            else
            {
                return vfLeafs;
            }
        }

        #endregion Function

        #region Field

        /// <summary>
        /// Maximum number of iteration for recursive function. 
        /// </summary>
        private const uint _maxIter = 10_000;

        #endregion Field

        #endregion Private
    }
}


