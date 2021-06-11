using static System.Reflection.Assembly;
using static System.IO.Directory;
using static System.IO.Path;
namespace Util {
    public class FileUtils {
        //This method looks for the relative filepath of "json" folder in this repo.
        public static string GetChildDir(string childDir) {
            string targetpath = GetExecutingAssembly().Location;
            string result = null;
            while(GetParent(targetpath) != null) {
                targetpath = GetParent(targetpath).FullName;
                result = Combine(targetpath, childDir);
                if(Exists(result)) {
                    break;
                } else {
                    result = null;
                }
            }
            return result;
        }
    }
}