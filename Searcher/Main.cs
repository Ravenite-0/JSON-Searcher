using static Data.Database;
using System;
using static Utils.CmdUtils;
using static Utils.ConsoleUtils;
using static Utils.Config;

public abstract class Program {

  static void Main(string[] args) {
    OutputToConsole("Thank you for using my JSON searcher!");
    ImportEntitiesFromJson();

    while(!closeApp) {
      OutputToConsole("");
      ExecuteCommand(Console.ReadLine());
      SetConsoleTextColor(ConsoleColor.White);
    }
  }
}