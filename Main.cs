using static Utils.SysUtils;
using System;
using static Utils.FileUtils;
using static System.IO.Directory;
using System.Linq;
using static Repo.ItemRepo;
using static Utils.Constants;
using static Command.Command;

class Program {
    static void Main(string[] args) {
        string json_filepath = GetChildDir("json");
        if(GetFiles(json_filepath).Count() > 1) {
            LoadRepo(GetFiles(json_filepath));
        } else {
            ThrowError("Empty directory. Did you remove all the files by accident?");
        }

        while(!exit) {
            HandleInput(Console.ReadLine());
        }
    }
}