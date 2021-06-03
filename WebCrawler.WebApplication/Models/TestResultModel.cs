using System.Collections.Generic;
using WebCrawler.Logic;

namespace WebCrawler.WebApplication.Models
{
    public class TestResultModel
    {
        public string Website { get; set; }
        public List<PerformanceResultDTO> Performance { get; set; }
        public List<string> OnlySitemapUrls { get; set; }
        public List<string> OnlyWebsiteUrls { get; set; }
    }
}
