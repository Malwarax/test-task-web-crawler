using System;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.Services
{
    public class CrawlerService
    {
        private readonly DbWorker _dbWorker;
        private readonly WebsiteCrawler _websiteCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly PerformanceEvaluationGetter _performanceEvaluationGetter;
        private readonly InputValidator _inputValidator;

        public CrawlerService(DbWorker dbWorker, WebsiteCrawler websiteCrawler, SitemapCrawler sitemapCrawler, PerformanceEvaluationGetter performanceEvaluationGetter, InputValidator inputValidator)
        {
            _dbWorker = dbWorker;
            _websiteCrawler = websiteCrawler;
            _sitemapCrawler = sitemapCrawler;
            _performanceEvaluationGetter = performanceEvaluationGetter;
            _inputValidator = inputValidator;
        }

        public ResponseModel Crawl(string url)
        {
            string errors;
            var inputParametersAreValid = _inputValidator.InputParameters(url, out errors);
            if (!inputParametersAreValid)
            {
                return new ResponseModel { IsSuccessful = false, Errors = errors };
            }

            var websiteUrl = new Uri(url);
            var websiteUrls = _websiteCrawler.Crawl(websiteUrl);
            var sitemapUrls = _sitemapCrawler.Crawl(websiteUrl);
            var performanceEvaluationResult = _performanceEvaluationGetter.PrepareLinks(websiteUrls, sitemapUrls);

            return new ResponseModel
            {
                Result = _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult).Result
            };
        }
    }
}
