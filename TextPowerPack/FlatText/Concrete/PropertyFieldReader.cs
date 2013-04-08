using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents a field reader that reads from an underlying .NET property
  /// </summary>
  public class PropertyFieldReader : IFieldReader
  {
    private PropertyInfo _propInfo;

    /// <summary>
    /// The name of the underlying field for identification purposes
    /// </summary>
    public string Name
    {
      get 
      {
        Contract.Assume(_propInfo != null);
        return _propInfo.Name; 
      }
    }

    /// <summary>
    /// Creates a new PropertyFieldReader with <paramref name="propInfo"/>
    /// as the backing field
    /// </summary>
    /// <param name="propInfo">The PropertyInfo representing 
    /// the property to read from</param>
    public PropertyFieldReader(PropertyInfo propInfo)
    {
      Contract.Requires(propInfo != null);
      Contract.Ensures(_propInfo == propInfo);
      _propInfo = propInfo;
    }

    /// <summary>
    /// Gets the value in the underlying field
    /// </summary>
    /// <param name="onObject">The instance of the object to get the value from</param>
    /// <param name="indices">Optional index array for indexed fields</param>
    /// <returns>The found value</returns>
    public object GetValue(object onObject, object[] indices = null)
    {
      Contract.Assume(_propInfo != null);
      return _propInfo.GetValue(onObject, indices);
    }
  }
}
