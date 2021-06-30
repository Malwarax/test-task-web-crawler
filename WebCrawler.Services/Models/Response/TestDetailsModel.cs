using System.Collections.Generic;

namespace WebCrawler.Services.Models.Response
{
    public class TestDetailsModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public IEnumerable<PerformanceResultModel> Results { get; set; }
    }
}
