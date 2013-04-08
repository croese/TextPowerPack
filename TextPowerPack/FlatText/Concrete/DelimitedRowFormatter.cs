using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents a row formatter that combines its fields
  /// with a delimiter
  /// </summary>
  public class DelimitedRowFormatter : IRowFormatter
  {
    /// <summary>
    /// The default row formatter
    /// </summary>
    private static DelimitedRowFormatter _default =
      new DelimitedRowFormatter(RowSettings.Default);

    /// <summary>
    /// Gets the default row formatter
    /// </summary>
    public static DelimitedRowFormatter Default
    {
      get
      {
        Contract.Ensures(Contract.Result<DelimitedRowFormatter>() != null);
        return _default;
      }
    }

    /// <summary>
    /// The row settings that determine how the row will be created
    /// </summary>
    public RowSettings Settings { get; private set; }

    /// <summary>
    /// Creates a new DelimitedRowFormatter with the settings given
    /// by <paramref name="settings"/>
    /// </summary>
    /// <param name="settings">Specifies how the row string should be created</param>
    public DelimitedRowFormatter(RowSettings settings)
    {
      this.Settings = settings;
    }

    /// <summary>
    /// Creates the flat-text row from the individual fields
    /// in <paramref name="fieldValues"/>, delimited by the delimiter in Settings
    /// </summary>
    /// <param name="fieldValues">The string values for the row's fields</param>
    /// <returns>The formatted row string</returns>
    public virtual string FormatRow(IEnumerable<string> fieldValues)
    {
      var delim = Settings.Delimiter.ToString();
      var retVal = string.Join(delim, fieldValues);

      return retVal;
    }
  }
}
