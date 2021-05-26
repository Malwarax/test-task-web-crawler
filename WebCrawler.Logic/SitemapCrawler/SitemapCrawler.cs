using System;
using System.Collections.Generic;

namespace WebCrawler.Logic
{
    public class SitemapCrawler 
    {
        virtual public List<Uri> Crawl(Uri websiteUri, SitemapLinkReceiver linkReceiver, PageDownloader pageDownloader, SitemapParser sitemapParser)
        {
            Uri sitemapUri = linkReceiver.GetSitemapUri(websiteUri,pageDownloader);
            string sitemap = pageDownloader.GetPage(sitemapUri);
            List<Uri> crawledLinks = sitemapParser.GetLinks(sitemap, websiteUri);

            return crawledLinks;
        }
    }
}
