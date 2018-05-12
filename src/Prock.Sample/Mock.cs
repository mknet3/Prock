using Prock.Core;

namespace Prock.Sample
{
    public class Mock : IMock
    {
        public Mock()
        {
            this.Json = "{}";
            this.StatusCode = 200;
            this.ContentType = "application/json";
        }

        public string Route { get; set; }

        public string FilePath { get; set; }

        public string Json { get; set; }

        public int StatusCode { get; set; }

        public string ContentType { get; set; }
    }
}
