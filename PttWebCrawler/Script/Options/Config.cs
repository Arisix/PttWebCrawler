namespace Options
{
    public sealed class Config
    {
        public static string LogFilePath = "./log";

        public readonly static string PttUrlBase = "https://www.ptt.cc/";
        public readonly static string PttHotUrlBase = "https://www.ptt.cc/bbs/";
        public readonly static string indexString = "/index.html";

        public static string BoardName = "Marginalman";
        public static int StartIndex = -1;
        public static int EndIndex = -1;
        public static string ArticleId = "M.1555662923.A.C9F";
        public static string Version = "1.0.0";
    }
}