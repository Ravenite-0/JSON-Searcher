using static System.Reflection.Assembly;
using static System.IO.Directory;
using static System.IO.Path;
using static System.IO.File;
using System.IO;

namespace Utils {
    public class FileUtils {
        public static string GetChildDir(string childDir) {
            string targetpath = GetExecutingAssembly().Location;
            string result = null;
            while(GetParent(targetpath) != null) {
                targetpath = GetParent(targetpath).FullName;
                result = Combine(targetpath, childDir);
                if(Directory.Exists(result)) {
                    break;
                } else {
                    result = null;
                }
            }
            return result;
        }
    }
}