using System;
using System.Collections.Generic;

namespace UVM.Interface.Interfaces;

/// <summary>
/// Interface for files that can generate outputs.
/// </summary>
public interface IGenerable
{
    #region Public

    /// <summary>
    /// Generate the output file.
    /// </summary>
    /// <param name="args"><see cref="IDictionary{TKey, TValue}"/> mapping a <see cref="String"/> flag to a <see cref="String"/> value to specify arguments for generation.</param>
    /// <returns><see langword="true"/> => generation succeed, <see langword="false"/> => otherwise.</returns>
    public Boolean Generate(IDictionary<String, String> args);

    #endregion Public
}
