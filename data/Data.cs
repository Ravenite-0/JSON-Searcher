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
using System.Reflection;

namespace Data {
  ///<summary>Data class imports and stores successfully imported valid JSON data.</summary>
  public class Database {

    //This table
    public struct TableProperties {
      public List<dynamic> content;
      public Type type;
      public List<string> foreignKeyFormats;

      public TableProperties(List<dynamic> content, List<string> foreignKeyFormats, Type type) {
        this.content = content;
        this.foreignKeyFormats = foreignKeyFormats;
        this.type = type;
      }
    }

    /*
      This dictionary holds the table key and its respective type, the imported data list, as well as the filter ID for other tables.
      Filter IDs are representations of how the corresponding table's _id property are used as foreign keys in other tables.
    */
    public static Dictionary<string, TableProperties> tables =
      new Dictionary<string, TableProperties>() {
        { $"{TBL_ORGANIZATION}.json", new TableProperties(
            new List<dynamic>(), new List<string>() {"organization_id"}, new List<Organization>().GetType()) },
        { $"{TBL_TICKET}.json", new TableProperties(
            new List<dynamic>(), new List<string>(), new List<Ticket>().GetType()) },
        { $"{TBL_USER}.json", new TableProperties(
          new List<dynamic>(), new List<string>() {"submitter_id","assignee_id"}, new List<User>().GetType()) },
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
              var test = ParseJsonToTable(tables[fileName].type, fileContent);
              tables[fileName].content.AddRange(ParseJsonToTable(tables[fileName].type, fileContent));
              if(tables[fileName].content.Count < 1) {
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
          OutputExceptionToConsole(e, "This JSON file is empty.");
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