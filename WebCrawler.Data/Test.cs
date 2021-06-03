using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Data
{
    public class Test
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime? Date { get; set; }

        public List<PerformanceResult> PerformanceResults { get; set; }
    }
}
