namespace Crawler
{
    public enum PushType
    {
        good = 0,
        bad,
        normal
    }
    
    public static class PushTypeExtensions
    {
        public static string ToStaticString(this PushType type)
        {
            string result = string.Empty;
            switch(type)
            {
                case PushType.good:
                    result = "推";
                    break;
                case PushType.bad:
                    result = "噓";
                    break;
                case PushType.normal:
                    result = "→";
                    break;
            }

            return result;
        }
    }
}
