using System;

namespace WebCrawler.Logic
{
    public class PerformanceResultDto
    {
        public string Url { get; set; }
        public int ResponseTime { get; set; }
        public bool InSitemap { get; set; }
        public bool InWebsite { get; set; }
    }
}
