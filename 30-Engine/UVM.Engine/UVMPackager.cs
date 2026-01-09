using System;
using System.Collections.Generic;
using System.Reflection;
using UVM.Interface.Interfaces;
using UVM.Logging;
using UVM.Logging.Enums;

namespace UVM.Engine
{
    /// <summary>
    /// Library for <see cref="I_VersionableFile"> packaging.
    /// </summary>
    public class UVMPackager
    {
        #region Singleton
        // TBD
        #endregion Singleton

        #region Public

        /// <summary>
        /// Generate the given file.
        /// </summary>
        /// <param name="gfToGenerate">F"ile to generate.</param>
        /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean GenerateFile(I_GenerableFile gfToGenerate)
        {
            return gfToGenerate.Generate();
        }

        /// <summary>
        /// Generate the given file. (pass it the arguments.)
        /// </summary>
        /// <param name="gfToGenerate">File to generate.</param>
        /// <param name="outputPath"><see cref="String"/> representation of the absolute path to the location to generate the package.</param>
        /// <param name="args"><see cref="List{T}"/> of <see cref="String"/> used to specify arguments for generation.</param>
        /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean GenerateFile(I_GenerableFile gfToGenerate, string outputPath, List<string> args)
        {
            String str_args = String.Empty;
            foreach (String arg in args)
            {
                str_args += $"{arg} ";
            }

            String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GenerateFile)}");
            String message = $"Generating file :\n\t outputPath={outputPath}, args={str_args}";
            UVMLogger.AddLog(E_LogLevel.INFO, title, message);

            return gfToGenerate.Generate(outputPath, args);
        }

        /// <summary>
        /// Generate all files.
        /// </summary>
        /// <param name="gfToGenerateOrdered"><see cref="List{T}"/> of all files to generate. They must be ordered in the chronological way.</param>
        /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered)
        {

            Boolean success = true;
            foreach (I_GenerableFile fileToGenerate in gfToGenerateOrdered)
            {
                if (GenerateFile(fileToGenerate) is false)
                {
                    success = false;
                }

            }
            return success;
        }

        /// <summary>
        /// Generate all files. (pass to each file the arguments (use positioning. file[i], path[i], args[i])).
        /// </summary>
        /// <param name="gfToGenerateOrdered"><see cref="List{T}"/> of all files to generate. They must be ordered in the chronological way.</param>
        /// <param name="outputPaths"><see cref="List{T}"/> of <see cref="String"/> representation of the absolute path to the location to generate the package.</param>
        /// <param name="args"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="String"/> used to specify arguments for each files.</param>
        /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean GenerateFiles(List<I_GenerableFile> gfToGenerateOrdered, List<String> outputPaths, List<List<String>> args)
        {
            if (gfToGenerateOrdered.Count != outputPaths.Count || gfToGenerateOrdered.Count != args.Count)
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GenerateFile)}");
                String message = $"filesToGeneratedOrdered, outputPaths and args must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);

                return false;
            }

            Boolean success = true;
            for (int i = 0; i < gfToGenerateOrdered.Count; i++)
            {
                I_GenerableFile fileToGenerate = gfToGenerateOrdered[i];
                String outputPath = outputPaths[i];
                List<String> argList = args[i];

                if (GenerateFile(fileToGenerate, outputPath, argList) is false)
                {
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Generate all files.
        /// </summary>
        /// <param name="gfToGenerateOrdered"><see cref="List{T}"/> of <see cref="List{T}"/> of all files to generate. They must be ordered in the chronological way.</param>
        /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered)
        {
            Boolean success = true;
            foreach (List<I_GenerableFile> subFilesToGenerateOrdered in gfToGenerateOrdered)
            {
                if (GenerateFiles(subFilesToGenerateOrdered) is false)
                {
                    success = false;
                }
            }
            return success;
        }

        /// <summary>
        /// Generate all files. (pass to each file the arguments (use positioning. file[i], path[i], args[i])).
        /// </summary>
        /// <param name="gfToGenerateOrdered"><see cref="List{T}"/> of <see cref="List{T}"/> of all files to generate. They must be ordered in the chronological way.</param>
        /// <param name="outputPaths"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="String"/> representation of the absolute path to the location to generate the package.</param>
        /// <param name="args"><see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="List{T}"/> of <see cref="String"/> used to specify arguments for each files.</param>
        /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
        public static Boolean GenerateFiles(List<List<I_GenerableFile>> gfToGenerateOrdered, List<List<string>> outputPaths, List<List<List<string>>> args)
        {

            if (gfToGenerateOrdered.Count != outputPaths.Count || gfToGenerateOrdered.Count != args.Count)
            {
                String title = UVMLogger.CreateTitle(_asmName, _className, $"{nameof(GenerateFile)}");
                String message = $"filesToGeneratedOrdered, outputPaths and args must be the same size.";
                UVMLogger.AddLog(E_LogLevel.ERROR, title, message);

                return false;
            }

            Boolean success = true;
            for (int i = 0; i < gfToGenerateOrdered.Count; i++)
            {
                List<I_GenerableFile> subFilesToGenerateOrdered = gfToGenerateOrdered[i];
                List<String> outputPathsSub = outputPaths[i];
                List<List<String>> argsSub = args[i];

                if (GenerateFiles(subFilesToGenerateOrdered, outputPathsSub, argsSub) is false)
                {
                    success = false;
                }
            }
            return success;
        }

        #endregion Public

        #region Protected
        // TBD
        #endregion Protected

        #region Private
        // TBD
        #endregion Private

        #region DEBUG

        /// <summary>
        /// <see cref="String"> representation of the assembly.
        /// </summary>
        private static String _asmName = Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty;

        /// <summary>
        /// <see cref="String"> representation of the class.
        /// </summary>
        private static String _className = nameof(UVMPackager);

        #endregion DEBUG
    }
}
