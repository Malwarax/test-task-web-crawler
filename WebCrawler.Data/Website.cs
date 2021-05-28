using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Data
{
    public class Website
    {
        public int Id { get; set; }
        public string WebsiteLink { get; set; }

        public List<PerformanceResult> PerformanceResults { get; set; }
    }
}
