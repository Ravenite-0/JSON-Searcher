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
using static Utils.Constants;
using static Utils.SysUtils;

namespace Data {
  ///<summary>DataManager manages methods that performs CRUD operations on the Database class.</summary>
  public static class SearchByProperty {
    public static void ValidateSearch(string[] input) {
      try {
        if(input.Length < 2) {
          throw new ArgumentNullException();
        } else {
          OutputResults(input);
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

    internal static void OutputResults(string[] input) {
      var tableKey = input[1].ParseToTableName();
      var baseTable = (input.Length == 2) ? tables[tableKey].content : FilterBaseTable(tables[tableKey].content, input);

      OutputSeparatorsToConsole(OUTPUT_MAJOR_LINESPLIT);
      OutputToConsole($"Searching in {tableKey}:{NewLine}");

      foreach(var row in baseTable) {
        OutputEntity(row);
        OutputRelatedEntities(tableKey, row, tables[tableKey].pKeys, tables[tableKey].fKeys);
      }

      OutputSeparatorsToConsole(OUTPUT_MAJOR_LINESPLIT);
      OutputPassToConsole($"{baseTable.Count()} results found.");
      OutputToConsole("End of search.");
    }

    internal static List<dynamic> FilterBaseTable(List<dynamic> baseTable, string[] input) {
      ListDictionary searchFields = new ListDictionary();
      for (int i = 2; i < input.Length; i++) {
        searchFields.Add(input[i], input[++i]);
      }

      return baseTable.Where(row => {
        foreach(DictionaryEntry field in searchFields) {
          if(!CalculateExpectedProperty(row.GetType().GetProperty(field.Key.ToString()), row, field.Value.ToString())) {
          //if(!.GetValue(row).ToString().Contains(field.Value.ToString())) {
            return false;
          }
        }
        return true;
      }).ToList();
    }

    internal static bool CalculateExpectedProperty(PropertyInfo p, dynamic obj, string condition) {
      if(IsObjectList(p)) {
        return Enumerable.ToList(p.GetValue(obj)).Contains(condition);
      } else if (IsObjectDateTime(p)) {
        var test = DateTime.Parse(ToStringIncNull(p.GetValue(obj))).Date;
        var test2 = DateTime.Parse(condition).Date;
        return DateTime.Parse(ToStringIncNull(p.GetValue(obj))).Date == DateTime.Parse(condition).Date;
      } else {
        return ContainsIgnoreCase(ToStringIncNull(p.GetValue(obj)), condition);
      }
    }

    internal static void OutputRelatedEntities(string tableKey, object row, List<string> pKeys, List<string> fKeys) {
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
          OutputToConsole(OUTPUT_LARGE_LINESPLIT);
          OutputToConsole($"Searching for related items from {table.Key}:");

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
          OutputToConsole(OUTPUT_LARGE_LINESPLIT);
        }
      }
    }
  }
}