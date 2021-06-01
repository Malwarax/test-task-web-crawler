using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Data
{
    public class OnlySitemapUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
