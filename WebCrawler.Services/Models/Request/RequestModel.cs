namespace WebCrawler.Services.Models.Request
{
    public class RequestModel
    {
        /// <summary>
        /// Get urls which were found in sitemap 
        /// </summary>
        public bool InSitemap { get; set; }
        /// <summary>
        /// Get urls which were found in website 
        /// </summary>
        public bool InWebsite { get; set; }
        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Rows per page
        /// </summary>
        public int PageSize { get; set; }
    }
}
