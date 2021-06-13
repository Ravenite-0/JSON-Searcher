using static Repo.ItemRepo;
using static Utils.FileUtils;
using System;
using static System.IO.Directory;
using static Utils.Constants;
using static Repo.ItemSearch;
using System.Collections.Generic;
using static Utils.SysUtils;


namespace Command {
    public abstract class Command {
        public static Dictionary<string, string> commands = new Dictionary<string, string>() {
            {CMD_HELP, "Shows the list of available commands and their functionality."},
            {CMD_CLEAR, "Clears the console window."},
            {CMD_EXIT, "Closes the application."},
            {CMD_RELOAD, "Reloads all files from the json folder."},
            {$"{CMD_SEARCH} table field value [field value]...", "Searches a specific table with at least a field that contains the given value."}
        };

        public static void DisplayHelpCommand() {
            foreach(var cmd in commands) {
                Console.WriteLine("{0, -50} {1}", cmd.Key, cmd.Value);
            }
        }

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
    }
}