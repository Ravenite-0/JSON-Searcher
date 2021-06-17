using static System.StringComparison;
using static System.String;
using System;

namespace Utils {
  ///<summary>Manages methods that performs operations on strings.</summary>
  public static class StringUtils {        
    public static bool ContainsIgnoreCase(string str, string condition) =>
      str.Contains(condition, InvariantCultureIgnoreCase);
      
    public static string ToStringIncNull(object obj) =>
      (obj is null) ? "" : obj.ToString();

    public static string ParseToTableName(this string str) =>
      $"{str.ToLower()}.json";
    
    public static string RemoveTableName(this string str) =>
      //This is fixed because all tableNames ends with .json
      str.Substring(0, str.Length - 5);

    public static string GetParseFileResults(int passedFiles, int totalFiles) =>
      Format("Imported: {0} -- Failed: {1}", passedFiles, totalFiles - passedFiles);

    public static string ParseEmptyIdentifier(string str) =>
      (str == "%") ? "" : str;
    
    public static string ParseDateTimeToString(DateTime date) =>
      $"{date.Year}-{date.Month}-{date.Day}_{date.Hour}-{date.Minute}-{date.Second}-{date.Millisecond}";
  }    
}