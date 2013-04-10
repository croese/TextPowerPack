using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Exceptions;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents a settings provider for specifying fixed-width
  /// settings via a dictionary
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class FixedWidthSettingsProvider<T> : ISettingsProvider
  {
    private Type _theType;

    private IRowFormatter _rowFormatter;

    private IFieldFormatter[] _fieldFormatters;

    /// <summary>
    /// Creates a new FixedWidthSettingsProvider where the formatting
    /// settings come from a field name-to-settings lookup
    /// </summary>
    /// <param name="fieldLookup">The settings to use for the specified fields</param>
    public FixedWidthSettingsProvider(IDictionary<string, FieldSettings> fieldLookup)
    {
      Contract.Requires(fieldLookup != null);

      _theType = typeof(T);
      _rowFormatter = new SimpleRowFormatter(new RowSettings
      {
        LineTerminator = Environment.NewLine
      });

      _fieldFormatters = makeFieldFormatters(fieldLookup);
    }

    /// <summary>
    /// Checks if this settings provider specifies settings
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to check</param>
    /// <returns>True if this provider handles <paramref name="theType"/>;
    /// false otherwise</returns>
    public bool RecognizesType(Type theType)
    {
      return theType == _theType;
    }

    /// <summary>
    /// Gets the row formatter from this provider
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to get a formatter for</param>
    /// <returns>The found formatter</returns>
    public IRowFormatter GetRowFormatter(Type theType)
    {
      return _rowFormatter;
    }

    /// <summary>
    /// Gets the field formatters from this provider
    /// for <paramref name="theType"/>
    /// </summary>
    /// <param name="theType">The type to get a formatter for</param>
    /// <returns>The found formatters</returns>
    public IFieldFormatter[] GetFieldFormatters(Type theType)
    {
      return _fieldFormatters;
    }

    /// <summary>
    /// Creates an array of field formatters based on the settings in
    /// <paramref name="fieldLookup"/>
    /// </summary>
    /// <param name="fieldLookup">The settings to use for the specified fields</param>
    /// <returns>The collection of formatters</returns>
    private IFieldFormatter[] makeFieldFormatters(IDictionary<string, FieldSettings> fieldLookup)
    {
      var props = _theType.GetProperties().Where(pi => pi.CanRead).ToArray();

      verifyFields(props, fieldLookup);

      var retVal = (from fieldPair in fieldLookup
                    join p in props on fieldPair.Key.ToLower()
                     equals p.Name.ToLower()
                    orderby fieldPair.Value.Order
                    select new FixedWidthFieldFormatter(
                      new PropertyFieldReader(p), fieldPair.Value))
                           .ToArray();

      return retVal;
    }

    /// <summary>
    /// Checks to see if there are any fields in <paramref name="fieldLookup"/>
    /// that aren't found on the type T
    /// </summary>
    /// <param name="props">The properties from T</param>
    /// <param name="fieldLookup">The settings to use for the specified fields</param>
    private void verifyFields(PropertyInfo[] props, IDictionary<string, FieldSettings> fieldLookup)
    {
      // left outer join in LINQ...
      var missingProps = (from fieldPair in fieldLookup
                          join p in props on fieldPair.Key.ToLower()
                           equals p.Name.ToLower() into gj
                          from g in gj.DefaultIfEmpty()
                          where g == null
                          select fieldPair.Key).ToArray();

      if (missingProps.Any())
      {
        throw new MissingFieldsException(missingProps);
      }
    }
  }
}
