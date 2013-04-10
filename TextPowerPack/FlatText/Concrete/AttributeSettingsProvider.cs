using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// A settings provider that gets settings from .NET Attributes
  /// </summary>
  /// <typeparam name="TRowAttrib">The row attribute to look for on classes</typeparam>
  public class AttributeSettingsProvider<TRowAttrib> : ISettingsProvider
    where TRowAttrib : FlatTextRowAttribute
  {
    /// <summary>
    /// The type of the row attribute
    /// </summary>
    private readonly Type _attribType;

    /// <summary>
    /// Creates a new AttributeSettingsProvider with the 
    /// specified row attribute type
    /// </summary>
    public AttributeSettingsProvider()
    {
      _attribType = typeof(TRowAttrib);
    }

    /// <summary>
    /// Checks if this provider can provide settings for
    /// <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to check</param>
    /// <returns>True if this provider handels <paramref name="theType"/>;
    /// false otherwise</returns>
    public bool RecognizesType(Type theType)
    {
      return theType.HasCustomAttribute<TRowAttrib>(true);
    }

    /// <summary>
    /// Gets the row formatter with the settings picked up by this provider
    /// </summary>
    /// <param name="theType">The type to get a row formatter for</param>
    /// <returns>The row formatter</returns>
    public IRowFormatter GetRowFormatter(Type theType)
    {
      var rowAttrib = theType.GetCustomAttributes(_attribType, true).OfType<TRowAttrib>().First();
      return rowAttrib.GetRowFormatter();
    }

    /// <summary>
    /// Gets the field formatters with the settings picked up by this provider
    /// </summary>
    /// <param name="theType">The type to get field formatters for</param>
    /// <returns>The collection of field formatters</returns>
    public IFieldFormatter[] GetFieldFormatters(Type theType)
    {
      var rowAttrib = theType.GetCustomAttributes(_attribType, true).OfType<TRowAttrib>().First();
      var fieldType = rowAttrib.FieldAttributeType;

      // get the public properties tagged with the field attrib type
      // and project them to field formatters
      var retVal = theType.GetProperties()
                          .Where(pi => pi.CanRead && pi.HasCustomAttribute(fieldType, true))
                          .Select(pi =>
                            {
                              var fieldAttrib = pi.GetCustomAttributes(fieldType, true)
                                                  .OfType<FlatTextFieldAttribute>()
                                                  .First();

                              return fieldAttrib.GetFieldFormatter(pi);
                            })
                            .OrderBy(ff => ff.Settings.Order)
                            .ToArray();

      return retVal;
    }
  }
}
