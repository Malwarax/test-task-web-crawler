using System;

namespace WebCrawler.Logic
{
    public class PerformanceResultDTO
    {
        public string Link { get; set; }
        public TimeSpan ResponseTime { get; set; }
    }
}
