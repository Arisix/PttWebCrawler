using Logger;
using Newtonsoft.Json;
using Options;
using Patterns;

namespace Crawler
{
    class CrawlerController : Singleton<CrawlerController>
    {
        private CookiesClient _WebClient = null;
        private LoggerManager _Logger = null;
        private ArticleCrawler _ArticleCrawler = null;
        private BoardCrawler _BoardCrawler = null;
        private ResultWriter _Writer = null;

        public override void Initialize()
        {
            _WebClient = CookiesClient.Instance;
            _Logger = LoggerManager.Instance;
            _ArticleCrawler = ArticleCrawler.Instance;
            _BoardCrawler = BoardCrawler.Instance;
            _Writer = ResultWriter.Instance;
        }

        public void Crawl()
        {
            if (Config.StartIndex >= 0)
            {
                if (Config.EndIndex < Config.StartIndex)
                {
                    Config.EndIndex = _BoardCrawler.GetBoardLastPageNumber();
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
            int now = Config.StartIndex;
            int end = Config.EndIndex;
            string board = Config.BoardName;

            _Writer.Write("{\"ArticleList\":[");

            while(now <= end)
            {
                var list = _BoardCrawler.GetAriticleIdInPage(board, now);

                for (int i = 0; i < list.Count; i++)
                {
                    ArticleData data = _ArticleCrawler.Crawl(board, list[i]);
                    var json = JsonConvert.SerializeObject(data);
                    if((now == end) && (i == list.Count - 1))
                    {
                        _Writer.Write(json);
                    }
                    else
                    {
                        string output = string.Format("{0},", json);
                        _Writer.Write(output);
                    }
                }
                now++;
            }

            _Writer.Write("]}");
        }

        private void CrawlByArticleId(string boardName = null, string articleId = null)
        {
            ArticleData data = _ArticleCrawler.Crawl(boardName, articleId);
            var json = JsonConvert.SerializeObject(data);
            _Writer.Write(json);
        }
    }
}
