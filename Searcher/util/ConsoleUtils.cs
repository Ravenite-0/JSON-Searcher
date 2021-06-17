using System;
using static Utils.Constants;
using static System.String;
using static Utils.SysUtils;
using System.Linq;
using static System.Environment;
using static Utils.StringUtils;
using static Utils.Config;

namespace Utils {
  ///<summary>ConsoleUtils manages output to console methods.</summary>
  public static class ConsoleUtils {

    public static void SetConsoleTextColor(ConsoleColor color) =>
      Console.ForegroundColor = color;

    public static void OutputToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.White);
      Output.WriteLine(str);
    }

    public static void OutputWarningToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.Yellow);
      Output.WriteLine(str);
    }

    public static void OutputPassToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.Green);
      Output.WriteLine(str);
    }

    public static void OutputExceptionToConsole(Exception e, string customString = "", bool showSystemException = true) {
      SetConsoleTextColor(ConsoleColor.Red);
      Output.WriteLine(((debugMode) ? e.ToString() : 
                        ((showSystemException) ? customString + NewLine + e.Message : customString)));
    }
    
    public static void OutputSeparatorsToConsole(string str) {
      SetConsoleTextColor(ConsoleColor.Cyan);
      Output.WriteLine(str);
    }

    public static void OutputEntity(dynamic entity) {
      foreach(var property in entity.GetType().GetProperties()) {
        OutputToConsole(Format("{0, -20} -> {1}", ToStringIncNull(property.Name), 
          (IsObjectStringList(property)) ?
            Join(',', Enumerable.ToList<string>(property.GetValue(entity))) :
            ToStringIncNull(property.GetValue(entity))));
      }
      OutputToConsole(OUTPUT_SMALL_LINESPLIT);
    }

    public static void OutputTypeFields(Type p, string entityName) {
      OutputToConsole($"Getting fields from {entityName}:");
      var test = p.GetProperties();
      foreach(var field in p.GetProperties()) {
        OutputToConsole(ToStringIncNull(field.Name));
      }
      OutputToConsole(OUTPUT_LARGE_LINESPLIT);
    }
  }
}