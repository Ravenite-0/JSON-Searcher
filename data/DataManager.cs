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
      OutputToConsole($"Searching in {tableKey}:{NewLine}");
      foreach(var row in tables[tableKey].content) {
        OutputEntity(row);
        GetAllRelatedEntities(tableKey, row, tables[tableKey].pKeys, tables[tableKey].fKeys);
      }
    }

    internal static void GetAllRelatedEntities(string tableKey, object row, List<string> pKeys, List<string> fKeys) {
      List<string> pValues = pKeys.Select(k => ToStringIncNull(row.GetType().GetProperty("_id").GetValue(row))).ToList();
      var pKeyValues = pKeys.Zip(pValues, (k,v) => new {
        key = k,
        value = v
      });
      List<string> fValues = fKeys.Select(k => ToStringIncNull(row.GetType().GetProperty(k).GetValue(row))).ToList();
      var fKeyValues = fKeys.Zip(fValues,(k,v) => new {
        key = k,
        value = v
      });

      foreach(var table in tables) {
        if(table.Key != tableKey) {
          foreach(var resultRow in table.Value.content) {
            if(pKeyValues.Any(pkv => {
                var test = ToStringIncNull(resultRow.GetType().GetProperty(pkv.key).GetValue(resultRow));
                return pkv.value == test;
              }) ||
              fKeyValues.Any(fkv => {
                var fkProperty = resultRow.GetType().GetProperty(fkv.key);
                if(fkProperty != null) {
                  var test = fkProperty.GetValue(resultRow);
                  return fkv.value == test;
                } else {return false;}
              })) {
                OutputEntity(resultRow);
                break;
            }
          }
        }
      }
    }

    public static void DisplaySearchResults<T>(this List<T> entities) {
      foreach(var o in entities) {
        OutputEntity(o);
      }
      Console.WriteLine($"Total results found: {entities.Count()}");
      Console.WriteLine("End of search.");
    }
  }
}