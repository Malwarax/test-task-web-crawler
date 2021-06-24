using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.WebAPI.Models
{
    public class TestDetailsDto
    {
        public int TestId { get; set; }
        public string Url { get; set; }
        public IEnumerable<PerformanceResultModel> Results { get; set; }
    }
}
