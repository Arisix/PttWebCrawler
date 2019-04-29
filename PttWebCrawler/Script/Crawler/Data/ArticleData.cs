using System.Collections.Generic;

namespace Crawler
{
    public class ArticleData
    {
        public string url { get; private set; }
        public string board { get; private set; }
        public string articleId { get; private set; }
        public string title { get; private set; }
        public string author { get; private set; }
        public string date { get; private set; }
        public string content { get; private set; }
        public string ip { get; private set; }
        public List<PushData> messages { get; private set; }
        public int messageCount { get; private set; }

        public ArticleData(string url, string board, string articleId,
            string title,string author, string date,
            string content, string ip, List<PushData> messages, int messageCount)
        {
            this.url = url;
            this.board = board;
            this.articleId = articleId;
            this.title = title;
            this.author = author;
            this.date = date;
            this.content = content;
            this.ip = ip;
            this.messages = messages;
            this.messageCount = messageCount;
        }
    }
}
