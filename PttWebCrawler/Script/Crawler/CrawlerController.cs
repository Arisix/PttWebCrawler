using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Logger;
using Options;
using Patterns;

namespace Crawler
{
    class CrawlerController : Singleton<CrawlerController>
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

            #region Prepare to Create Data
            string url = string.Empty;
            string board = string.Empty;
            string Id = string.Empty;
            string title = string.Empty;
            string author = string.Empty;
            string date = string.Empty;
            string content = string.Empty;
            string ip = string.Empty;
            #endregion

            _Logger.Debug("Processing article : " + articleId);
            HtmlDocument doc = GetArticlePage(articleId);
            var mainContent = doc.QuerySelectorAll("#main-content");
            var mainContentText = mainContent[0].InnerText;


            var metas = mainContent.QuerySelectorAll("div.article-metaline");

            if(metas.Count >= 3)
            {
                author = metas[0].QuerySelector("span.article-meta-value").InnerText;
                title = metas[1].QuerySelector("span.article-meta-value").InnerText;
                date = metas[2].QuerySelector("span.article-meta-value").InnerText;

                Console.WriteLine(author);
                Console.WriteLine(title);
                Console.WriteLine(date);
            }

            string pattern = "※ 發信站:.*來自: ([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})";
            ip = Regex.Match(mainContentText, pattern).Groups[1].Value;
            Console.WriteLine(ip);

            var pushes = mainContent.QuerySelectorAll("div.push");
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
            string url = Helper.GetArticlePageUrl(articleId);
            MemoryStream ms = new MemoryStream(_WebClient.DownloadData(url));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);

            return doc;
        }
    }
}
