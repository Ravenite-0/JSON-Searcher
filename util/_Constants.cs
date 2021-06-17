using static System.Environment;

namespace Utils {
  ///<summary>Manages the constants like commands and table names.</summary>
  public abstract class Constants {
    public const string CMD_HELP = "help",
                        CMD_CLEAR = "clear",
                        CMD_EXIT = "exit",
                        CMD_RELOAD = "reload",
                        CMD_SEARCH = "search",
                        TBL_ORGANIZATION = "organizations",
                        TBL_TICKET = "tickets",
                        TBL_USER = "users",
                        EMPTY_SEARCH_TERM = "%",
                        OUTPUT_SMALL_LINESPLIT = "----------------------------------------------------------------------";

    public static string OUTPUT_LARGE_LINESPLIT = $"{NewLine}======================================================================",
                         OUTPUT_MAJOR_LINESPLIT = $"{NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~{NewLine}";
  }
}