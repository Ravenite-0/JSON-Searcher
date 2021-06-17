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
using static System.StringComparison;
using Model;

namespace Data {
  ///<summary>DataSearcher manages file search methods.</summary>
  public static class DataSearcher {

    //These lists are for testing purposes only.
    public static List<dynamic> entities;
    public static List<List<dynamic>> relatedEntities = new List<List<dynamic>>();


    public static void GetTableFields(string[] input) {
      try{
        if(input.Length == 1) {
          foreach(var table in tables) {
            OutputTypeFields(table.Value.type.GetGenericArguments().Single(), table.Key);
          }
        } else {
          foreach(string tableName in input.Skip(1)) {
            if(!IsNullOrWhiteSpace(tableName)) {
              var table = tables[tableName.ParseToTableName()];
              OutputTypeFields(table.type.GetGenericArguments().Single(), tableName.ParseToTableName());
            } else {
              throw new NullReferenceException();
            }
          } 
        }
        OutputToConsole("End of search.");
      } catch (Exception e) {
        if (e is KeyNotFoundException) {
          OutputExceptionToConsole(e, "This table cannot be found inside the database:");
        } else if (e is NullReferenceException) {
          OutputExceptionToConsole(e, "Empty table field found.", false);
        }else {
          OutputExceptionToConsole(e, "Oops! Something went wrong with table fields retrieval.");
        }
      }
      
    }

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

    public static void OutputSearchResults(string[] input) {
        var tableKey = input[1].ParseToTableName();
        var baseTable = (input.Length == 2) ? tables[tableKey].content : SearchBaseTable(tables[tableKey].content, input);
        OutputToConsole($"Searching in {tableKey}:{NewLine}");

        foreach(var row in baseTable) {
          OutputEntity(row);
          SearchAndOutputRelatedEntities(tableKey, row, tables[tableKey].pKeys, tables[tableKey].fKeys);
        }

        OutputSeparatorsToConsole(OUTPUT_MAJOR_LINESPLIT);
        OutputPassToConsole($"{baseTable.Count()} results found.");
        OutputToConsole("End of search.");
    }

    public static List<dynamic> SearchBaseTable(List<dynamic> baseTable, string[] input) {
      ListDictionary searchFields = new ListDictionary();
      try {
        for (int i = 2; i <= input.Length - 1; i++) {
          searchFields.Add(input[i].ToLower(), input[++i]);
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
        } else if (e is NullReferenceException) {
          OutputExceptionToConsole(e, $"One of the supplied field(s) not found in this table.", false);
        }
        //Exception found implies no results returned.
        return new List<dynamic>();
      }
    }

    public static bool CalculateExpectedProperty(dynamic entity, DictionaryEntry keyValue) {
      KeyValuePair<string, string> kpv = new KeyValuePair<string, string>(ToStringIncNull(keyValue.Key), ToStringIncNull(keyValue.Value));
      PropertyInfo p = GetPropertyFromEntity(entity, kpv.Key);
      if(p is null) {
        throw new NullReferenceException();
      }
      if(IsObjectStringList(p)) {
        var list = Enumerable.ToList<string>(p.GetValue(entity));
        foreach(string str in list) {
          if(str.Equals(kpv.Value, OrdinalIgnoreCase)) {
            return true;
          }
        }
        return false;
      } else if (IsObjectDateTime(p)) {
        return RoundDownDate(DateTime.Parse(ToStringIncNull(p.GetValue(entity)))) == DateTime.Parse(kpv.Value);
      } else {
        return (IsNullOrEmpty(kpv.Value.Trim())) ?
          ToStringIncNull(p.GetValue(entity) == kpv.Value.Trim()) :
          ContainsIgnoreCase(ToStringIncNull(p.GetValue(entity)), kpv.Value);
      }
    }


    public static void SearchAndOutputRelatedEntities(string tableKey, object row, List<string> pKeys, List<string> fKeys) {
      var pKeyValues = GenerateKeyValues(pKeys, row, true);
      var fKeyValues = GenerateKeyValues(fKeys, row);

      foreach(var table in tables) {
        if(table.Key != tableKey) {
          OutputToConsole(OUTPUT_LARGE_LINESPLIT);
          OutputToConsole($"Searching for related items from {table.Key}:");

          var resultTable = table.Value.content.Where(row => 
            (pKeyValues.Any(pkv => pkv.Value == ToStringIncNull(GetValueFromEntityProperty(row, pkv.Key)))) ||
            fKeyValues.Any(fkv => {
                var fkProperty = GetPropertyFromEntity(row, fkv.Key);
                return (fkProperty != null) ? fkv.Value == fkProperty.GetValue(row) : false;
              }));
          relatedEntities.Add(resultTable.ToList());

          foreach(var result in resultTable) {
            OutputEntity(result);
          }
          OutputToConsole(OUTPUT_LARGE_LINESPLIT);
          OutputPassToConsole($"A total of {resultTable.Count()} records are related to {tableKey.RemoveTableName()} entity {GetValueFromEntityProperty(row, "_id")}");
        }
      }
    }

    public static List<KeyValuePair<string, string>> GenerateKeyValues(List<string> keys, object row, bool isPk = false) =>
      keys.Zip(keys
          .Select(k => ToStringIncNull(GetValueFromEntityProperty(row, (isPk) ? "_id" : k))).ToList(),
          (k, v) => new KeyValuePair<string, string>(k, v)).ToList();
    
  }
}