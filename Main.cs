using System;
using static Util.FileUtils;
using static System.IO.Directory;
using System.Linq;
using System.Collections.Generic;

namespace Main {
    class Program {
        static void Main(string[] args) {
            string json_filepath = GetChildDir("json");            
            List<string> files = GetFiles(json_filepath).Select(file => {
                Console.WriteLine($"Importing from {file}...");
                return file;
            }).ToList();

            Console.WriteLine("All files have been imported!");
            
            
        }
    }
}