using System;
using WebCrawler.Logic;

namespace WebCrawler.WebApplication.Services
{
    public class CrawlerService
    {
        private readonly DbWorker _dbWorker;
        private readonly WebsiteCrawler _websiteCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly PerformanceEvaluationGetter _performanceEvaluationGetter;

        public CrawlerService(DbWorker dbWorker, WebsiteCrawler websiteCrawler, SitemapCrawler sitemapCrawler, PerformanceEvaluationGetter performanceEvaluationGetter)
        {
            _dbWorker = dbWorker;
            _websiteCrawler = websiteCrawler;
            _sitemapCrawler = sitemapCrawler;
            _performanceEvaluationGetter = performanceEvaluationGetter;
        }

        public void Crawl(string url)
        {
            var websiteUrl = new Uri(url);
            var websiteUrls = _websiteCrawler.Crawl(websiteUrl);
            var sitemapUrls = _sitemapCrawler.Crawl(websiteUrl);
            var performanceEvaluationResult = _performanceEvaluationGetter.PrepareLinks(websiteUrls, sitemapUrls);

            _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult).Wait();
        }
    }
}
