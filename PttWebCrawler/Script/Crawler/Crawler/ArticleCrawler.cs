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
    class ArticleCrawler : Singleton<ArticleCrawler>
    {
        private ILoggerManager _Logger = null;
        private CookiesClient _WebClient = null;

        public override void Initialize()
        {
            _Logger = LoggerManager.Instance;
            _WebClient = CookiesClient.Instance;
        }

        public ArticleData Crawl(string boardName = null,string articleId = null)
        {
            if (string.IsNullOrEmpty(articleId))
            {
                articleId = Config.ArticleId;
            }
            if(string.IsNullOrEmpty(boardName))
            {
                boardName = Config.BoardName;
            }

            #region Prepare to Create Data
            ArticleData result = null;
            string url = string.Empty;
            string board = string.Empty;
            string Id = string.Empty;
            string title = string.Empty;
            string author = string.Empty;
            string date = string.Empty;
            string content = string.Empty;
            string ip = string.Empty;
            List<PushData> messageList = new List<PushData>(); 
            int messageCount = 0;
            #endregion

            _Logger.Debug("Processing article : " + articleId);
            HtmlDocument doc = GetArticlePage(boardName, articleId);

            var mainContent = doc.QuerySelectorAll("div#main-content");
            var mainContentText = mainContent[0].InnerText;

            #region Url Id
            url = Helper.GetArticlePageUrl(boardName, articleId);
            Id = articleId;
            #endregion

            #region Author Title Date
            var metas = mainContent.QuerySelectorAll("div.article-metaline");
            if (metas.Count >= 3)
            {
                author = metas[0].QuerySelector("span.article-meta-value").InnerText;
                title = metas[1].QuerySelector("span.article-meta-value").InnerText;
                date = metas[2].QuerySelector("span.article-meta-value").InnerText;
            }
            #endregion

            #region Board
            var metaright = mainContent.QuerySelectorAll("div.article-metaline-right");
            if(metaright.Count > 0)
            {
                var boardSpan = metaright.QuerySelectorAll("span.article-meta-value");
                if(boardSpan.Count > 0)
                {
                    board = boardSpan[0].InnerText;
                }
            }
            #endregion

            #region IP
            var ipText = mainContent.QuerySelectorAll("span.f2");
            if(ipText.Count > 0)
            {
                string pattern = "※ 發信站:.*來自: ([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})";
                var group = Regex.Match(ipText[0].InnerText, pattern).Groups;
                if(group.Count > 1)
                {
                    ip = Regex.Match(ipText[0].InnerText, pattern).Groups[1].Value;
                }
            }
            #endregion

            #region Pushes
            var pushes = mainContent.QuerySelectorAll("div.push");

            foreach(var push in pushes)
            {
                var tagText = push.QuerySelector("span.push-tag").InnerText.Trim();
                PushType tag = PushType.normal;
                if(tagText.Equals("推"))
                {
                    tag = PushType.good;
                    messageCount++;
                }
                else if(tagText.Equals("噓"))
                {
                    tag = PushType.bad;
                    messageCount--;
                }
                
                var userId = push.QuerySelector("span.push-userid").InnerText.Trim();
                var pushConetent = push.QuerySelector("span.push-content").InnerText.Remove(0,1).Trim();
                var ipDateTime = push.QuerySelector("span.push-ipdatetime").InnerText.Trim();

                PushData data = new PushData(tag, userId, pushConetent, ipDateTime);
                messageList.Add(data);
            }
            #endregion

            #region Content
            var PrepareToRemove = new List<HtmlNode>();
            foreach (var node in metas) PrepareToRemove.Add(node);
            foreach(var node in metaright) PrepareToRemove.Add(node);
            foreach (var node in ipText) PrepareToRemove.Add(node);
            foreach (var node in pushes) PrepareToRemove.Add(node);

            foreach (var node in PrepareToRemove)
            {
                var nodeContent = node.InnerText;
                int index = mainContentText.IndexOf(nodeContent);
                if (index > -1)
                {
                    mainContentText = mainContentText.Remove(index, nodeContent.Length);
                }
            }

            string[] lines = mainContentText.Split('\n');
            mainContentText = string.Empty;
            foreach(var line in lines)
            {
                if(line.Equals("--") || string.IsNullOrEmpty(line))
                    continue;

                mainContentText += string.Format("{0}\n", line);
            }

            content = mainContentText;
            #endregion

            result = new ArticleData(url, board, Id, title, author, date, content, ip, messageList, messageCount);

            return result;
        }

        private HtmlDocument GetArticlePage(string boardName, string articleId = null)
        {
            string url = Helper.GetArticlePageUrl(boardName, articleId);
            MemoryStream ms = new MemoryStream(_WebClient.DownloadData(url));
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);

            return doc;
        }
    }
}
