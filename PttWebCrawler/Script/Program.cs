using Crawler;
using Options;

namespace PttWebCrawler
{
    class Program
    {
        private static CrawlerController _Crawler = null;

        public static void Main(string[] args)
        {
            ParseArguments(args);
            Initializae();

            //_Crawler.Crawl();
            _Crawler.CrawlByArticleId();
        }

        private static void ParseArguments(string[] args)
        {
            OptionsParser.Parse(args);
        }

        private static void Initializae()
        {
            _Crawler = CrawlerController.Instance;
        }
    }
}
