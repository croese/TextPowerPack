using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TextPowerPack.FlatText.Exceptions
{
  /// <summary>
  /// Used when one or more fields aren't found by the flat-text formatting
  /// process
  /// </summary>
  [Serializable]
  public class MissingFieldsException : Exception
  {
    /// <summary>
    /// Creates a new MissingFieldsException with a message built
    /// from the missing field names given by <paramref name="fieldsInError"/>
    /// </summary>
    /// <param name="fieldsInError">The names of the missing fields</param>
    public MissingFieldsException(IEnumerable<string> fieldsInError)
      : base(makeErrorMessage(fieldsInError))
    {
      Contract.Requires(fieldsInError != null);
      Contract.Requires(fieldsInError.Any(),
        "The missing fields sequence may not be empty");
    }

    /// <summary>
    /// Helper method to construct the exception message
    /// </summary>
    /// <param name="fieldsInError">The names of the missing fields</param>
    /// <returns>The error message</returns>
    private static string makeErrorMessage(IEnumerable<string> fieldsInError)
    {
      return string.Format("Unable to find the following field(s): {0}{0}{1}",
        Environment.NewLine,
        fieldsInError.Select(f =>
          string.Format("- '{0}'{1}", f, Environment.NewLine)));
    }
  }
}
