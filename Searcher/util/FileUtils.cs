using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Reflection.Assembly;
using static Utils.Config;
using static System.DateTime;
using System;
using static Utils.StringUtils;

namespace Utils {
  ///<summary>FileUtils manages methods that interacts with files in the system.</summary>
  public static class FileUtils {
    //Keep looping the parent directory until the folder is found.
    internal static string GetChildDir(string childDir) {
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