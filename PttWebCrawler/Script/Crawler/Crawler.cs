using Options;
using Patterns;
using HtmlAgilityPack;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Logger;

namespace Crawler
{
    class CrawlerAgent : Singleton<CrawlerAgent>
    {
        private CookiesClient _WebClient = null;
        private LoggerManager _Logger = null;

        public override void Initialize()
        {
            _WebClient = CookiesClient.Instance;
            _Logger = LoggerManager.Instance;
        }

        public void Crawl()
        {
            if (Config.StartIndex >= 0)
            {
                if (Config.EndIndex < Config.StartIndex)
                {
                    Config.EndIndex = GetLastBoardPageNumber();
                }

                CrawlByBoardIndex();
            }
            else
            {
                CrawlByArticleId();
            }
        }

        private void CrawlByBoardIndex()
        {

        }

        public void CrawlByArticleId(string articleId = null)
        {
            if(string.IsNullOrEmpty(articleId))
            {
                articleId = Config.ArticleId;
            }
            _Logger.Debug("Processing article : " + articleId);
            GetArticlePage(articleId);

        }

        private int GetLastBoardPageNumber()
        {
            int result;

            HtmlDocument data = GetBoardPage();
            string pattern = string.Format("href=\"/bbs/{0}/index([0-9]+).html\">&lsaquo;", Config.BoardName);
            var match = Regex.Match(data.DocumentNode.InnerHtml, pattern);

            result = int.Parse(match.Groups[1].Value);

            return result;
        }

        private HtmlDocument GetBoardPage(int index = -1)
        {
            string url = Helper.GetBoradPageUrl(index);
            MemoryStream ms = new MemoryStream(_WebClient.DownloadData(url));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            return doc;
        }

        private HtmlDocument GetArticlePage(string articleId = null)
        {
            if(articleId == null)
            {
                articleId = Config.ArticleId;
            }

            string url = Helper.GetArticlePageUrl(articleId);
            MemoryStream ms = new MemoryStream(_WebClient.DownloadData(url));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);

            return doc;
        }
    }
}
