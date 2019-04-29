using HtmlAgilityPack;
using Logger;
using Options;
using Patterns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawler
{
    class BoardCrawler : Singleton<BoardCrawler>
    {
        private ILoggerManager _Logger = null;
        private CookiesClient _WebClient = null;

        public override void Initialize()
        {
            _Logger = LoggerManager.Instance;
            _WebClient = CookiesClient.Instance;
        }

        public int GetBoardLastPageNumber(string boardName = null)
        {
            if(string.IsNullOrEmpty(boardName))
            {
                boardName = Config.BoardName;
            }

            int result;

            HtmlDocument data = GetBoardPage(boardName);
            string pattern = string.Format("href=\"/bbs/{0}/index([0-9]+).html\">&lsaquo;", boardName);
            var match = Regex.Match(data.DocumentNode.InnerHtml, pattern);

            result = int.Parse(match.Groups[1].Value);

            return result;
        }

        public List<string> GetAriticleIdInPage(string boardName, int index)
        {
            _Logger.Debug(string.Format("Processing page : {0}", index));

            List<string> result = new List<string>();

            var PageHtml = GetBoardPage(boardName, index);
            var divs = PageHtml.QuerySelectorAll("div.r-ent");
            foreach(var div in divs)
            {
                string href = string.Empty;
                try
                {
                    href = div.QuerySelector("a").GetAttributeValue("href", string.Empty);
                    string[] datas = href.Split('/');

                    string articleId = datas[datas.Length - 1].Replace(".html", string.Empty);

                    result.Add(articleId);
                    _Logger.Debug(string.Format("Get {0} in Page {1}", articleId, index));
                }
                catch(NullReferenceException)
                {
                    _Logger.Error(string.Format("Can not find article link in Page {0}.", index));
                }
            }

            return result;
        }

        private HtmlDocument GetBoardPage(string boardName, int index = -1)
        {
            string url = Helper.GetBoradPageUrl(boardName, index);
            MemoryStream ms = new MemoryStream(_WebClient.DownloadData(url));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            return doc;
        }
    }
}
