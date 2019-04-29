using Patterns;
using System.IO;

namespace Crawler
{
    public class ResultWriter : Singleton<ResultWriter>
    {
        private StreamWriter _Writer = null;
        public override void Initialize()
        {
            _Writer = new StreamWriter(Helper.GetOutputPath());
            _Writer.AutoFlush = true;
        }

        public void Write(string output)
        {
            _Writer.Write(output);
        }
    }
}
