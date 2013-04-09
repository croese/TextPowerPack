using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  public class AttributeSettingsProvider<TRowAttrib> : ISettingsProvider
    where TRowAttrib : FlatTextRowAttribute
  {
    private readonly Type _attribType;

    public AttributeSettingsProvider()
    {
      _attribType = typeof(TRowAttrib);
    }

    public bool RecognizesType(Type theType)
    {
      return theType.HasCustomAttribute<TRowAttrib>(true);
    }

    public IRowFormatter GetRowFormatter(Type theType)
    {
      var rowAttrib = theType.GetCustomAttributes(_attribType, true).OfType<TRowAttrib>().First();
      return rowAttrib.GetRowFormatter();
    }

    public IFieldFormatter[] GetFieldFormatters(Type theType)
    {
      var rowAttrib = theType.GetCustomAttributes(_attribType, true).OfType<TRowAttrib>().First();
      var fieldType = rowAttrib.FieldAttributeType;
      var retVal = theType.GetProperties().Where(pi => pi.HasCustomAttribute(fieldType, true))
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
