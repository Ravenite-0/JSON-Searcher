using static Data.Database;
using System;
using static Utils.CmdUtils;
using static Utils.ConsoleUtils;
public abstract class Program {
  public static bool closeApp = false;

  static void Main(string[] args) {
    OutputToConsole("Thank you for using my JSON searcher!");
    ImportEntitiesFromJson();

    while(!closeApp) {
      ExecuteCommand(Console.ReadLine());
    }
  }
}