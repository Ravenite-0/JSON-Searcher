using System.ComponentModel;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Reflection.Assembly;

namespace Utils {
    ///<summary>FileUtils manages methods that interacts with files in the system.</summary>
    public class FileUtils {

        [Description("Returns the full filepath of a folder in this project by providing the folder name."),Category("File")]
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

        public static string[] GetAllJsonFilepaths() =>
            GetFiles(GetChildDir("json"));
    }
}