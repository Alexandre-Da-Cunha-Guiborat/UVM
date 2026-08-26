using System;
using System.Collections.Generic;
using UVM.Interface.Interfaces;

namespace UVM.Testing.Models;

/// <summary>
/// Mocked implementation of a <see cref="IGenerable"/> used for testing purposes. 
/// </summary>
public class GenerableMock : IGenerable
{
    #region Public

    /// <summary>
    /// Generate the output file.
    /// </summary>
    /// <param name="outputDirPath"><see cref="String"/> representation of the absolute path to the output.</param>
    /// <param name="args"><see cref="IDictionary{TKey, TValue}"/> mapping a <see cref="String"/> flag to a <see cref="String"/> value to specify arguments for generation.</param>
    /// <returns><see langword="true"/> => args contains a key "true", <see langword="false"/> => otherwise.</returns>
    public Boolean Generate(IDictionary<string, string> args)
    {
        if (args.ContainsKey($"true"))
        {
            return true;
        }

        return false;
    }

    #endregion Public
}
