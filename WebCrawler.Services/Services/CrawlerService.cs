using System;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Services.Interfaces;

namespace WebCrawler.Services
{
    public class CrawlerService : ICrawlerService
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

        public async Task<int> Crawl(string url)
        {
            var websiteUrl = new Uri(url);
            var websiteUrls = _websiteCrawler.Crawl(websiteUrl);
            var sitemapUrls = _sitemapCrawler.Crawl(websiteUrl);
            var performanceEvaluationResult = _performanceEvaluationGetter.PrepareLinks(websiteUrls, sitemapUrls);

            return await _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult);
        }
    }
}
