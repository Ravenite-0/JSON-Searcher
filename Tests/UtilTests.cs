using NUnit.Framework;
using System;
using System.Reflection;
using System.Collections.Generic;
using static Utils.ConsoleUtils;
using static Utils.FileUtils;
using static Utils.StringUtils;
using static Utils.SysUtils;

namespace Tests {
  public class UnitTest {
    [TestFixture]
    public class ConsoleUtilsTest {
      [Test]
      public void Test_SetConsoleTextColor() {
        Assert.AreNotEqual(ConsoleColor.Black, Console.BackgroundColor);
        SetConsoleTextColor(ConsoleColor.Black);
        Assert.AreEqual(ConsoleColor.Black, Console.BackgroundColor);
      }
    }

    [TestFixture]
    public class FileUtilsTest {
      [Test]
      public void Test_GetChildDir() =>
        StringAssert.EndsWith("JSON-Searcher\\Tests\\bin", GetChildDir("bin"));
    }

    [TestFixture]
    public class StringUtilsTest {
      [Test]
      public void Test_ContainsIgnoreCase() =>
        Assert.IsTrue(ContainsIgnoreCase("AbCdEfG", "aBcDeFg"));

      [Test]
      public void Test_ToStringIncNull() {
        StringAssert.AreEqualIgnoringCase("", ToStringIncNull(null));
        StringAssert.AreEqualIgnoringCase("abc", ToStringIncNull("abc"));
      }
      
      [Test]
      public void Test_ParseToTableName() =>
        StringAssert.AreEqualIgnoringCase("test.json", "test".ParseToTableName());
      
      [Test]
      public void Test_RemoveTableName() =>
        StringAssert.AreEqualIgnoringCase("test", "test.json".RemoveTableName());
      
      [Test]
      public void Test_GetParseFileResults() =>
        StringAssert.AreEqualIgnoringCase("Imported: 6 -- Failed: 4", GetParseFileResults(6, 10));
      
      [Test]
      public void Test_ParseEmptyIdentifier() {
        StringAssert.AreEqualIgnoringCase("", ParseEmptyIdentifier("%"));
        StringAssert.AreEqualIgnoringCase("abc", ParseEmptyIdentifier("abc"));
      }

      [Test]
      public void Test_ParseDateTimeToString() =>
        StringAssert.AreEqualIgnoringCase("2020-12-27_8-30-52-111",ParseDateTimeToString(new DateTime(2020,12,27,8,30,52,111)));
    }

    [TestFixture]
    public class SysUtilsTests {
      public class TestObject {
        public List<string> list {get; set;}
        public DateTime dt {get; set;}
        public TestObject(List<string> list, DateTime dt) {
          this.list = list;
          this.dt = dt;
        }
      }

      public static TestObject testObj = new TestObject(new List<string>() {""}, new DateTime(2020, 1, 1));

      [Test]
      public void Test_IsObjectStringList() =>
        Assert.IsTrue(IsObjectStringList(testObj.GetType().GetProperty("list")));

      [Test]
      public void Test_IsIsObjectDateTime() =>
        Assert.IsTrue(IsObjectDateTime(testObj.GetType().GetProperty("dt")));

      [Test]
      public void Test_GetPropertyFromEntity() =>
        Assert.AreEqual(testObj.GetType().GetProperty("dt"), GetPropertyFromEntity(testObj, "dt"));

      [Test]
      public void Test_GetValueFromEntityProperty() =>
        Assert.AreEqual(new DateTime(2020, 1, 1), GetValueFromEntityProperty(testObj, "dt"));

      [Test]
      public void Test_RoundDownDate() =>
        Assert.AreEqual(new DateTime(2020, 1, 1), RoundDownDate(new DateTime(2020, 1, 1, 23, 59, 59 ,999)));
    }
  }
}
