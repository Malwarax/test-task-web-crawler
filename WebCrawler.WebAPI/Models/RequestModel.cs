using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCrawler.WebAPI.Models
{
    public class RequestModel
    {
        /// <summary>
        /// Get url which was found in sitemap 
        /// </summary>
        public bool InSitemap { get; set; }
        /// <summary>
        /// Get url which was found in website 
        /// </summary>
        public bool InWebsite { get; set; }
    }
}
