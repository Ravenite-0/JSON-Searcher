using static Data.Database;
using static Data.DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static Utils.Constants;
using static Program;
using static System.String;
using static Utils.ConsoleUtils;
using static System.StringComparer;

namespace Utils {
  ///<summary>CmdUtils manages custom commands in this application.</summary>
  public static class CmdUtils {
    //The Action column of the commands dictionary always contain 1 input to accommodate SearchItems.
    public static Dictionary<string, KeyValuePair<string, Action<string[]>>> commands = 
      new Dictionary<string, KeyValuePair<string, Action<string[]>>>(OrdinalIgnoreCase) {
        {CMD_HELP, new KeyValuePair<string, Action<string[]>>(
          "Shows the list of available commands and their functionality.",
          (string[] input) => DisplayHelpCommand()) }, 
        {CMD_CLEAR, new KeyValuePair<string, Action<string[]>>(
          "Clears the console window.",
          (string[] input) => Console.Clear())},
        {CMD_EXIT, new KeyValuePair<string, Action<string[]>>(
          "Closes the application.",
          (string[] input) => closeApp = true)},
        {CMD_RELOAD, new KeyValuePair<string, Action<string[]>>(
          "Reloads all files from the json folder.",
          (string[] input) => ImportEntitiesFromJson())},
        {CMD_SEARCH, new KeyValuePair<string, Action<string[]>>(
          "search table field value [field value]... Searches a table with at least 1 field with value as filter",
          (string[] input) => ValidateSearch(input))}
    };

    public static void ExecuteCommand(string input) {
      if(!IsNullOrWhiteSpace(input)) {
        string[] input_blocks = input.Split(' ');
        try {
          commands[input_blocks[0]].Value.Invoke(input_blocks);
        } catch(Exception e){
          if(e is KeyNotFoundException) {
            OutputExceptionToConsole(e, $"Command {input_blocks[0]} not found:");
          } else {
            OutputExceptionToConsole(e, $"Oops! Looks like something went wrong with command operations:");
          }
        }
      }
      //No actions needed if input is empty/spaces.
    }

    [Description("Displays the command dictionary above to user via console."),Category("Cmd")]
    internal static void DisplayHelpCommand() {
      foreach(var cmd in commands) {
        OutputToConsole(Format("{0, -20} {1}", cmd.Key, cmd.Value.Key));
      }
    }
  }
}