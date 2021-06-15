using static Data.Database;
using static Data.DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static Utils.Constants;
using static Program;
using static System.String;

namespace Utils {
  ///<summary>ConsoleUtils manages interactions (I/O) related methods.</summary>
  public static class ConsoleUtils {
    public static void SetConsoleTextColor(ConsoleColor color) =>
      Console.ForegroundColor = color;

    public static void OutputToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.White);
      Console.WriteLine(str);
    }

    public static void OutputErrorToConsole(string err) {
      SetConsoleTextColor(ConsoleColor.Red);
      Console.WriteLine(err);
      SetConsoleTextColor(ConsoleColor.White);
    }

    public static void OutputWarningToConsole(string warning) {
      SetConsoleTextColor(ConsoleColor.Yellow);
      Console.WriteLine(warning);
      SetConsoleTextColor(ConsoleColor.White);
    }

    [Description("Displays exception message to user via console."),Category("Cmd")]
      public static void ThrowError(string err, Exception e = null) {
        OutputErrorToConsole(err);
        OutputWarningToConsole(e.Message.ToString());
      }

        [Description("Displays provided object's properties and respective values via console."),Category("Cmd")]
        public static void ToConsoleString(this object entity) {
            foreach(var property in entity.GetType().GetProperties()) {
                Console.WriteLine($"{property.Name.ToString()}  ->  {property.GetValue(entity).ToString()}");
            }
            Console.WriteLine("==================================================");
        }
  }
}