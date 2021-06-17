using Model;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using static Utils.Constants;
using static Utils.JsonUtils;
using static Utils.FileUtils;
using static Utils.ConsoleUtils;
using static System.IO.File;
using static System.String;
using static Utils.StringUtils;
using static System.Environment;

namespace Data {
  ///<summary>Data class imports and stores successfully imported valid JSON data.</summary>
  public class Database {

    //Stores search parameters and other variables for easier searching.
    public struct TableProperties {
      public List<dynamic> content;
      public Type type;
      public List<string> pKeys;
      public List<string> fKeys;

      public TableProperties(List<dynamic> content, List<string> pKeys, List<string> fKeys, Type type) {
        this.content = content;
        this.pKeys = pKeys;
        this.fKeys = fKeys;
        this.type = type;
      }
    }

    /*
      This dictionary maps the file content to their respective object type list.
      It also includes how the entities are related to other tables (Linking _id to various id foreign keys).
    */
    public static Dictionary<string, TableProperties> tables =
      new Dictionary<string, TableProperties>() {
        { $"{TBL_ORGANIZATION}.json", new TableProperties(
            new List<dynamic>(), new List<string>() {"organization_id"}, new List<string>(), new List<Organization>().GetType()) },
        { $"{TBL_TICKET}.json", new TableProperties(
            new List<dynamic>(), new List<string>(), new List<string>() {"submitter_id", "assignee_id", "organization_id"}, new List<Ticket>().GetType()) },
        { $"{TBL_USER}.json", new TableProperties(
          new List<dynamic>(), new List<string>(), new List<string>() {"organization_id"}, new List<User>().GetType()) },
      };

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
        OutputPassToConsole($"All files have been imported. ({parseResult})");
      } else {
        OutputWarningToConsole($"Not all files were successfully imported. ({parseResult})");
      }
    }
  }
}