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

namespace Data {
    ///<summary>DataManager manages methods that performs CRUD operations on the Database class.</summary>
    public abstract class DataManager {
        public static Stopwatch stopwatch = new Stopwatch();
        public static void SearchItems(string[] input) {
            if(input.Length < 2) {
                //ThrowError("No tables searched, please type HELP for more information regarding search command formatting.");
            } else {
                var table = tables[input[1].ParseToTableName()];
                SearchByFields(input.Skip(2).ToArray(), table);
            }
        }

        public static void SearchByFields<T>(string[] fields, List<T> table) {
            if(fields.Count() == 0) {
                DisplaySearchResults(table); 
            }
            else if(fields.Length % 2 == 0) {
                ListDictionary searchFields = new ListDictionary();

                for(int i = 0; i < fields.Length; i++) {
                    searchFields.Add(fields[i], fields[++i]);
                }

                var filtered = table.Where(row => {
                    foreach(DictionaryEntry field in searchFields) {
                        if(!row.GetType().GetProperty(field.Key.ToString()).GetValue(row).ToString().Contains(field.Value.ToString())) {
                            return false;
                        }
                    }
                    return true;
                });

                DisplaySearchResults<T>(filtered.ToList());
            } else {
                //ThrowError($"Field {fields.Last()} has no value. Please type HELP for more details on using the search command.");
            }
        }

        public static void DisplaySearchResults<T>(List<T> entities) {
            
            foreach(var o in entities) {
                o.ToConsoleString();
            }
            Console.WriteLine($"Total results found: {entities.Count()}");
            Console.WriteLine("End of search.");
        }
    }
}