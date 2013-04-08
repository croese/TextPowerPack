using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;

namespace TextPowerPack.FlatText.Interfaces
{
  /// <summary>
  /// Represents an object that can format a given 
  /// field into an appropriate string
  /// </summary>
  [ContractClass(typeof(IFieldFormatterContract))]
  public interface IFieldFormatter
  {
    /// <summary>
    /// The field reader to get the value to be stringified
    /// </summary>
    IFieldReader FieldInfo { get; }

    /// <summary>
    /// The settings that determine the formatted string
    /// </summary>
    FieldSettings Settings { get; }

    /// <summary>
    /// Handles formatting the value from <paramref name="instance"/>
    /// into a string based on the attached Settings
    /// </summary>
    /// <param name="instance">The object to get the field value from</param>
    /// <returns>The formatted string</returns>
    string FormatField(object instance);
  }

  [ContractClassFor(typeof(IFieldFormatter))]
  abstract class IFieldFormatterContract : IFieldFormatter
  {
    public IFieldReader FieldInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<IFieldReader>() != null);
        return default(IFieldReader);
      }
    }

    public FieldSettings Settings
    {
      get
      {
        Contract.Ensures(Contract.Result<FieldSettings>() != null);
        return default(FieldSettings);
      }
    }

    public string FormatField(object instance)
    {
      Contract.Requires(instance != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}
