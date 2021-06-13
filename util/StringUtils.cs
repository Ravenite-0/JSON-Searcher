using System;
using static System.StringComparison;

namespace Utils {
    //This class handles string-related operations.
    public static class StringUtils {
        public static void ToConsoleString<T>(this T obj) {
            var p = obj.GetType().GetProperties();
            foreach(var p1 in p) {
                Console.WriteLine($"{p1.Name.ToString()}  ->  {p1.GetValue(obj).ToString()}");
            }
            Console.WriteLine("==================================================");
        } 
    }
}