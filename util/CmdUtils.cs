using static Data.Database;
using static Data.DataSearcher;
using System;
using System.Collections.Generic;
using static Utils.Constants;
using static System.String;
using static Utils.ConsoleUtils;
using static System.StringComparer;
using static Utils.Config;
using System.Linq;
using static Utils.StringUtils;

namespace Utils {
  ///<summary>CmdUtils manages custom commands in this application.</summary>
  public static class CmdUtils {
    //A commands dictionary that maps command keys, descriptions, and functinalities respectively.
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
          "search table [field value]... Searches a table with custom filters.",
          (string[] input) => ValidateAndReturnSearchResults(input))}
    };

    public static void ExecuteCommand(string input) {
      if(!IsNullOrWhiteSpace(input)) {
        string[] input_blocks = input.Split(' ').Select(input => ParseEmptyIdentifier(input)).ToArray();
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

    internal static void DisplayHelpCommand() {
      foreach(var cmd in commands) {
        OutputToConsole(Format("{0, -20} {1}", cmd.Key, cmd.Value.Key));
      }
    }
  }
}