using System.Collections.Generic;
using WebCrawler.Data;
using WebCrawler.Logic;

namespace WebCrawler.WebApplication.Models
{
    public class TestResultModel
    {
        public string Website { get; set; }
        public List<PerformanceResult> Performance { get; set; }
        public List<string> UrlsFoundOnlyInSitemap { get; set; }
        public List<string> UrlsFoundOnlyInWebsite { get; set; }
    }
}
