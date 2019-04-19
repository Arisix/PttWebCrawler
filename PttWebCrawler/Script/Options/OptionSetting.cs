namespace Options
{
    class OptionSetting
    {
        public static bool IsShowHelp = false;
        public static bool IsShowVersion = false;

        public readonly static OptionSet Setting = new OptionSet() {
            { "h|help",  "show this message and exit", v => IsShowHelp = v != null },
            { "l", "log file path. Defualt is \"./log\"", v => Config.LogFilePath = v },
            { "b", "target board name. Defualt is \"Marginalman\"", v => Config.BoardName = v },
            { "i={=>}", "start and end index", (s, e) =>{
                Config.StartIndex = int.Parse(s);
                Config.EndIndex = int.Parse(e);
            }},
            { "a", "article id. Defualt is \"M.1555662923.A.C9F\"", v => Config.ArticleId = v },
            { "v|?|version", "show program's version number and exit", v => IsShowVersion = v != null },
        };
    }
}
