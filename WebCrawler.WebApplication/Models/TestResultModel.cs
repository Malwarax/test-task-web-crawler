using System.Collections.Generic;
using WebCrawler.Data;
using WebCrawler.Logic;

namespace WebCrawler.WebApplication.Models
{
    public class TestResultModel
    {
        public string Url { get; set; }
        public IEnumerable<PerformanceResult> Performance { get; set; }
        public IEnumerable<string> UrlsFoundOnlyInSitemap { get; set; }
        public IEnumerable<string> UrlsFoundOnlyInWebsite { get; set; }
    }
}
