using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TextPowerPack.FlatText.Concrete;
using System.Collections.Generic;
using TextPowerPack.FlatText.Common;
using TextPowerPack.FlatText;

namespace TextPowerPack.Tests.FlatText
{
  [TestClass]
  public class FixedWidthFlatTextWriterTests
  {
    [TestMethod]
    public void Fixed_Width_Settings_Produce_Correct_Output()
    {
      // arrange - setup any data for the test here
      var sw = new StringWriter();
      var fwsp = new FixedWidthSettingsProvider<MyTestClass>(new Dictionary<string, FieldSettings>
      {
        { "StringProp", 
            new FieldSettings() 
            { 
              Format = "G", 
              Justification = FieldJustification.Left,
              Order = 2,
              PaddingChar = 'X',
              Width = 10
            }
          },
          { "IntProp", 
            new FieldSettings()
            {
              Format = "G", 
              Justification = FieldJustification.Right,
              Order = 1,
              PaddingChar = '0',
              Width = 5
            }
          }
      });

      FlatTextWriterFactory.RegisterSettingsProvider(fwsp);
      var ftw = FlatTextWriterFactory.Create<MyTestClass>(sw);
      var expected = "00042helloXXXXX";

      // act - call code under test here
      ftw.Write(new MyTestClass()
      {
        StringProp = "hello",
        IntProp = 42
      });

      var actual = sw.ToString();

      // assert - assert the correct test results here
      Assert.AreEqual(expected, actual);
    }
  }
}
