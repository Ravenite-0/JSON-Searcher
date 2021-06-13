using System.Collections.Generic;
using Model;
using System;
using Newtonsoft.Json;
using Utils;
using static System.IO.File;
using static Newtonsoft.Json.JsonConvert;
using static System.StringComparison;

namespace Repo {
    public class ItemRepo {
        public static List<User> users;
        public static List<Ticket> tickets;
        public static List<Organization> organizations;
        public static void LoadFiles(string[] filepaths) {
            foreach(var fp in filepaths) {
                string fileContent = ReadAllText(fp);
                Console.WriteLine($"Importing from {fp}...");
                if(fp.EndsWith("\\organizations.json", InvariantCultureIgnoreCase)) {
                    organizations = DeserializeObject<List<Organization>>(fileContent, new JsonUtils<List<Organization>>());
                } else if (fp.EndsWith("\\tickets.json", InvariantCultureIgnoreCase)) {
                    tickets = DeserializeObject<List<Ticket>>( fileContent, new JsonUtils<List<Ticket>>());
                } else if (fp.EndsWith("\\users.json", InvariantCultureIgnoreCase)) {
                    users = DeserializeObject<List<User>>(fileContent, new JsonUtils<List<User>>());
                } else {
                    Console.WriteLine($"Invalid file name at {fp}, please check the README.md for more details regarding file naming.");
                }
            }
            Console.WriteLine("All files have been imported!");
        }
    }
}