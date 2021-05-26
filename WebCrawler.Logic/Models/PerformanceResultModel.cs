using System;

namespace WebCrawler.Logic
{
    public class PerformanceResultModel
    {
        public string Link { get; set; }
        public TimeSpan ResponseTime { get; set; }
    }
}
