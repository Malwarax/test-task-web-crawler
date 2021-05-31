using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WebCrawler.Logic
{
    public class SitemapLinkReceiver
    {

        virtual public Uri GetSitemapUri(Uri baseUri, string robots)
        {
            Uri sitemapUri;
            Uri robotsTxtUri = new Uri(baseUri, "/robots.txt");

            var sitemapString = robots.Split('\n')
            .Select(x => x.Trim())
            .Where(x => x.StartsWith("Sitemap:"))
            .FirstOrDefault();

            if (!String.IsNullOrEmpty(sitemapString))
            {
                sitemapString = sitemapString
                .Replace("Sitemap:", "")
                .Trim();
                sitemapUri = new Uri(sitemapString);
            }
            else
            {
                sitemapUri = new Uri(baseUri, "/sitemap.xml");
            }

            return sitemapUri;
        }
    }
}
