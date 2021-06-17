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
using static System.String;

namespace Data {
  ///<summary>DataSearcher manages file search methods.</summary>
  public static class DataSearcher {
    public static void ValidateAndReturnSearchResults(string[] input) {
      try {
        if(input.Length < 2) {
          throw new ArgumentNullException();
        } else {
          OutputSearchResults(input);
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

    internal static void OutputSearchResults(string[] input) {
      var tableKey = input[1].ParseToTableName();
      var baseTable = (input.Length == 2) ? tables[tableKey].content : SearchBaseTable(tables[tableKey].content, input);

      OutputSeparatorsToConsole(OUTPUT_MAJOR_LINESPLIT);
      OutputToConsole($"Searching in {tableKey}:{NewLine}");

      foreach(var row in baseTable) {
        OutputEntity(row);
        SearchAndOutputRelatedEntities(tableKey, row, tables[tableKey].pKeys, tables[tableKey].fKeys);
      }

      OutputSeparatorsToConsole(OUTPUT_MAJOR_LINESPLIT);
      OutputPassToConsole($"{baseTable.Count()} results found.");
      OutputToConsole("End of search.");
    }

    internal static List<dynamic> SearchBaseTable(List<dynamic> baseTable, string[] input) {
      ListDictionary searchFields = new ListDictionary();
      try {
        for (int i = 2; i <= input.Length - 1; i++) {
          searchFields.Add(input[i], input[++i]);
        }

        return baseTable.Where(row => {
          foreach(DictionaryEntry field in searchFields) {
            if(!CalculateExpectedProperty(row, field)) {
              return false;
            }
          }
          return true;
        }).ToList();
      } catch (Exception e) {
        if (e is IndexOutOfRangeException) {
          OutputExceptionToConsole(e, $"Field {input.Last()} had no value provided");
        }
        //Exception found implies no results returned.
        return new List<dynamic>();
      }
    }

    internal static bool CalculateExpectedProperty(dynamic entity, DictionaryEntry keyValue) {
      KeyValuePair<string, string> kpv = new KeyValuePair<string, string>(ToStringIncNull(keyValue.Key), ToStringIncNull(keyValue.Value));
      PropertyInfo p = GetPropertyFromEntity(entity, kpv.Key);
      if(IsObjectStringList(p)) {
        //TODO - Resolve case sensitivity
        return Enumerable.ToList<string>(p.GetValue(entity)).Contains(kpv.Value);
      } else if (IsObjectDateTime(p)) {
        return RoundDownDate(DateTime.Parse(ToStringIncNull(p.GetValue(entity)))) == DateTime.Parse(kpv.Value);
      } else {
        return (IsNullOrEmpty(kpv.Value.Trim())) ?
          ToStringIncNull(p.GetValue(entity) == kpv.Value.Trim()) :
          ContainsIgnoreCase(ToStringIncNull(p.GetValue(entity)), kpv.Value);
      }
    }

    internal static void SearchAndOutputRelatedEntities(string tableKey, object row, List<string> pKeys, List<string> fKeys) {
      var pKeyValues = GenerateKeyValues(pKeys, row, true);
      var fKeyValues = GenerateKeyValues(fKeys, row);

      foreach(var table in tables) {
        if(table.Key != tableKey) {
          OutputToConsole(OUTPUT_LARGE_LINESPLIT);
          OutputToConsole($"Searching for related items from {table.Key}:");

          foreach(var resultRow in table.Value.content) {
            if(pKeyValues.Any(pkv => 
                pkv.Value == ToStringIncNull(GetValueFromEntityProperty(resultRow, pkv.Key))) ||
              fKeyValues.Any(fkv => {
                var fkProperty = GetPropertyFromEntity(resultRow, fkv.Key);
                return (fkProperty != null) ? fkv.Value == fkProperty.GetValue(resultRow) : false;
              })) {
                OutputEntity(resultRow);
                break;
            }
          }
          OutputToConsole(OUTPUT_LARGE_LINESPLIT);
        }
      }
    }

    internal static List<KeyValuePair<string, string>> GenerateKeyValues(List<string> keys, object row, bool isPk = false) =>
      keys.Zip(keys
          .Select(k => ToStringIncNull(GetValueFromEntityProperty(row, (isPk) ? "_id" : k))).ToList(),
          (k, v) => new KeyValuePair<string, string>(k, v)).ToList();
    
  }
}