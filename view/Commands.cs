using static Repo.ItemRepo;
using static Utils.FileUtils;
using System;
using static System.IO.Directory;
using static Utils.Constants;

namespace Command {
    public abstract class Command {
        public static void HandleInput(string input) {
            string[] input_blocks = input.Split(' ');
            switch(input_blocks[0]) {
                case CMD_HELP:
                    Console.WriteLine("");
                    break;
                case CMD_RELOAD:
                    LoadRepo(GetFiles(GetChildDir("json")));
                    break;
                case CMD_CLEAR:
                    Console.Clear();
                    break;
            }
        }
    }
}