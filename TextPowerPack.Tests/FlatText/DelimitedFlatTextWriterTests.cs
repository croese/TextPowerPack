using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextPowerPack.FlatText;
using TextPowerPack.FlatText.Concrete;
using TextPowerPack.FlatText.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TextPowerPack.FlatText.Common;

namespace TextPowerPack.Tests.FlatText
{
  class MyTestClass
  {
    public string StringProp { get; set; }

    public int IntProp { get; set; }

    public decimal DecimalProp { get; set; }

    public double DoubleProp { get; set; }
  }

  [TestClass]
  public class DelimitedFlatTextWriterTests
  {
    IRowFormatter getCommaDelimitedRowFormatter()
    {
      var s = new RowSettings
      {
        Delimiter = ',',
        LineTerminator = Environment.NewLine
      };

      var mock = new Mock<IRowFormatter>();
      mock.Setup(f => f.Settings).Returns(s);
      mock.Setup(f => f.FormatRow(It.IsAny<IEnumerable<string>>())).Returns<IEnumerable<string>>(fs => string.Join(s.Delimiter.ToString(), fs));

      return mock.Object;
    }

    IFieldFormatter[] getFieldFormatters()
    {
      var stringPropMock = new Mock<IFieldFormatter>();
      stringPropMock.Setup(ff => ff.FormatField(It.IsAny<MyTestClass>())).Returns<MyTestClass>(tc => tc.StringProp);

      var intPropMock = new Mock<IFieldFormatter>();
      intPropMock.Setup(ff => ff.FormatField(It.IsAny<MyTestClass>())).Returns<MyTestClass>(tc => tc.IntProp.ToString());

      var decimalPropMock = new Mock<IFieldFormatter>();
      decimalPropMock.Setup(ff => ff.FormatField(It.IsAny<MyTestClass>())).Returns<MyTestClass>(tc => tc.DecimalProp.ToString());

      return new IFieldFormatter[] 
      {
        stringPropMock.Object,
        intPropMock.Object,
        decimalPropMock.Object
      };
    }

    [TestMethod]
    public void Delimited_Writer_With_Fake_Formatters_Writes_Correct_Line()
    {
      // arrange - setup any data for the test here
      var sw = new StringWriter();
      var ftw = new FlatTextWriter<MyTestClass>(sw, this.getCommaDelimitedRowFormatter(), this.getFieldFormatters());
      var inst = new MyTestClass
      {
        StringProp = "hello world",
        IntProp = 42,
        DecimalProp = 3.14159M
      };

      var expected = "hello world,42,3.14159";

      // act - call code under test here
      ftw.Write(inst);

      // assert - assert the correct test results here
      Assert.AreEqual(expected, sw.ToString());
    }
  }
}
