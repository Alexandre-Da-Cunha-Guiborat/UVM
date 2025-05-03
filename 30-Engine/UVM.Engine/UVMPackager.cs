using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using UVM.Interface;
using UVM.Logging;

namespace UVM.Engine
{
    public class UVMPackager
    {
        #region DEBUG

        /// <summary>
        /// String representation of the assembly name.
        /// </summary>
        private const string _asmName = "UVM.Engine";

        /// <summary>
        /// String representation of the class name.
        /// </summary>
        private const string _className = "UVMPackager";

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
        /// Generate the given file.
        /// </summary>
        /// <param name="gfToGenerate">File to generate.</param>
        /// <returns>true => generation successed, false => otherwise</returns>
        public static bool GenerateFile(I_GenerableFile gfToGenerate)
        {
            return gfToGenerate.Generate();
        }

        /// <summary>
        /// Generate the given file. (pass it the arguments.)
        /// </summary>
        /// <param name="gfToGenerate">File to generate.</param>
        /// <param name="outputPath">String representation of the absolute path to the location to generate the package.</param>
        /// <param name="args">List of string used to specify arguments for generation.</param>
        /// <returns>true => generation successed, false => otherwise</returns>
        public static bool GenerateFile(I_GenerableFile gfToGenerate, string outputPath, List<string> args)
        {
            string str_args = string.Empty;
            foreach (string arg in args)
            {
                str_args += $"{arg} ";
            }
            string title = UVMLogger.CreateTitle(_asmName, _className, $"GenerateFile");
            string message = $"Generating file {gfToGenerate.VFName} :\n\t outputPath={outputPath}, args={str_args}";
            UVMLogger.AddLog(LogLevel.Information, title, message);

            return gfToGenerate.Generate(outputPath, args);
        }

        /// <summary>
        /// Generate all files.
        /// </summary>
        /// <param name="gfToGenerateOrdered">List of all files to generate. They must be ordered in the chronological way.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered)
        {

            bool result = true;
            foreach (I_GenerableFile fileToGenerate in gfToGenerateOrdered)
            {
                if (GenerateFile(fileToGenerate) is false)
                {
                    result = false;
                }

            }
            return result;
        }

        /// <summary>
        /// Generate all files. (pass to each file the arguments (use positioning. file[i], path[i], args[i])).
        /// </summary>
        /// <param name="gfToGenerateOrdered">List of all files to generate. They must be ordered in the chronological way.</param>
        /// <param name="outputPaths">List of string representation of the absolute path to the location to generate the package.</param>
        /// <param name="args">List of List of string used to specifie arguments for each files.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered, List<string> outputPaths, List<List<string>> args)
        {
            if (gfToGenerateOrdered.Count != outputPaths.Count || gfToGenerateOrdered.Count != args.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"GenerateFiles");
                string message = $"filesToGeneratedOrdered, outputPaths and args must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);

                return false;
            }

            bool result = true;
            for (int i = 0; i < gfToGenerateOrdered.Count; i++)
            {
                I_GenerableFile fileToGenerate = gfToGenerateOrdered[i];
                string outputPath = outputPaths[i];
                List<string> argList = args[i];

                if (GenerateFile(fileToGenerate, outputPath, argList) is false)
                {
                    result = false;
                }
            }

            return result;

        }

        /// <summary>
        /// Generate all files.
        /// </summary>
        /// <param name="gfToGenerateOrdered">List of List of all files to generate. They must be ordered in the chronological way.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered)
        {
            bool result = true;
            foreach (List<I_GenerableFile> subFilesToGenerateOrdered in gfToGenerateOrdered)
            {
                if (GenerateFiles(subFilesToGenerateOrdered) is false)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Generate all files. (pass to each file the arguments (use positioning. file[i], path[i], args[i])).
        /// </summary>
        /// <param name="gfToGenerateOrdered">List of List of all files to generate. They must be ordered in the chronological way.</param>
        /// <param name="outputPaths">List of List of string representation of the absolute path to the location to generate the package.</param>
        /// <param name="args">List of List of List of string used to specifie arguments for each files.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered, List<List<string>> outputPaths, List<List<List<string>>> args)
        {

            if (gfToGenerateOrdered.Count != outputPaths.Count || gfToGenerateOrdered.Count != args.Count)
            {
                string title = $"{_asmName} | {_className} | GenerateFiles";
                string message = $"filesToGeneratedOrdered, outputPaths and args must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            bool result = true;
            for (int i = 0; i < gfToGenerateOrdered.Count; i++)
            {
                List<I_GenerableFile> subFilesToGenerateOrdered = gfToGenerateOrdered[i];
                List<string> outputPathsSub = outputPaths[i];
                List<List<string>> argsSub = args[i];

                if (GenerateFiles(subFilesToGenerateOrdered, outputPathsSub, argsSub) is false)
                {
                    result = false;
                }
            }
            return result;
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
        // TBD
        #endregion Function

        #region Field
        // TBD
        #endregion Field

        #endregion Private
    }
}


