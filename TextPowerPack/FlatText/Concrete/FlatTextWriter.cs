using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextPowerPack.FlatText.Interfaces;

namespace TextPowerPack.FlatText.Concrete
{
  /// <summary>
  /// Represents an object that writes flat-text rows consisting of fields
  /// </summary>
  /// <typeparam name="TRow">The type containing the fields that
  /// make up the rows</typeparam>
  public class FlatTextWriter<TRow> : IFlatTextWriter<TRow>
  {
    protected IRowFormatter rowFormatter { get; set; }

    protected IFieldFormatter[] fieldFormatters { get; set; }

    public TextWriter BackingWriter { get; private set; }

    public FlatTextWriter(TextWriter backingWriter, IRowFormatter rowFormatter, 
      IFieldFormatter[] fieldFormatters)
    {
      this.BackingWriter = backingWriter;
      this.rowFormatter = rowFormatter;
      this.fieldFormatters = fieldFormatters;
    }

    /// <summary>
    /// Writes the flat-text row to some backing stream
    /// using the values from <paramref name="instance"/>
    /// </summary>
    /// <param name="instance">The instance to write values from</param>
    public void Write(TRow instance)
    {
      string row = makeRowForInstance(instance);

      // output the complete row
      BackingWriter.Write(row);
    }

    /// <summary>
    /// Writes the flat-text row to some backing stream
    /// using the values from <paramref name="instance"/>
    /// followed by a line terminator
    /// </summary>
    /// <param name="instance">The instance to write values from</param>
    public void WriteLine(TRow instance)
    {
      // write out single row...
      this.Write(instance);

      // ... then add terminator
      BackingWriter.Write(rowFormatter.Settings.LineTerminator);
    }

    private string makeRowForInstance(TRow instance)
    {
      // project each field value to a string
      var fieldValues = fieldFormatters.Select(ff => ff.FormatField(instance));

      // combine fields into a complete row
      var row = rowFormatter.FormatRow(fieldValues);

      return row;
    }
  }
}
