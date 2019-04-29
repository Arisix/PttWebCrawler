namespace Crawler
{
    public class PushData
    {
        public PushType type { get; private set; }
        public string userId { get; private set; }
        public string content { get; private set; }
        public string ipDateTime { get; private set; }

        public PushData(PushType type, string userId, string content, string ipDateTime)
        {
            this.type = type;
            this.userId = userId;
            this.content = content;
            this.ipDateTime = ipDateTime;
        }
    }
}
