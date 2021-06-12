using System;
using static Utils.FileUtils;
using static System.IO.Directory;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Main {
    class Program {
        static void Main(string[] args) {
            string json_filepath = GetChildDir("json");       

            var files = GetFiles(json_filepath).Select(filepath => {
                //Console.WriteLine($"Importing from {filepath}...");
                string filename = 
                var text = JsonConvert.DeserializeObject(FileToString(filepath));
                Console.WriteLine(text);
                return text;
            }).ToList();

            //Console.WriteLine("All files have been imported!");


            
            
        }
    }
}