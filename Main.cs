using static Utils.SysUtils;
using System;
using static Utils.FileUtils;
using static System.IO.Directory;
using System.Linq;
using static Repo.ItemRepo;
using static Utils.Constants;
using static Command.Command;

class Program {
    public static bool closeApp = false;
    
    static void Main(string[] args) {
        string[] files =  GetFiles(GetChildDir("json"));
        if(files.Count() > 0) {
            LoadFiles(files);
        } else {
            ThrowError("Empty directory. Did you remove all the files by accident?");
        }

        while(!closeApp) {
            HandleInput(Console.ReadLine());
        }
    }
}