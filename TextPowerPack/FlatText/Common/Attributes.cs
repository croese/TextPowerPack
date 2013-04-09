using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using TextPowerPack.FlatText.Concrete;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Common
{
  /// <summary>
  /// Represents the base class for any flat-text row attributes
  /// </summary>
  public abstract class FlatTextRowAttribute : Attribute
  {
    /// <summary>
    /// The current row settings that this attribute will return
    /// </summary>
    private RowSettings _rowSettings = new RowSettings
    {
      Delimiter = ',',
      LineTerminator = Environment.NewLine
    };

    /// <summary>
    /// The field delimiter - wrapper prop for subclasses to use
    /// </summary>
    protected char delimiter
    {
      get
      {
        return _rowSettings.Delimiter;
      }
      set
      {
        _rowSettings.Delimiter = value;
      }
    }

    /// <summary>
    /// The current row settings that this attribute will return -
    /// wrapper prop for subclasses to use
    /// </summary>
    protected RowSettings rowSettings
    {
      get
      {
        return _rowSettings;
      }
      set
      {
        _rowSettings = value;
      }
    }

    /// <summary>
    /// The line terminator for a row of text
    /// </summary>
    public string LineTerminator
    {
      get
      {
        return _rowSettings.LineTerminator;
      }
      set
      {
        _rowSettings.LineTerminator = value;
      }
    }

    /// <summary>
    /// The attribute type that is used in marking fields to be output
    /// </summary>
    public Type FieldAttributeType { get; private set; }

    /// <summary>
    /// Creates a new FlatTextRowAttribute with the specified field attribute type
    /// </summary>
    /// <param name="fieldAttribType">The attribute type that is used in marking fields to be output</param>
    public FlatTextRowAttribute(Type fieldAttribType)
    {
      Contract.Requires(fieldAttribType != null);
      Contract.Requires(fieldAttribType.IsSubclassOf(typeof(FlatTextFieldAttribute)),
        "fieldAttribType must be a subclass of FlatTextFieldAttribute");

      this.FieldAttributeType = fieldAttribType;
    }

    /// <summary>
    /// Uses the row settings from this class to construct a row formatter
    /// </summary>
    /// <returns>The appropriate row formatter</returns>
    public abstract IRowFormatter GetRowFormatter();
  }

  /// <summary>
  /// An attribute to tag classes that should act as delimited flat-text rows
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
  public sealed class DelimitedRowAttribute : FlatTextRowAttribute
  {
    /// <summary>
    /// The field delimiter
    /// </summary>
    public char Delimiter
    {
      get
      {
        return delimiter;
      }
      set
      {
        delimiter = value;
      }
    }

    /// <summary>
    /// Creates a new DelimitedRowAttribute with the DelimitedFieldAttribute
    /// as the field attribute type
    /// </summary>
    public DelimitedRowAttribute()
      : base(typeof(DelimitedFieldAttribute))
    {
    }

    /// <summary>
    /// Uses the row settings from this class to construct a row formatter
    /// </summary>
    /// <returns>The appropriate row formatter</returns>
    public override IRowFormatter GetRowFormatter()
    {
      return new DelimitedRowFormatter(rowSettings);
    }
  }

  /// <summary>
  /// An attribute to tag classes that should act as fixed-width flat-text rows
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
  public sealed class FixedWidthRowAttribute : FlatTextRowAttribute
  {
    /// <summary>
    /// Creates a new FixedWidthRowAttribute with the FixedWidthFieldAttribute
    /// as the field attribute type
    /// </summary>
    public FixedWidthRowAttribute()
      : base(typeof(FixedWidthFieldAttribute))
    {

    }

    /// <summary>
    /// Uses the row settings from this class to construct a row formatter
    /// </summary>
    /// <returns>The appropriate row formatter</returns>
    public override IRowFormatter GetRowFormatter()
    {
      return new SimpleRowFormatter(rowSettings);
    }
  }

  /// <summary>
  /// Represents the base class for any flat-text field attributes
  /// </summary>
  [ContractClass(typeof(FlatTextFieldAttributeContract))]
  public abstract class FlatTextFieldAttribute : Attribute
  {
    /// <summary>
    /// The current field settings that this attribute will return
    /// </summary>
    private FieldSettings _fieldSettings = new FieldSettings
    {
      Format = "G",
      Justification = FieldJustification.Right,
      Order = 0,
      PaddingChar = ' ',
      Width = 0
    };

    /// <summary>
    /// The alignment of the value in its field -
    /// wrapper prop for subclasses to use
    /// </summary>
    protected FieldJustification justification
    {
      get
      {
        return _fieldSettings.Justification;
      }
      set
      {
        _fieldSettings.Justification = value;
      }
    }

    /// <summary>
    /// The padding character to fill the field up to its width -
    /// wrapper prop for subclasses to use
    /// </summary>
    protected char paddingChar
    {
      get
      {
        return _fieldSettings.PaddingChar;
      }
      set
      {
        _fieldSettings.PaddingChar = value;
      }
    }

    /// <summary>
    /// The width of the field -
    /// wrapper prop for subclasses to use
    /// </summary>
    protected int width
    {
      get
      {
        return _fieldSettings.Width;
      }
      set
      {
        _fieldSettings.Width = value;
      }
    }

    /// <summary>
    /// The current field settings that this attribute will return
    /// </summary>
    protected FieldSettings fieldSettings
    {
      get
      {
        return _fieldSettings;
      }
      set
      {
        _fieldSettings = value;
      }
    }

    /// <summary>
    /// The formatting directive string to use to stringify the field value
    /// </summary>
    public string Format
    {
      get
      {
        return _fieldSettings.Format;
      }
      set
      {
        _fieldSettings.Format = value;
      }
    }

    /// <summary>
    /// The order that this field should appear in the output
    /// </summary>
    public int Order
    {
      get
      {
        return _fieldSettings.Order;
      }
      set
      {
        _fieldSettings.Order = value;
      }
    }

    /// <summary>
    /// Uses <paramref name="prop"/> and the field settings to construct
    /// a field formatter
    /// </summary>
    /// <param name="prop">The PropertyInfo for the property 
    /// tagged with this attribute</param>
    /// <returns>The appropriate field formatter</returns>
    public abstract IFieldFormatter GetFieldFormatter(PropertyInfo prop);
  }

  [ContractClassFor(typeof(FlatTextFieldAttribute))]
  abstract class FlatTextFieldAttributeContract : FlatTextFieldAttribute
  {

    public override IFieldFormatter GetFieldFormatter(PropertyInfo prop)
    {
      Contract.Requires(prop != null);
      return default(IFieldFormatter);
    }
  }

  /// <summary>
  /// An attribute to tag delimited flat-text fields
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
  public sealed class DelimitedFieldAttribute : FlatTextFieldAttribute
  {
    /// <summary>
    /// Uses <paramref name="prop"/> and the field settings to construct
    /// a field formatter
    /// </summary>
    /// <param name="prop">The PropertyInfo for the property 
    /// tagged with this attribute</param>
    /// <returns>The appropriate field formatter</returns>
    public override IFieldFormatter GetFieldFormatter(PropertyInfo prop)
    {
      return new SimpleFieldFormatter(new PropertyFieldReader(prop), fieldSettings);
    }
  }

  /// <summary>
  /// An attribute to tag fixed-width flat-text fields
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
  public sealed class FixedWidthFieldAttribute : FlatTextFieldAttribute
  {
    /// <summary>
    /// The alignment of the value in its field
    /// </summary>
    public FieldJustification Justification
    {
      get
      {
        return justification;
      }
      set
      {
        justification = value;
      }
    }

    /// <summary>
    /// The padding character to fill the field up to its width
    /// </summary>
    public char PaddingChar
    {
      get
      {
        return paddingChar;
      }
      set
      {
        paddingChar = value;
      }
    }

    /// <summary>
    /// The width of the field
    /// </summary>
    public int Width
    {
      get
      {
        return width;
      }
      set
      {
        width = value;
      }
    }

    /// <summary>
    /// Uses <paramref name="prop"/> and the field settings to construct
    /// a field formatter
    /// </summary>
    /// <param name="prop">The PropertyInfo for the property 
    /// tagged with this attribute</param>
    /// <returns>The appropriate field formatter</returns>
    public override IFieldFormatter GetFieldFormatter(PropertyInfo prop)
    {
      return new FixedWidthFieldFormatter(new PropertyFieldReader(prop), fieldSettings);
    }
  }
}
