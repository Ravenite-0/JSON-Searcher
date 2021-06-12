
namespace Utils {
    //This class uses inheritance to instantiate a custom JSON reader that converts JSON files to various classes
    public static class StringUtils {
        public static bool CaselessEndsWith(this string str, string criteria) =>
            str.ToLower().EndsWith(criteria);
        
        public static bool CaselessStartsWith(this string str, string criteria) =>
            str.ToLower().StartsWith(criteria);
    }
}