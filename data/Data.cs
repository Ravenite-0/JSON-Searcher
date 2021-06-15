using Model;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using static Utils.Constants;
using static Utils.CmdUtils;
using static Utils.JsonUtils;
using static Utils.FileUtils;
using static Utils.ConsoleUtils;
namespace Data {
    ///<summary>Data class manages the storage of imported valid JSON data.</summary>
    public class Database {
        public static Dictionary<string, dynamic> tables = new Dictionary<string, dynamic> {
            {$"{TBL_ORGANIZATION}.json", new List<Organization>()},
            {$"{TBL_TICKET}.json", new List<Ticket>()},
            {$"{TBL_USER}.json", new List<User>()}
        };

        [Description("Imports data from the json folder into the database."),Category("Data")]
        public static void ImportEntitiesFromJson() {
            try{
                string[] filepaths =  GetAllJsonFilepaths();
                    if(filepaths.Count() > 0) {
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
                    } else {
                        //ThrowError("No files found (Did you empty the folder by accident)? Type reload to try again");
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