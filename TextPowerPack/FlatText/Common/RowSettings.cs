using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TextPowerPack.FlatText.Common
{
  /// <summary>
  /// Represents row-level settings
  /// </summary>
  public class RowSettings
  {
    /// <summary>
    /// The default settings for a row
    /// </summary>
    private static RowSettings _default = new RowSettings
    {
      Delimiter = ',',
      LineTerminator = Environment.NewLine
    };

    /// <summary>
    /// Gets the default row settings
    /// </summary>
    public static RowSettings Default
    {
      get
      {
        Contract.Ensures(Contract.Result<RowSettings>() != null);
        return _default;
      }
    }

    /// <summary>
    /// The string that separates one row of output from the next
    /// </summary>
    public string LineTerminator { get; set; }

    /// <summary>
    /// The delimiter between fields in this row
    /// </summary>
    public char Delimiter { get; set; }
  }
}
