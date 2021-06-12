using System;
using static Utils.FileUtils;
using static System.IO.Directory;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Repo;

namespace Main {
    class Program {
        static void Main(string[] args) {
            string json_filepath = GetChildDir("json");
            //Loads up the lists of entities by utilising the filenames.   
            if(GetFiles(json_filepath).Count() == 0) {
                Console.WriteLine("Empty directory. Did you removed any files by accident?");
            } else {
                ItemRepo.LoadRepo(GetFiles(json_filepath));
            }

            //Console.WriteLine("All files have been imported!");


            
            
        }
    }
}