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
        /// <param name="fileToGenerate">File to generate.</param>
        /// <returns>true => generation successed, false => otherwise</returns>
        public static bool GenerateFiles(I_GenerableFile fileToGenerate)
        {

            fileToGenerate.Generate();
            return true;
        }

        /// <summary>
        /// Generate the given file. (pass it the arguments.)
        /// </summary>
        /// <param name="fileToGenerate">File to generate.</param>
        /// <param name="outputPath">String representation of the absolute path to the location to generate the package.</param>
        /// <param name="args">List of string used to specify arguments for generation.</param>
        /// <returns>true => generation successed, false => otherwise</returns>
        public static bool GenerateFile(I_GenerableFile fileToGenerate, string outputPath, List<string> args)
        {
            string str_args = string.Empty;
            foreach (string arg in args)
            {
                str_args += $"{arg} ";
            }
            string title = UVMLogger.CreateTitle(_asmName, _className, $"GenerateFile");
            string message = $"Generating file {fileToGenerate.VFName} :\n\t outputPath={outputPath}, args={str_args}";
            UVMLogger.AddLog(LogLevel.Information, title, message);

            fileToGenerate.Generate(outputPath, args);
            return true;
        }

        /// <summary>
        /// Generate all files.
        /// </summary>
        /// <param name="filesToGenerateOrdered">List of all files to generate. They must be ordered in the chronological way.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<I_GenerableFile> filesToGenerateOrdered)
        {

            foreach (I_GenerableFile fileToGenerate in filesToGenerateOrdered)
            {
                GenerateFiles(fileToGenerate);
            }
            return true;
        }

        /// <summary>
        /// Generate all files. (pass to each file the arguments (use positioning. file[i], path[i], args[i])).
        /// </summary>
        /// <param name="filesToGenerateOrdered">List of all files to generate. They must be ordered in the chronological way.</param>
        /// <param name="outputPaths">List of string representation of the absolute path to the location to generate the package.</param>
        /// <param name="args">List of List of string used to specifie arguments for each files.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<I_GenerableFile> filesToGenerateOrdered, List<string> outputPaths, List<List<string>> args)
        {
            if (filesToGenerateOrdered.Count != outputPaths.Count || filesToGenerateOrdered.Count != args.Count)
            {
                string title = UVMLogger.CreateTitle(_asmName, _className, $"GenerateFiles");
                string message = $"filesToGeneratedOrdered, outputPaths and args must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);

                return false;
            }

            for (int i = 0; i < filesToGenerateOrdered.Count; i++)
            {
                I_GenerableFile fileToGenerate = filesToGenerateOrdered[i];
                string outputPath = outputPaths[i];
                List<string> argList = args[i];

                GenerateFile(fileToGenerate, outputPath, argList);
            }

            return true;

        }

        /// <summary>
        /// Generate all files.
        /// </summary>
        /// <param name="filesToGenerateOrdered">List of List of all files to generate. They must be ordered in the chronological way.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<List<I_GenerableFile>> filesToGenerateOrdered)
        {

            foreach (List<I_GenerableFile> subFilesToGenerateOrdered in filesToGenerateOrdered)
            {
                GenerateFiles(subFilesToGenerateOrdered);
            }
            return true;
        }

        /// <summary>
        /// Generate all files. (pass to each file the arguments (use positioning. file[i], path[i], args[i])).
        /// </summary>
        /// <param name="filesToGenerateOrdered">List of List of all files to generate. They must be ordered in the chronological way.</param>
        /// <param name="outputPaths">List of List of string representation of the absolute path to the location to generate the package.</param>
        /// <param name="args">List of List of List of string used to specifie arguments for each files.</param>
        /// <returns>true => all generation successed, false => otherwise</returns>
        public static bool GenerateFiles(List<List<I_GenerableFile>> filesToGenerateOrdered, List<List<string>> outputPaths, List<List<List<string>>> args)
        {

            if (filesToGenerateOrdered.Count != outputPaths.Count || filesToGenerateOrdered.Count != args.Count)
            {
                string title = $"{_asmName} | {_className} | GenerateFiles";
                string message = $"filesToGeneratedOrdered, outputPaths and args must be the same size.";
                UVMLogger.AddLog(LogLevel.Error, title, message);
                return false;
            }

            for (int i = 0; i < filesToGenerateOrdered.Count; i++)
            {

                List<I_GenerableFile> subFilesToGenerateOrdered = filesToGenerateOrdered[i];
                List<string> outputPathsSub = outputPaths[i];
                List<List<string>> argsSub = args[i];

                GenerateFiles(subFilesToGenerateOrdered, outputPathsSub, argsSub);
            }

            return true;
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


