using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.WebApplication.Models
{
    public class TestResultModel
    {
        public string Website { get; set; }
        public List<PerformanceResultModel> Performance { get; set; }
        public List<string> OnlySitemapUrls { get; set; }
        public List<string> OnlyWebsiteUrls { get; set; }
    }
}
