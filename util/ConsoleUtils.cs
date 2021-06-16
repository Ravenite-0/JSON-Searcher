using System;
using static Utils.Constants;
using static System.String;
using static Utils.SysUtils;
using System.Linq;
using static Utils.StringUtils;

namespace Utils {
  ///<summary>ConsoleUtils manages interactions (I/O) related methods.</summary>
  public static class ConsoleUtils {
    internal static void SetConsoleTextColor(ConsoleColor color) =>
      Console.ForegroundColor = color;

    public static void OutputToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.White);
      Console.WriteLine(str);
    }

    public static void OutputWarningToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.Yellow);
      Console.WriteLine(str);
    }

    public static void OutputPassToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.Green);
      Console.WriteLine(str);
    }

    public static void OutputExceptionToConsole(Exception e, string customString = "", bool showSystemException = true) {
      SetConsoleTextColor(ConsoleColor.Red);
      //Console.WriteLine((showSystemException) ? customString + NewLine + e.Message : customString);
      //For debugging only.
      Console.WriteLine(e);
    }
    
    public static void OutputSeparatorsToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.Cyan);
      Console.WriteLine(str);
    }

    public static void OutputEntity(dynamic entity) {
      foreach(var property in entity.GetType().GetProperties()) {
        OutputToConsole(Format("{0, -20} -> {1}", (property.Name ?? "").ToString(), 
          (IsObjectStringList(property)) ?
            Join(',', Enumerable.ToList<string>(property.GetValue(entity))) :
            ToStringIncNull(property.GetValue(entity))));
      }
      OutputToConsole(OUTPUT_SMALL_LINESPLIT);
    }
  }
}