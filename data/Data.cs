using System.Collections.Generic;
using Model;
using System;
using System.ComponentModel;
using Utils;
using static System.IO.File;
using static Newtonsoft.Json.JsonConvert;
using static System.StringComparison;
using System.Linq;

namespace Data {
    ///<summary>Data class manages the data imported from json files in various lists for storage/searching.</summary>
    public static class Data {
        public static List<User> users;
        public static List<Ticket> tickets;
        public static List<Organization> organizations;

        [Description("Loads json files from the identified filepaths into the lists into Data."),Category("Data")]
        public static void LoadFiles(string[] filepaths) {
            foreach(var fp in filepaths) {
                string fileName = fp.Split('\\').Last();
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