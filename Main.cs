using static Data.Database;
using System;
using static Utils.CmdUtils;

public abstract class Program {
  public static bool closeApp = false;

  static void Main(string[] args) {
    Console.WriteLine("Thank you for using JSON searcher!");
    ImportEntitiesFromJson();

    while(!closeApp) {
      ExecuteCommand(Console.ReadLine());
    }
  }
}