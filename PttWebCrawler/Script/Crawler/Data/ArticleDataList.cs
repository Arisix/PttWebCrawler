using System.Collections.Generic;

namespace Crawler
{
    public class ArticleDataList
    {
        private List<ArticleData> ArticleList = new List<ArticleData>();

        public void Add(ArticleData data)
        {
            ArticleList.Add(data);
        }

        public void Reset()
        {
            ArticleList = new List<ArticleData>();
        }

        public List<ArticleData> GetList()
        {
            return ArticleList;
        }
    }
}
