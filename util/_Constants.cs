using System.Collections.Generic;

namespace Utils {
    public abstract class Constants {
        public static string[] tables = new string[] {TBL_ORGANIZATION, TBL_TICKET, TBL_USER};  
        public const string CMD_HELP = "help",
                            CMD_CLEAR = "clear",
                            CMD_EXIT = "exit",
                            CMD_RELOAD = "reload",
                            CMD_SEARCH = "search",
                            TBL_ORGANIZATION = "organization",
                            TBL_TICKET = "ticket",
                            TBL_USER = "user";
    }
}