using Repo;
using System;

namespace Command {
    public class Command {
        public const string RELOAD = "RELOAD";
        public const string CLEAR = "CLEAR";
        public const string ORGANIZATION = "ORGANIZATION";
        public const string TICKET = "TICKET";
        public const string USER = "USER";

        public void HandleInput(string input) {
            if(input == RELOAD) {
                
            } else if (input == CLEAR) {
                Console.Clear();
            } else {
                string[] inputParams = input.Split(' ');
            } 
        }
    }
}