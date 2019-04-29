using System.Net;

namespace Crawler
{
    class CookiesClient : WebClient
    {
        private static CookiesClient _Instance = null;
        public static CookiesClient Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CookiesClient();
                }
                return _Instance;
            }
        }

        private CookiesClient()
        {
            Headers.Add(HttpRequestHeader.Cookie, "over18=1;");
        }
    }
}
