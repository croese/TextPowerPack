using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TextPowerPack.FlatText.Interfaces
{
  /// <summary>
  /// Represents an object that writes flat-text rows consisting of fields
  /// </summary>
  /// <typeparam name="TRow">The type containing the fields that
  /// make up the rows</typeparam>
  [ContractClass(typeof(IFlatTextWriterContract<>))]
  public interface IFlatTextWriter<TRow>
  {
    /// <summary>
    /// Writes the flat-text row to some backing stream
    /// using the values from <paramref name="instance"/>
    /// </summary>
    /// <param name="instance">The instance to write values from</param>
    void Write(TRow instance);

    /// <summary>
    /// Writes the flat-text row to some backing stream
    /// using the values from <paramref name="instance"/>
    /// followed by a line terminator
    /// </summary>
    /// <param name="instance">The instance to write values from</param>
    void WriteLine(TRow instance);
  }

  [ContractClassFor(typeof(IFlatTextWriter<>))]
  abstract class IFlatTextWriterContract<TRow> : IFlatTextWriter<TRow>
  {
    public void Write(TRow instance)
    {
      Contract.Requires(instance != null);
    }

    public void WriteLine(TRow instance)
    {
      Contract.Requires(instance != null);
    }
  }
}
