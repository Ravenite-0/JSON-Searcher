using static Utils.Constants;
using static Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using static Utils.SysUtils;
using System.Collections.Specialized;
using System.Collections;
using Model;
using System.Reflection;
using static Utils.StringUtils;

namespace Data {
    public abstract class ItemSearch {
        public static void SearchItems(string[] input) {
            switch(input[1].ToUpper()) {
                case TBL_ORGANIZATION:
                    Search(input.Skip(2).ToArray());
                    break;
                case TBL_TICKET:
                    Search(input.Skip(2).ToArray());
                    break;
                case TBL_USER:
                    break;
                default:
                    ThrowError($"Invalid table {input[1]}. Please type HELP for more information regarding tables.");
                    break;
            }
        }

        public static void Search(string[] fields) {
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
                    o.ToConsoleString<Organization>();
                }
                Console.WriteLine("End of search.");
            }
        }
    }
}