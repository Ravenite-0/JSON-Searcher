using System.Collections.Generic;
using Model;
using System;
using Newtonsoft.Json;
using Utils;
using static System.IO.File;
using System.Linq;

namespace Repo {
    public class ItemRepo {
        static List<User> users {get; set;}
        static List<Ticket> tickets {get; set;}
        static List<Organization> organizations {get; set;}

        public static void LoadRepo(string[] filepaths) {
            foreach(var fp in filepaths) {
                Console.WriteLine($"Importing from {fp}...");
                if(fp.ToLower().EndsWith("\\organizations.json")) {
                    organizations = JsonConvert.DeserializeObject<Organization[]>(ReadAllText(fp), new JsonUtils<Organization[]>()).ToList();
                } else if (fp.ToLower().EndsWith("\\tickets.json")) {
                    tickets = JsonConvert.DeserializeObject<List<Ticket>>(ReadAllText(fp), new JsonUtils<List<Ticket>>());
                } else if (fp.ToLower().EndsWith("\\users.json")) {
                    users = JsonConvert.DeserializeObject<List<User>>(ReadAllText(fp), new JsonUtils<List<User>>());
                } else {
                    throw new InvalidOperationException($"Invalid file name at {fp}, please check the README.md for more information");
                }
            }
            Console.WriteLine("All files imported");
        }
    }
}