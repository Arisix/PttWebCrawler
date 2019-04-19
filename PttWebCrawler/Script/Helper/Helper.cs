using Options;
using System;

namespace Crawler
{
    class Helper
    {
        public static string GetBoradPageUrl(int index = -1)
        {
            string result = Config.PttHotUrlBase;

            result += Config.BoardName;
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

        public static string GetArticlePageUrl(string articleId = null)
        {
            string result = Config.PttHotUrlBase;

            result += string.Format("{0}/",Config.BoardName);
            if(articleId == null)
            {
                result += Config.ArticleId;
            }
            else
            {
                result += articleId;
            }
            result += ".html";

            return result;
        }

        public static string OutputPath(int startIndex, int endIndex)
        {
            return string.Format("Marginalman_{0}to{1}_{2}.json", startIndex, endIndex, DateTime.Now.ToFileTimeUtc());
        }

        public static string OutputPath(string articleId)
        {
            return string.Format("Marginalman_{0}_{1}.json", articleId, DateTime.Now.ToFileTimeUtc());
        }
    }
}
