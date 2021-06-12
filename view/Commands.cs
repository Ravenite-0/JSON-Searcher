using static Repo.ItemRepo;
using static Utils.FileUtils;
using System;
using static System.IO.Directory;
using static Utils.Constants;

namespace Command {
    public abstract class Command {
        public static void HandleInput(string input) {
            string[] input_blocks = input.Split(' ');
            switch(input_blocks[0].ToUpper()) {
                case CMD_HELP:
                    Console.WriteLine(HELP_STR);
                    break;
                case CMD_RELOAD:
                    LoadRepo(GetFiles(GetChildDir("json")));
                    break;
                case CMD_CLEAR:
                    Console.Clear();
                    break;
                case CMD_SEARCH:
                    //TODO
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine($"Command {input_blocks[0]} is not found. Type HELP for the list of available commands.");
                    break;
            }
        }
    }
}