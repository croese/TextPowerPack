using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TextPowerPack.FlatText.Common
{
  /// <summary>
  /// Represents field-level settings
  /// </summary>
  public class FieldSettings
  {
    /// <summary>
    /// The default settings for a field
    /// </summary>
    private static FieldSettings _default = new FieldSettings
    {
      Format = "G",
      Justification = FieldJustification.Right,
      Order = 0,
      PaddingChar = ' ',
      Width = 0
    };

    /// <summary>
    /// Gets the default field settings
    /// </summary>
    public static FieldSettings Default
    {
      get
      {
        Contract.Ensures(Contract.Result<FieldSettings>() != null);
        return _default;
      }
    }

    /// <summary>
    /// The order of this field in the final output
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// The format string used to generate the string value for
    /// this field's value (see http://msdn.microsoft.com/en-us/library/26etazsy.aspx)
    /// </summary>
    public string Format { get; set; }

    /// <summary>
    /// The max width of this field's value
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The alignment of this field's value
    /// </summary>
    public FieldJustification Justification { get; set; }

    /// <summary>
    /// The character used for padding the field's value 
    /// out to Width
    /// </summary>
    public char PaddingChar { get; set; }

    /// <summary>
    /// Signals whether an exception is thrown when a fixed-width value
    /// overflows its width or whether it gets truncated to fit
    /// </summary>
    public bool ThrowOnOverflow { get; set; }
  }
}
