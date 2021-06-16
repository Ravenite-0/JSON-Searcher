using static Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Collections;
using System.Reflection;
using static Utils.StringUtils;
using static Utils.ConsoleUtils;
using static System.Environment;

namespace Data {
  ///<summary>DataManager manages methods that performs CRUD operations on the Database class.</summary>
  public static class DataManager {
    public static void ValidateSearch(string[] input) {
      try {
        if(input.Length < 2) {
          throw new ArgumentNullException();
        } else if (input.Length == 2) {
          OutputAllResults(input[1].ParseToTableName());
        } else {
          //SearchAndOutputResults(input);
        }
      } catch(Exception e) {
        if(e is ArgumentNullException) {
          OutputExceptionToConsole(e, "Please provide a table to be searched (Organizations, Tickets, Users).", false);
        } else if (e is KeyNotFoundException) {
          OutputExceptionToConsole(e, $"Table {input[1]} is invalid:");
        } else {
          OutputExceptionToConsole(e, $"Oops! Something went wrong during table lookup:");
        }
      }
    }

    internal static void OutputAllResults(string tableKey) {
      foreach(var row in tables[tableKey].content) {
        OutputToConsole($"{tableKey}:{NewLine}");
        OutputEntity(row);
        GetAllRelatedEntities(tableKey, row, tables[tableKey].foreignKeyFormats);
      }
    }

    internal static void GetAllRelatedEntities(string tableKey, object row, List<string> foreignKeys) {
      string rowID = row.GetType().GetProperty("_id").GetValue(row).ToString();
      foreach(var table in tables) {
        if(table.Key != tableKey) {
          foreach(var tester in table.Value.content) {
            foreach(var fk in foreignKeys) {
              var test = tester.GetType().GetProperty(fk).GetValue(tester);
              if((tester.GetType().GetProperty(fk).GetValue(tester) ?? "").ToString() == rowID) {
                OutputEntity(tester);
                break;
              }
            }
          }
        }
      }
    }

    
          // var tableResults = table.Where(row => {
          //   foreach(DictionaryEntry field in searchFields) {
          //     if(!row.GetType().GetProperty(field.Key.ToString()).GetValue(row).ToString().Contains(field.Value.ToString())) {
          //       return false;
          //     }
          //   }
          //   return true;
          // });
        
    public static void DisplaySearchResults<T>(this List<T> entities) {
      foreach(var o in entities) {
        OutputEntity(o);
      }
      Console.WriteLine($"Total results found: {entities.Count()}");
      Console.WriteLine("End of search.");
    }
  }
}