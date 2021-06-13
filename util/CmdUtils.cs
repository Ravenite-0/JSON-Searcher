using static Data.Data;
using static Data.ItemSearch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.IO.Directory;
using static Utils.Constants;
using static Utils.FileUtils;

namespace Utils {
    ///<summary>CmdUtils manages methods that controls the inputs and outputs from the console.</summary>
    public abstract class CmdUtils {

        [Description("Holds the list of commands and their respective descriptions."),Category("Cmd")]
        public static Dictionary<string, string> commands = new Dictionary<string, string>() {
            {CMD_HELP, "Shows the list of available commands and their functionality."},
            {CMD_CLEAR, "Clears the console window."},
            {CMD_EXIT, "Closes the application."},
            {CMD_RELOAD, "Reloads all files from the json folder."},
            {$"{CMD_SEARCH} table field value [field value]...", "Searches a specific table with at least a field that contains the given value."}
        };

        [Description("Handles user input and executes different methods accordingly."),Category("Cmd")]
        public static void HandleInput(string input) {
            string[] input_blocks = input.Split(' ');
            switch(input_blocks[0].ToLower()) {
                case CMD_HELP:
                    DisplayHelpCommand();
                    break;
                case CMD_CLEAR:
                    Console.Clear();
                    break;
                case CMD_EXIT:
                    Program.closeApp = true;
                    break;
                case CMD_RELOAD:
                    LoadFiles(GetFiles(GetChildDir("json")));
                    break;
                case CMD_SEARCH:
                    SearchItems(input_blocks);
                    break;
                default:
                    ThrowError($"Command {input_blocks[0]} not found. Type HELP for all the available commands.");
                    break;
            }
        }

        [Description("Displays exception message to user via console."),Category("Cmd")]
        public static void ThrowError(string errorMessage) {
            Console.WriteLine(errorMessage);
            Console.ReadLine();
        }

        [Description("Displays the command dictionary above to user via console."),Category("Cmd")]
        public static void DisplayHelpCommand() {
            foreach(var cmd in commands) {
                Console.WriteLine("{0, -50} {1}", cmd.Key, cmd.Value);
            }
        }
    }
}