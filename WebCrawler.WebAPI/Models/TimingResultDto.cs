using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.WebAPI.Models
{
    public class TimingResultDto
    {
        public string Url { get; set; }
        public int? ResponseTime { get; set; }
    }
}
