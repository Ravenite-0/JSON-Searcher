using static System.StringComparison;

namespace Utils {
    ///<summary>StringUtils manages methods that performs operations on strings.</summary>
    public static class StringUtils {
        public static bool EndsWithIgnoreCase(this string str, string condition) =>
            str.EndsWith(condition,  InvariantCultureIgnoreCase);
        
        public static bool StartsWithIgnoreCase(this string str, string condition) =>
            str.StartsWith(condition, InvariantCultureIgnoreCase);
        
        public static bool ContainsIgnoreCase(this string str, string condition) =>
            str.Contains(condition, InvariantCultureIgnoreCase);
    }    
}