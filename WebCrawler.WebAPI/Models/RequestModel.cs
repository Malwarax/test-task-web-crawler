using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.WebAPI.Models
{
    public class RequestModel
    {
        /// <summary>
        /// Get urls which were found in sitemap or website category 
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// Rows per page
        /// </summary>
        public int perPage { get; set; }
    }
}
