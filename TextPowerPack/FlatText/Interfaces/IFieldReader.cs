using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TextPowerPack.FlatText.Interfaces
{
  /// <summary>
  /// Represents an abstract way to read a value from an underlying 'field'
  /// (the meaning of which depends on the concrete implementation)
  /// </summary>
  [ContractClass(typeof(IFieldReaderContract))]
  public interface IFieldReader
  {
    /// <summary>
    /// The name of the underlying field for identification purposes
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the value in the underlying field
    /// </summary>
    /// <param name="onObject">The instance of the object to get the value from</param>
    /// <param name="indices">Optional index array for indexed fields</param>
    /// <returns>The found value</returns>
    object GetValue(object onObject, object[] indices = null);
  }

  [ContractClassFor(typeof(IFieldReader))]
  abstract class IFieldReaderContract : IFieldReader
  {
    public string Name
    {
      get { return default(string); }
    }

    public object GetValue(object onObject, object[] indices = null)
    {
      Contract.Requires(onObject != null);
      return default(object);
    }
  }

}
