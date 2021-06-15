using static Data.Database;
using static Data.DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.IO.Directory;
using static Utils.Constants;
using static Utils.FileUtils;
using static Program;
using static System.String;
using static System.ConsoleColor;

namespace Utils {
    ///<summary>CmdUtils manages methods that controls the inputs and outputs from the console.</summary>
    public static class CmdUtils {
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

        [Description("Commands dictionary that maps commands with their descriptions and actions."),Category("Cmd")]
        //The actions always contain 1 input is to accommodate SearchItems().
        public static Dictionary<string, KeyValuePair<string, Action<string[]>>> commands = new Dictionary<string, KeyValuePair<string, Action<string[]>>>() {
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
                (string[] input) => ImportEntitiesFromJson(GetAllJsonFilepaths()))},
            {CMD_SEARCH, new KeyValuePair<string, Action<string[]>>(
                "search table field value [field value]... Searches a table with at least 1 field with value as filter",
                (string[] input) => SearchItems(input))}
        };


        [Description("Performs respective actions based on user input"),Category("Cmd")]
        public static void ExecuteCommand(string input) {
            if(!IsNullOrWhiteSpace(input)) {
                string[] input_blocks = input.Split(' ');
                commands[input_blocks[0].ToLower()].Value.Invoke(input_blocks);
            }
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

        [Description("Displays the command dictionary above to user via console."),Category("Cmd")]
        public static void DisplayHelpCommand() {
            foreach(var cmd in commands) {
                Console.WriteLine("{0, -50} {1}", cmd.Key, cmd.Value);
            }
        }
    }
}