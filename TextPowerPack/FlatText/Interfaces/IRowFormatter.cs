using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;

namespace TextPowerPack.FlatText.Interfaces
{
  /// <summary>
  /// Represents an object that can format a series of fields
  /// into a flat-text row
  /// </summary>
  [ContractClass(typeof(IRowFormatterContract))]
  public interface IRowFormatter
  {
    /// <summary>
    /// The row settings that determine how the row will be created
    /// </summary>
    RowSettings Settings { get; }

    /// <summary>
    /// Creates the flat-text row from the individual fields
    /// in <paramref name="fieldValues"/>
    /// </summary>
    /// <param name="fieldValues">The string values for the row's fields</param>
    /// <returns>The formatted row string</returns>
    string FormatRow(IEnumerable<string> fieldValues);
  }

  [ContractClassFor(typeof(IRowFormatter))]
  abstract class IRowFormatterContract : IRowFormatter
  {
    public RowSettings Settings
    {
      get 
      {
        Contract.Ensures(Contract.Result<RowSettings>() != null);
        return default(RowSettings);
      }
    }

    public string FormatRow(IEnumerable<string> fieldValues)
    {
      Contract.Requires(fieldValues != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}
