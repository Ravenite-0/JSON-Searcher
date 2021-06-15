using static Utils.Constants;
using static Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Collections;
using Model;
using System.Reflection;
using static Utils.CmdUtils;
using static System.StringComparison;
using System.ComponentModel;
using static Utils.StringUtils;
using static System.IO.File;
using System.Diagnostics;
using static Utils.ConsoleUtils;

namespace Data {
  ///<summary>DataManager manages methods that performs CRUD operations on the Database class.</summary>
  public abstract class DataManager {
    public static void SearchItems(string[] input) {
      try {
        if(input.Length < 2) {
          throw new ArgumentNullException();
        } else {
          var table = tables[input[1].ParseToTableName()];
          SearchByFields(input.Skip(2).ToArray(), table);
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

    public static void SearchByFields<T>(string[] fields, List<T> table) {
      try {
        if(fields.Length > 0) {
          ListDictionary searchFields = new ListDictionary();
          for(int i = 0; i < fields.Length; i++) {
            searchFields.Add(fields[i], fields[++i]);
          }

          var tableResults = table.Where(row => {
            foreach(DictionaryEntry field in searchFields) {
              if(!row.GetType().GetProperty(field.Key.ToString()).GetValue(row).ToString().Contains(field.Value.ToString())) {
                return false;
              }
            }
            return true;
          });

          DisplaySearchResults<T>(tableResults.ToList());
        } else {
          DisplaySearchResults(table); 
        }
      } catch (Exception e) {
        if(e is IndexOutOfRangeException) {
          OutputExceptionToConsole(e, $"Your search field {fields.Last()} had no search value (Did you forget to use % to search empty fields?):");
        }
      }
    }

    public static void DisplaySearchResults<T>(List<T> entities) {
      foreach(var o in entities) {
        o.OutputEntity();
      }
      Console.WriteLine($"Total results found: {entities.Count()}");
      Console.WriteLine("End of search.");
    }
  }
}