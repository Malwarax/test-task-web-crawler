using System;
using System.Collections.Generic;

namespace WebCrawler.Logic
{
    public class SitemapCrawler 
    {
        private readonly SitemapLinkReceiver _linkReceiver;
        private readonly PageDownloader _pageDownloader;
        private readonly SitemapParser _sitemapParser;

        public SitemapCrawler(PageDownloader pageDownloader, SitemapLinkReceiver linkReceiver, SitemapParser sitemapParser)
        {
            _linkReceiver = linkReceiver;
            _pageDownloader = pageDownloader;
            _sitemapParser = sitemapParser;
        }

        virtual public List<Uri> Crawl(Uri websiteUri)
        {
            string robots = _pageDownloader.GetPage(new Uri(websiteUri, "/robots.txt"));
            Uri sitemapUri = _linkReceiver.GetSitemapUri(websiteUri,robots);
            string sitemap = _pageDownloader.GetPage(sitemapUri);
            List<Uri> crawledLinks = _sitemapParser.GetLinks(sitemap, websiteUri);

            return crawledLinks;
        }
    }
}
