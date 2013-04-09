using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText.Concrete;

namespace TextPowerPack.Tests.FlatText
{
  [DelimitedRow]
  class MyDefaultDelimitedTestClass
  {
    public string StringProp { get; set; }

    [DelimitedField]
    public int IntProp { get; set; }

    public decimal DecimalProp { get; set; }

    [DelimitedField]
    public double DoubleProp { get; set; }
  }

  [FixedWidthRow]
  class MyDefaultFixedWidthTestClass
  {
    [FixedWidthField]
    public string StringProp { get; set; }

    [FixedWidthField]
    public int IntProp { get; set; }

    public decimal DecimalProp { get; set; }

    [FixedWidthField]
    public double DoubleProp { get; set; }
  }

  [TestClass]
  public class AttributeSettingsProviderTests
  {
    [TestMethod]
    public void Default_Delimited_Attribute_Recognizes_Valid_Type()
    {
      // arrange - setup any data for the test here
      var provider = new AttributeSettingsProvider<DelimitedRowAttribute>();

      // act - call code under test here
      var actual = provider.RecognizesType(typeof(MyDefaultDelimitedTestClass));

      // assert - assert the correct test results here
      Assert.IsTrue(actual);
    }

    [TestMethod]
    public void Default_Delimited_Attribute_Fails_To_Recognize_Invalid_Type()
    {
      // arrange - setup any data for the test here
      var provider = new AttributeSettingsProvider<DelimitedRowAttribute>();

      // act - call code under test here
      var actual = provider.RecognizesType(typeof(MyDefaultFixedWidthTestClass));

      // assert - assert the correct test results here
      Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Default_Delimited_Attribute_Returns_Correct_Row_Formatter()
    {
      // arrange - setup any data for the test here
      var provider = new AttributeSettingsProvider<DelimitedRowAttribute>();

      // act - call code under test here
      var actual = provider.GetRowFormatter(typeof(MyDefaultDelimitedTestClass));

      // assert - assert the correct test results here
      Assert.AreEqual(',', actual.Settings.Delimiter);
      Assert.AreEqual(Environment.NewLine, actual.Settings.LineTerminator);
    }

    [TestMethod]
    public void Default_Delimited_Attribute_Returns_Correct_Field_Formatters()
    {
      // arrange - setup any data for the test here
      var provider = new AttributeSettingsProvider<DelimitedRowAttribute>();

      // act - call code under test here
      var actual = provider.GetFieldFormatters(typeof(MyDefaultDelimitedTestClass));

      // assert - assert the correct test results here
      Assert.AreEqual(2, actual.Length);
      Assert.AreEqual("IntProp", actual[0].FieldInfo.Name);
      Assert.AreEqual("G", actual[0].Settings.Format);
      Assert.AreEqual(FieldJustification.Right, actual[0].Settings.Justification);
      Assert.AreEqual(0, actual[0].Settings.Order);
      Assert.AreEqual(' ', actual[0].Settings.PaddingChar);
      Assert.AreEqual(0, actual[0].Settings.Width);
      Assert.AreEqual("DoubleProp", actual[1].FieldInfo.Name);
      Assert.AreEqual("G", actual[1].Settings.Format);
      Assert.AreEqual(FieldJustification.Right, actual[1].Settings.Justification);
      Assert.AreEqual(0, actual[1].Settings.Order);
      Assert.AreEqual(' ', actual[1].Settings.PaddingChar);
      Assert.AreEqual(0, actual[1].Settings.Width);
    }

    [TestMethod]
    public void Default_Fixed_Width_Attribute_Returns_Correct_Field_Formatters()
    {
      // arrange - setup any data for the test here
      var provider = new AttributeSettingsProvider<FlatTextRowAttribute>();

      // act - call code under test here
      var actual = provider.GetFieldFormatters(typeof(MyDefaultFixedWidthTestClass));

      // assert - assert the correct test results here
      Assert.AreEqual(3, actual.Length);
      Assert.AreEqual("StringProp", actual[0].FieldInfo.Name);
      Assert.AreEqual("G", actual[0].Settings.Format);
      Assert.AreEqual(FieldJustification.Right, actual[0].Settings.Justification);
      Assert.AreEqual(0, actual[0].Settings.Order);
      Assert.AreEqual(' ', actual[0].Settings.PaddingChar);
      Assert.AreEqual(0, actual[0].Settings.Width);
      Assert.AreEqual("IntProp", actual[1].FieldInfo.Name);
      Assert.AreEqual("G", actual[1].Settings.Format);
      Assert.AreEqual(FieldJustification.Right, actual[1].Settings.Justification);
      Assert.AreEqual(0, actual[1].Settings.Order);
      Assert.AreEqual(' ', actual[1].Settings.PaddingChar);
      Assert.AreEqual(0, actual[1].Settings.Width);
      Assert.AreEqual("DoubleProp", actual[2].FieldInfo.Name);
      Assert.AreEqual("G", actual[2].Settings.Format);
      Assert.AreEqual(FieldJustification.Right, actual[2].Settings.Justification);
      Assert.AreEqual(0, actual[2].Settings.Order);
      Assert.AreEqual(' ', actual[2].Settings.PaddingChar);
      Assert.AreEqual(0, actual[2].Settings.Width);
    }
  }
}
