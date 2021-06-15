using Model;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using static Utils.Constants;
using static Utils.CmdUtils;
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
            try{
                foreach(var fp in filepaths) {
                    Console.WriteLine($"Importing from {fp}...");
                    string fileName = fp.Split('\\').Last();
                    string fileContent = System.IO.File.ReadAllText(fp);
                    if(!String.IsNullOrWhiteSpace(fileContent)) {
                        tables[fileName].AddRange(ParseJsonToTable(tables[fileName].GetType(), fileContent));
                    } else {
                        ThrowError($"{fileName} does not have any content. Please double check your files in json folder.");
                    }
                }
            } catch(Exception e) {
                if(e is KeyNotFoundException) {
                    ThrowError("Files in the json folder must be of .json format. Type RELOAD after you've gotten the right files in the json folder", e);
                }
            }
            Console.WriteLine("All valid files have been imported!");
        }
    }
}