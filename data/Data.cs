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
                string fileContent = ReadAllText(fp);
                Console.WriteLine($"Importing from {fp}...");

                // Type tableType = tables[fileName].GetType();

                // var test = tableType.GetMethod("DeserializeObject");
                // var method = test.MakeGenericMethod(tableType);

                tables[fileName].AddRange(DeserializeObject<List<Entity>>(fileContent, new JsonUtils<List<Entity>>()));


                // //if(fileName.StartsWith())
                // if(fp.EndsWith("\\organizations.json", InvariantCultureIgnoreCase)) {
                //     organizations = DeserializeObject<List<Organization>>(fileContent, new JsonUtils<List<Organization>>());
                // } else if (fp.EndsWith("\\tickets.json", InvariantCultureIgnoreCase)) {
                //     tickets = DeserializeObject<List<Ticket>>( fileContent, new JsonUtils<List<Ticket>>());
                // } else if (fp.EndsWith("\\users.json", InvariantCultureIgnoreCase)) {
                //     users = DeserializeObject<List<User>>(fileContent, new JsonUtils<List<User>>());
                // } else {
                //     Console.WriteLine($"Invalid file name at {fp}, please check the README.md for more details regarding file naming.");
                // }
            }
            Console.WriteLine("All files have been imported!");
        }
    }
}