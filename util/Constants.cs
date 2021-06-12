namespace Utils {
    public abstract class Constants {
        public const string CMD_RELOAD = "RELOAD",
                            CMD_CLEAR = "CLEAR",
                            CMD_HELP = "HELP",
                            CMD_SEARCH = "SEARCH",
                            TBL_ORGANIZATION = "ORGANIZATION",
                            TBL_TICKET = "TICKET",
                            TBL_USER = "USER";
        public static bool exit = false;
        public const string HELP_STR = $@"
help                                                Show command line help.
clear                                               Clears the console panel.
reload                                              Re-imports data from the JSON files.
search table field value [field value]...           Searches a specific table using at least 1 field with a specific value.";
    }
}