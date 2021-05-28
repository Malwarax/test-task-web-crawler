using System;

namespace WebCrawler.Data
{
    public class PerformanceResult
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public TimeSpan ResponseTime { get; set; }

        public int WebsiteId { get; set; }
        public Website Website { get; set; }
    }
}
