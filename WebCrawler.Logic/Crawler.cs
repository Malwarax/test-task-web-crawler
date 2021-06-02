using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Logic
{
    public class Crawler
    {
        private readonly WebsiteCrawler _websiteCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly PerformanceEvaluationGetter _performanceEvaluationGetter;
        private List<Uri> websiteLinks;
        private List<Uri> sitemapLinks;

        public Crawler()
        {
            _websiteCrawler = new WebsiteCrawler(new PageDownloader(), new PageParser());
            _sitemapCrawler = new SitemapCrawler(new PageDownloader(), new SitemapLinkReceiver(), new SitemapParser());
            _performanceEvaluationGetter = new PerformanceEvaluationGetter(new PerformanceEvaluator());
        }

        public void Crawl(Uri url)
        {
            websiteLinks = _websiteCrawler.Crawl(url);
            sitemapLinks = _sitemapCrawler.Crawl(url);
        }

        public List<Uri> GetOnlySitemapUrls()
        {
            return GetUniqueLinks(sitemapLinks, websiteLinks);
        }

        public List<Uri> GetOnlyWebsiteUrls()
        {
            return GetUniqueLinks(websiteLinks, sitemapLinks);
        }

        public List<PerformanceResultDTO> GetPerformance()
        {
            var combinedLinks = sitemapLinks.Union(websiteLinks).ToList();
            return _performanceEvaluationGetter.PrepareLinks(combinedLinks);
        }

        private List<Uri> GetUniqueLinks(List<Uri> baseLinks, List<Uri> linksToExcept)
        {
            return baseLinks
            .Except(linksToExcept)
            .ToList();
        }
    }
}
