using Model;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using static Utils.Constants;
using static Utils.JsonUtils;
using static Utils.FileUtils;
using static Utils.ConsoleUtils;
using static System.StringComparer;
using static System.IO.File;
using static System.String;
using static Utils.StringUtils;
using static System.Environment;

namespace Data {
  ///<summary>Data class imports and stores successfully imported valid JSON data.</summary>
  public class Database {
    public static Dictionary<string, dynamic> tables = new Dictionary<string, dynamic>(OrdinalIgnoreCase){
      {$"{TBL_ORGANIZATION}.json", new List<Organization>()},
      {$"{TBL_TICKET}.json", new List<Ticket>()},
      {$"{TBL_USER}.json", new List<User>()}
    };

    [Description("Imports data from the json folder into the database."),Category("Data")]
    public static void ImportEntitiesFromJson() {
      OutputToConsole("Starting file import" + NewLine);
      int passedImports = 0;
      string[] filepaths =  GetAllJsonFilepaths();

      try {
        if(filepaths.Length > 0) {
          foreach(var fp in filepaths) {
            string fileName = fp.Split('\\').Last();
            OutputToConsole($"Importing from {fileName}...");

            string fileContent = ReadAllText(fp);
            if(!IsNullOrWhiteSpace(fileContent)) {
              tables[fileName].AddRange(ParseJsonToTable(tables[fileName].GetType(), fileContent));
              if(tables[fileName].Count < 1) {
                throw new NullReferenceException();
              }
            } else {
              throw new NullReferenceException();
            }
            
            OutputPassToConsole("SUCCESS!");
            passedImports++;
          }
        } else {
          throw new WarningException();
        } 
      } catch(Exception e) {
        if(e is WarningException) {
          OutputWarningToConsole("Empty json folder detected.");
        } else if (e is KeyNotFoundException) {
          OutputExceptionToConsole(e, "This file has incorrect naming (File name must be [className].json):");
        } else if (e is NullReferenceException) {
          OutputExceptionToConsole(e, "This JSON file is empty.", false);
        }
      }

      string parseResult = GetParseFileResults(passedImports, filepaths.Length);
      if(passedImports == filepaths.Length) {
        OutputToConsole($"All files have been imported. ({parseResult})");
      } else {
        OutputWarningToConsole($"Not all files were successfully imported. ({parseResult})");
      }
    }
  }
}