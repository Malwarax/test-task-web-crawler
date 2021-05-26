using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebCrawler.Logic
{
    public class PageDownloader
    {
        public virtual string GetPage(Uri websiteUri)
        {
            WebClient client = new WebClient();
            string page = "";
            try
            {
                using (client)
                {
                    page = client.DownloadString(websiteUri);
                }
            }
            catch
            {
                Console.WriteLine($"Error with downloading page from {websiteUri}");
            }
            return page;
        }
    }
}
