using System;
using static Utils.FileUtils;
using static System.IO.Directory;
using System.Linq;
using static Repo.ItemRepo;
using static Utils.Constants;
using static Command.Command;

namespace Main {
    class Program {
        static void Main(string[] args) {
            string json_filepath = GetChildDir("json");
            if(GetFiles(json_filepath).Count() == 0) {
                Console.WriteLine("Empty directory. Did you removed any files by accident?");
            } else {
                LoadRepo(GetFiles(json_filepath));
            }
            while(!exit) {
                HandleInput(Console.ReadLine());
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();   
        }
    }
}