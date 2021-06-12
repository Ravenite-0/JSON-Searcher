namespace Utils {
    public abstract class Constants {
        public const string CMD_RELOAD = "RELOAD",
                            CMD_CLEAR = "CLEAR",
                            CMD_HELP = "HELP",
                            CMD_SEARCH = "SEARCH",
                            TBL_ORGANIZATION = "ORGANIZATION",
                            TBL_TICKET = "TICKET",
                            TBL_USER = "USER";
        
        public const string HELP_STR = $@"";

        public static bool exit = false;
    }
}