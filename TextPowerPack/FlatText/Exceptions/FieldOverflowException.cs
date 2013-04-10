using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TextPowerPack.FlatText.Exceptions
{
  /// <summary>
  /// Represents an error when a fixed-width field overflows its width
  /// </summary>
  [Serializable]
  public class FieldOverflowException : Exception
  {
    /// <summary>
    /// Creates a new FieldOverflowException with a message
    /// built from the parameters
    /// </summary>
    /// <param name="fieldName">The name of the overflowed field</param>
    /// <param name="actualValue">The value that overflowed</param>
    /// <param name="width">The field's width</param>
    public FieldOverflowException(string fieldName, string actualValue, 
      int width) : base(makeErrorMessage(fieldName, actualValue, width))
    {
      Contract.Requires(fieldName != null);
      Contract.Requires(actualValue != null);
    }

    /// <summary>
    /// Helper method to create the actual error message
    /// </summary>
    /// <param name="fieldName">The name of the overflowed field</param>
    /// <param name="actualValue">The value that overflowed</param>
    /// <param name="width">The field's width</param>
    /// <returns>The error message</returns>
    private static string makeErrorMessage(string fieldName, 
      string actualValue, int width)
    {
      return string.Format("The value '{0}' overflowed the '{1}' field which has a width of {2}",
        actualValue, fieldName, width);
    }
  }
}
