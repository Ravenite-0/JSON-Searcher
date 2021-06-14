using static Utils.Constants;
using static Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Collections;
using Model;
using System.Reflection;
using static Utils.CmdUtils;
using static System.StringComparison;

namespace Data {
    public abstract class ItemSearch {
        public static void SearchItems(string[] input) {
            if(input.Length < 2) {
                ThrowError("No tables searched, please type HELP for more information regarding search command formatting.");
            } else {
                if(tables.Any(table => table.Contains(input[1], InvariantCultureIgnoreCase))) {
                    
                } else {
                    ThrowError($"Invalid table {input[1]}. Please type HELP for more information regarding tables.");
                }
                switch(input[1].ToLower()) {
                    case TBL_ORGANIZATION:
                        SearchByFields<Organization>(input, organizations);
                        break;
                    case TBL_TICKET:
                        SearchByFields<Ticket>(input, tickets);
                        break;
                    case TBL_USER:
                        SearchByFields<User>(input, users);
                        break;
                    default:
                        
                        break;
            }
            }
            
        }

        public static void SearchByFields<T>(string[] fields, List<T> table) {
            ListDictionary searchFields = new ListDictionary();
            try {
                for(int i = 0; i < fields.Length; i++) {
                    searchFields.Add(fields[i], fields[++i]);
                }

                var results = organizations.Where(organization => {
                    foreach(DictionaryEntry f in searchFields) {
                        var test = organization.GetType().GetProperty(f.Key.ToString()).GetValue(organization);
                        if(!organization.GetType().GetProperty(f.Key.ToString()).GetValue(organization).ToString().Contains(f.Value.ToString())) {
                            return false;
                        }
                    }
                    return true;
                });

                Output(results.ToList());
            } catch(IndexOutOfRangeException) {
                ThrowError("");
            }
        }

        public static void Output(List<Organization> orgs) {
            var resultCount = orgs.Count(); 
            Console.WriteLine($"Total results found: {resultCount}");
            if(resultCount > 0) {
                foreach(var o in orgs) {
                    //o.ToConsoleString<Organization>();
                }
                Console.WriteLine("End of search.");
            }
        }
    }
}