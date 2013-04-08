using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents a field formatter that simpl stringifies its field
  /// value with a specific format
  /// </summary>
  public class SimpleFieldFormatter : IFieldFormatter
  {
    /// <summary>
    /// The field reader to get the value to be stringified
    /// </summary>
    public IFieldReader FieldInfo { get; private set; }

    /// <summary>
    /// The settings that determine the formatted string
    /// </summary>
    public FieldSettings Settings { get; private set; }

    /// <summary>
    /// Creates a new SimpleFieldFormatter that gets values from <paramref name="fieldInfo"/>
    /// and serializes the field values based on <paramref name="settings"/>
    /// </summary>
    /// <param name="fieldInfo">Specifies how to retrieve field values</param>
    /// <param name="settings">Specifies how to format field values</param>
    public SimpleFieldFormatter(IFieldReader fieldInfo, FieldSettings settings)
    {
      this.FieldInfo = fieldInfo;
      this.Settings = settings;
    }

    /// <summary>
    /// Handles formatting the value from <paramref name="instance"/>
    /// into a string based on the attached Settings
    /// </summary>
    /// <param name="instance">The object to get the field value from</param>
    /// <returns>The formatted string</returns>
    public virtual string FormatField(object instance)
    {
      var val = FieldInfo.GetValue(instance);
      var fmtStr = "{0:" + Settings.Format + "}";
      var retVal = val == null ? string.Empty : string.Format(fmtStr, val);

      return retVal;
    }
  }
}
