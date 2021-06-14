using System.Collections.Generic;
using Model;
using System;
using Utils;
using static System.IO.File;
using static Newtonsoft.Json.JsonConvert;
using static System.StringComparison;
using System.Linq;
using System.Reflection;
using static Utils.Constants;
using System.ComponentModel;
using static Utils.JsonUtils;

namespace Data {
    ///<summary>Data class manages the storage of imported valid JSON data.</summary>
    public class Database {
        public static List<User> users = new List<User>();
        public static List<Ticket> tickets = new List<Ticket>();
        public static List<Organization> organizations = new List<Organization>();

        public static Dictionary<string, dynamic> tables = new Dictionary<string, dynamic> {
            {$"{TBL_ORGANIZATION}.json", organizations},
            {$"{TBL_TICKET}.json", tickets},
            {$"{TBL_USER}.json", users}
        };

        [Description("Imports data from the json folder into the database."),Category("Data")]
        public static void ImportEntitiesFromJson(string[] filepaths) {
            foreach(var fp in filepaths) {
                string fileName = fp.Split('\\').Last();
                Console.WriteLine($"Importing from {fp}...");
                tables[fileName].AddRange(ParseJsonToTable(tables[fileName].GetType(), fp));
            }
            Console.WriteLine("All files have been imported!");
        }
    }
}