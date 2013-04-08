using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TextPowerPack.FlatText;
using TextPowerPack.FlatText.Concrete;
using System.Collections.Generic;
using TextPowerPack.FlatText.Common;

namespace TextPowerPack.Tests.FlatText
{
  [TestClass]
  public class FactoryTests
  {
    class MyTestClass
    {
      public string StringProp { get; set; }

      public int IntProp { get; set; }

      public decimal DecimalProp { get; set; }

      public double DoubleProp { get; set; }
    }

    [TestMethod]
    public void Factory_Create_Returns_Default_Delimited_Writer()
    {
      // arrange - setup any data for the test here
      var sw = new StringWriter();
      var ftw = FlatTextWriterFactory.Create<MyTestClass>(sw);
      var inst = new MyTestClass
      {
        StringProp = "hello world",
        IntProp = 42,
        DecimalProp = 3.14159M
      };

      var expected = "hello world,42,3.14159,0";

      // act - call code under test here
      ftw.Write(inst);

      // assert - assert the correct test results here
      Assert.AreEqual(expected, sw.ToString());
    }

  }
}
