using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents a row formatter that simply concatenates all
  /// fields together
  /// </summary>
  public class SimpleRowFormatter : IRowFormatter
  {
    /// <summary>
    /// The row settings that determine how the row will be created
    /// </summary>
    public RowSettings Settings { get; private set; }

    /// <summary>
    /// Creates a new SimpleRowFormatter with the specified settings
    /// </summary>
    /// <param name="settings">The settings that control formtting</param>
    public SimpleRowFormatter(RowSettings settings)
    {
      this.Settings = settings;
    }

    /// <summary>
    /// Creates the flat-text row from the individual fields
    /// in <paramref name="fieldValues"/> by concatenating them together
    /// </summary>
    /// <param name="fieldValues">The string values for the row's fields</param>
    /// <returns>The formatted row string</returns>
    public virtual string FormatRow(IEnumerable<string> fieldValues)
    {
      return string.Join(string.Empty, fieldValues);
    }
  }
}
