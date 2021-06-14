using static Utils.CmdUtils;
using System;
using static Utils.FileUtils;
using static System.IO.Directory;
using System.Linq;
using static Data.Data;
using static Utils.Constants;

public abstract class Program {
    public static bool closeApp = false;
    
    static void Main(string[] args) {
        string[] filepaths =  GetAllJsonFilepaths();
        Console.WriteLine("Thank you for using JSON searcher!");

        if(filepaths.Count() > 0) {
            LoadFiles(filepaths);
        } else {
            ThrowError("No files found (Did you empty the folder by accident)? Type reload to try again");
        }

        while(!closeApp) {
            ExecuteCommand(Console.ReadLine());
        }
    }
}