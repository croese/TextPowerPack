using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Exceptions;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents a field formatter that stringifies its field value
  /// into a string of a fixed width
  /// </summary>
  public class FixedWidthFieldFormatter : SimpleFieldFormatter
  {
    /// <summary>
    /// Creates a new FixedWidthFieldFormatter that gets values from <paramref name="fieldInfo"/>
    /// and serializes the fixed-width field values based on <paramref name="settings"/>
    /// </summary>
    /// <param name="fieldInfo">Specifies how to retrieve field values</param>
    /// <param name="settings">Specifies how to format field values</param>
    public FixedWidthFieldFormatter(IFieldReader fieldInfo, FieldSettings settings)
      : base(fieldInfo, settings)
    {
    }

    /// <summary>
    /// Handles formatting the value from <paramref name="instance"/>
    /// into a string based on the attached Settings
    /// </summary>
    /// <param name="instance">The object to get the field value from</param>
    /// <returns>The formatted string</returns>
    public override string FormatField(object instance)
    {
      // apply custom formatting to field value
      var formattedStr = base.FormatField(instance);

      // handle padding and justification
      if (formattedStr.Length < this.Settings.Width)
      {
        formattedStr = this.Settings.Justification == FieldJustification.Left ?
          formattedStr.PadRight(this.Settings.Width, this.Settings.PaddingChar) :
          formattedStr.PadLeft(this.Settings.Width, this.Settings.PaddingChar);
      }
      else if (formattedStr.Length > this.Settings.Width)
      {
        // either we throw...
        if (this.Settings.ThrowOnOverflow)
        {
          throw new FieldOverflowException(this.FieldInfo.Name, formattedStr, 
            this.Settings.Width);
        }
        else // ...or we truncate
        {
          formattedStr = this.Settings.Justification == FieldJustification.Left ?
            formattedStr.Substring(0, this.Settings.Width) :
            formattedStr.Substring(formattedStr.Length - this.Settings.Width);
        }
      }

      return formattedStr;
    }
  }
}
