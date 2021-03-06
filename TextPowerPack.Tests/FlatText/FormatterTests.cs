﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextPowerPack.FlatText.Concrete;
using TextPowerPack.FlatText.Common;
using Moq;
using TextPowerPack.FlatText.Interfaces;
using System.Globalization;
using System.Threading;
using TextPowerPack.FlatText.Exceptions;

namespace TextPowerPack.Tests.FlatText
{
  [TestClass]
  public class FormatterTests
  {
    [TestMethod]
    public void SimpleRowFormatter_Returns_Correctly()
    {
      // arrange - setup any data for the test here
      var fmtter = new SimpleRowFormatter(new RowSettings());

      var values = new string[] { "one", "two", "three" };
      var expected = "onetwothree";

      // act - call code under test here
      var actual = fmtter.FormatRow(values);

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DelimitedRowFormatter_Returns_Correctly()
    {
      // arrange - setup any data for the test here
      var fmtter = new DelimitedRowFormatter(new RowSettings
      {
        Delimiter = '|'
      });

      var values = new string[] { "one", "two", "three" };
      var expected = "one|two|three";

      // act - call code under test here
      var actual = fmtter.FormatRow(values);

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SimpleFieldFormatter_With_General_Format_Returns_Correctly()
    {
      // arrange - setup any data for the test here
      var mockFieldReader = new Mock<IFieldReader>();
      mockFieldReader.Setup(fr => fr.GetValue(It.IsAny<object>(), null)).Returns(42);

      var fmtter = new SimpleFieldFormatter(mockFieldReader.Object, new FieldSettings
      {
        Format = "G"
      });

      var expected = "42";

      // act - call code under test here
      var actual = fmtter.FormatField(new object());

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SimpleFieldFormatter_With_Currency_Format_Returns_Correctly()
    {
      // arrange - setup any data for the test here

      // the expected result is dependent on the culture so make sure test always uses
      // US culture to avoid test failures under different cultures
      var culture = Thread.CurrentThread.CurrentCulture;
      Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

      var mockFieldReader = new Mock<IFieldReader>();
      mockFieldReader.Setup(fr => fr.GetValue(It.IsAny<object>(), null)).Returns(42);

      var fmtter = new SimpleFieldFormatter(mockFieldReader.Object, new FieldSettings
      {
        Format = "C"
      });

      var expected = "$42.00";

      // act - call code under test here
      var actual = fmtter.FormatField(new object());

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);

      Thread.CurrentThread.CurrentCulture = culture;
    }

    [TestMethod]
    [ExpectedException(typeof(FieldOverflowException))]
    public void Fixed_Width_Formatter_Throws_On_Field_Overthrow()
    {
      // arrange - setup any data for the test here
      var mockFieldReader = new Mock<IFieldReader>();
      mockFieldReader.Setup(fr => fr.GetValue(It.IsAny<object>(), null)).Returns("hello");
      mockFieldReader.Setup(fr => fr.Name).Returns("TestField");

      var fmtter = new FixedWidthFieldFormatter(mockFieldReader.Object, new FieldSettings
      {
        Format = "G",
        Justification = FieldJustification.Left,
        ThrowOnOverflow = true,
        Width = 3
      });

      // act - call code under test here
      var actual = fmtter.FormatField(new object());

      // assert - assert the correct test results here
      // no asserts since we expect exception
    }

    [TestMethod]
    public void Fixed_Width_Formatter_Right_Truncates_On_Field_Overthrow()
    {
      // arrange - setup any data for the test here
      var mockFieldReader = new Mock<IFieldReader>();
      mockFieldReader.Setup(fr => fr.GetValue(It.IsAny<object>(), null)).Returns("hello");
      
      var fmtter = new FixedWidthFieldFormatter(mockFieldReader.Object, new FieldSettings
      {
        Format = "G",
        Justification = FieldJustification.Left,
        ThrowOnOverflow = false,
        Width = 3
      });

      var expected = "hel";

      // act - call code under test here
      var actual = fmtter.FormatField(new object());

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Fixed_Width_Formatter_Left_Truncates_On_Field_Overthrow()
    {
      // arrange - setup any data for the test here
      var mockFieldReader = new Mock<IFieldReader>();
      mockFieldReader.Setup(fr => fr.GetValue(It.IsAny<object>(), null)).Returns("hello");
      mockFieldReader.Setup(fr => fr.Name).Returns("TestField");

      var fmtter = new FixedWidthFieldFormatter(mockFieldReader.Object, new FieldSettings
      {
        Format = "G",
        Justification = FieldJustification.Right,
        ThrowOnOverflow = false,
        Width = 3
      });

      var expected = "llo";

      // act - call code under test here
      var actual = fmtter.FormatField(new object());

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);
    }
  }
}
