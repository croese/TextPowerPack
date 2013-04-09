using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextPowerPack.FlatText.Common;
using System.Linq;

namespace TextPowerPack.Tests.FlatText
{
  [TestClass]
  public class ExtensionMethodsTests
  {
    class MyAttrib : Attribute
    {
    }

    class OtherAttrib : Attribute
    {
    }

    [MyAttrib]
    public class MyClass
    {
      [MyAttrib]
      public int MyProp { get; set; }

      public string OtherProp { get; set; }
    }

    [TestMethod]
    public void HasCustomAttribute_Returns_True_For_Correctly_Tagged_Type()
    {
      // arrange - setup any data for the test here

      // act - call code under test here
      bool actual = typeof(MyClass).HasCustomAttribute<MyAttrib>();

      // assert - assert the correct test results here
      Assert.IsTrue(actual);
    }

    [TestMethod]
    public void HasCustomAttribute_Returns_False_For_Untagged_Type()
    {
      // arrange - setup any data for the test here

      // act - call code under test here
      bool actual = typeof(MyClass).HasCustomAttribute<OtherAttrib>();

      // assert - assert the correct test results here
      Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Correct_Number_Of_Tagged_Props_Are_Returned()
    {
      // arrange - setup any data for the test here

      // act - call code under test here
      var actual = typeof(MyClass).GetProperties().Where(x => x.HasCustomAttribute<MyAttrib>()).Count();

      // assert - assert the correct test results here
      Assert.AreEqual(1, actual);
    }
  }
}
