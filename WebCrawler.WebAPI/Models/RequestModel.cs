using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.WebAPI.Models
{
    public class RequestModel
    {
        /// <summary>
        /// Get urls which were found in sitemap 
        /// </summary>
        public bool sitemap { get; set; }
        /// <summary>
        /// Get urls which were found in website 
        /// </summary>
        public bool website { get; set; }
    }
}
