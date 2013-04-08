using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;

namespace TextPowerPack.FlatText.Interfaces
{
  /// <summary>
  /// Represents an object that provides flat-text serialization
  /// settings for a type or types
  /// </summary>
  [ContractClass(typeof(ISettingsProviderContract))]
  public interface ISettingsProvider
  {
    /// <summary>
    /// Checks if this settings provider specifies settings
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to check</param>
    /// <returns>True if this provider handles <paramref name="theType"/>;
    /// false otherwise</returns>
    [Pure]
    bool RecognizesType(Type theType);

    /// <summary>
    /// Gets the row formatter from this provider
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to get a formatter for</param>
    /// <returns>The found formatter</returns>
    IRowFormatter GetRowFormatter(Type theType);

    /// <summary>
    /// Gets the field formatters from this provider
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to get a formatter for</param>
    /// <returns>The found formatters</returns>
    IFieldFormatter[] GetFieldFormatters(Type theType);
  }

  [ContractClassFor(typeof(ISettingsProvider))]
  abstract class ISettingsProviderContract : ISettingsProvider
  {
    public bool RecognizesType(Type theType)
    {
      Contract.Requires(theType != null);
      return default(bool);
    }

    public IRowFormatter GetRowFormatter(Type theType)
    {
      Contract.Requires(theType != null);
      Contract.Requires(RecognizesType(theType),
        "Unrecognized type");
      return default(IRowFormatter);
    }

    public IFieldFormatter[] GetFieldFormatters(Type theType)
    {
      Contract.Requires(theType != null);
      Contract.Requires(RecognizesType(theType),
        "Unrecognized type");
      return default(IFieldFormatter[]);
    }
  }
}
