using Options;
using System;

namespace Crawler
{
    class Helper
    {
        public static string GetBoradPageUrl(string boardName = null, int index = -1)
        {
            string result = Config.PttHotUrlBase;

            result += string.IsNullOrEmpty(boardName) ? Config.BoardName : boardName;
            if(index <= 0)
            {
                result += Config.indexString;
            }
            else
            {
                result += string.Format("/index{0}.html", index);
            }

            return result;
        }

        public static string GetArticlePageUrl(string boardName = null, string articleId = null)
        {
            string result = Config.PttHotUrlBase;

            result += string.Format("{0}/",string.IsNullOrEmpty(boardName) ? Config.BoardName : boardName);
            result += string.IsNullOrEmpty(articleId) ?  Config.ArticleId : articleId;
            result += ".html";

            return result;
        }

        public static string GetOutputPath()
        {
            if(Config.StartIndex > 0)
            {
                return string.Format("Marginalman_{0}_{1}.json", Config.StartIndex, Config.EndIndex);
            }

            return string.Format("Marginalman_{0}.json", Config.ArticleId);
        }
    }
}
