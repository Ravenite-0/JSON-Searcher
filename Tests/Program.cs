using System;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Utils.ConsoleUtils;

namespace Tests {
  abstract class UnitTest {
    [TestFixture]
    public abstract class ConsoleUtilsTest {
      [Test]
      public void Test_SetConsoleTextColor() {
        SetConsoleTextColor(ConsoleColor.Black);
        AreEqual(ConsoleColor.Black, Console.BackgroundColor.GetType());
      }
    }

  }
}
