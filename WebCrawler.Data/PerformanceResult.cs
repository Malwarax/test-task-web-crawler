using System;

namespace WebCrawler.Data
{
    public class PerformanceResult
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? ResponseTime { get; set; }
        public bool? InSitemap { get; set; }
        public bool? InWebsite { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
