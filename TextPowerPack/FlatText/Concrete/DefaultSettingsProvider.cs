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
  /// Represents a default provider that returns the appropriate settings
  /// and formatters for a comma-delimited writer
  /// </summary>
  public class DefaultSettingsProvider : ISettingsProvider
  {
    /// <summary>
    /// Checks if this settings provider specifies settings
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to check</param>
    /// <returns>True if this provider handles <paramref name="theType"/>;
    /// false otherwise</returns>
    public bool RecognizesType(Type theType)
    {
      return true;
    }

    /// <summary>
    /// Gets the row formatter from this provider
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to get a formatter for</param>
    /// <returns>The found formatter</returns>
    public IRowFormatter GetRowFormatter(Type theType)
    {
      return DelimitedRowFormatter.Default;
    }

    /// <summary>
    /// Gets the field formatters from this provider
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to get a formatter for</param>
    /// <returns>The found formatters</returns>
    public IFieldFormatter[] GetFieldFormatters(Type theType)
    {
      IFieldFormatter[] retVal = theType.GetProperties()
                                        .Where(pi => pi.CanRead)
                                        .Select(pi =>
                                          new SimpleFieldFormatter(
                                            new PropertyFieldReader(pi), 
                                            FieldSettings.Default))
                                        .ToArray();

      return retVal;
    }
  }
}
