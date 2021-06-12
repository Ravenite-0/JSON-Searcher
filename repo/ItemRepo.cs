using System.Collections.Generic;
using Model;
using System;
using Newtonsoft.Json;
using Utils;
using static System.IO.File;
using static Newtonsoft.Json.JsonConvert;

namespace Repo {
    public class ItemRepo {
        static List<User> users {get; set;}
        static List<Ticket> tickets {get; set;}
        static List<Organization> organizations {get; set;}

        public static void LoadRepo(string[] filepaths) {
            foreach(var fp in filepaths) {
                Console.WriteLine($"Importing from {fp}...");
                if(fp.CaselessEndsWith("\\organizations.json")) {
                    organizations = DeserializeObject<List<Organization>>(
                                ReadAllText(fp),
                                new JsonUtils<List<Organization>>()
                            );
                } else if (fp.CaselessEndsWith("\\tickets.json")) {
                    tickets = DeserializeObject<List<Ticket>>(
                                ReadAllText(fp),
                                new JsonUtils<List<Ticket>>()
                            );
                } else if (fp.CaselessEndsWith("\\users.json")) {
                    users = DeserializeObject<List<User>>(
                                ReadAllText(fp),
                                new JsonUtils<List<User>>()
                            );
                } else {
                    Console.WriteLine($"Invalid file name at {fp}, please check the README.md for more details regarding file naming.");
                }
                Console.WriteLine("All files have been imported!");
            }
        }
    }
}