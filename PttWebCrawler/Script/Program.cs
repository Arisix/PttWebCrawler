using Crawler;
using Options;
using System.Threading;

namespace PttWebCrawler
{
    class Program
    {
        private static CrawlerController _Crawler = null;

        public static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(250, 250);
            ThreadPool.SetMaxThreads(250, 250);

            ParseArguments(args);
            Initializae();

            _Crawler.Crawl();

            System.Console.ReadKey();
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
