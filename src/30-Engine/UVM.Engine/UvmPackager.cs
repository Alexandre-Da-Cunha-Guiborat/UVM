using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UVM.Interface.Interfaces;
using UVM.Logging;

namespace UVM.Engine;

/// <summary>
/// Library for <see cref="IVersionable" /> packaging.
/// </summary>
public static class UvmPackager
{
    #region Public

    /// <summary>
    /// Generates the given file.
    /// </summary>
    /// <param name="gfToGenerate"><see cref="IGenerable"/> to generate.</param>
    /// <param name="args"><see cref="Dictionary{TKey, TValue}"/> mapping a <see cref="String" /> flag to a <see cref="String" /> value to specify arguments for generation.</param>
    /// <returns><see langword="true" /> => generation succeed, <see langword="false" /> => otherwise.</returns>
    public static Boolean GenerateFile(IGenerable gfToGenerate, IDictionary<String, String> args)
    {
        return gfToGenerate.Generate(args);
    }

    /// <summary>
    /// Generates all files.
    /// </summary>
    /// <param name="gfToGenerateOrdered"><see cref="IEnumerable{T}"/> of all <see cref="IGenerable"/> to generate.</param>
    /// <param name="args"><see cref="IEnumerable{T}"/> of <see cref="Dictionary{TKey, TValue}"/> mapping a <see cref="String"/> flag to a <see cref="String"/> value to specify arguments for generations.</param>
    /// <returns><see langword="true"/> => all generations succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean GenerateFiles(IEnumerable<IGenerable> gfToGenerateOrdered, IEnumerable<IDictionary<String, String>> args)
    {
        IList<IGenerable> gfToGenerateOrderedList = gfToGenerateOrdered.ToList();
        IList<IDictionary<String, String>> argsList = args.ToList();

        if (gfToGenerateOrdered.Count() != args.Count())
        {
            _logger.Log(LogLevel.Error, $"filesToGeneratedOrdered and args must be the same size.");
            return false;
        }

        for (Int32 i = 0; i < gfToGenerateOrderedList.Count; i++)
        {
            IGenerable fileToGenerate = gfToGenerateOrderedList[i];
            IDictionary<String, String> argList = argsList[i];

            if (!GenerateFile(fileToGenerate, argList))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Generate all files.
    /// </summary>
    /// <param name="gfToGenerateOrdered"><see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> of all <see cref="IGenerable"/> to generate.</param>
    /// <param name="args"><see cref="IEnumerable{T}"/> of <see cref="IEnumerable{T}"/> of <see cref="Dictionary{TKey, TValue}"/> mapping a <see cref="String"/> flag to a <see cref="String"/> value to specify arguments for generations.</param>
    /// <returns><see langword="true"/> => all generations succeed, <see langword="false"/> => otherwise.</returns>
    public static Boolean GenerateFiles(IEnumerable<IEnumerable<IGenerable>> gfToGenerateOrdered, IEnumerable<IEnumerable<IDictionary<String, String>>> args)
    {
        IList<IEnumerable<IGenerable>> gfToGenerateOrderedList = gfToGenerateOrdered.ToList();
        IList<IEnumerable<IDictionary<String, String>>> argsList = args.ToList();

        if (gfToGenerateOrdered.Count() != args.Count())
        {
            _logger.Log(LogLevel.Error, $"filesToGeneratedOrdered and args must be the same size.");
            return false;
        }

        for (Int32 i = 0; i < gfToGenerateOrderedList.Count; i++)
        {
            IEnumerable<IGenerable> subFilesToGenerateOrdered = gfToGenerateOrderedList[i];
            IEnumerable<IDictionary<String, String>> argsSub = argsList[i];

            if (!GenerateFiles(subFilesToGenerateOrdered, argsSub))
            {
                return false;
            }
        }

        return true;
    }

    #endregion Public

    #region Private

    /// <summary>
    /// <see cref="ILogger"/> to use within that class.
    /// </summary>
    private static ILogger _logger = UvmLogger.Instance;

    #endregion Private
}
