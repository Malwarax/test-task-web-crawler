using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class WebCrawlerApp
    {
        private readonly DbWorker _dbWorker;
        private readonly WebsiteCrawler _websiteCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly PerformanceEvaluationGetter _performanceEvaluationGetter;
        private readonly UserInteractionService _userInteractionService;

        public WebCrawlerApp(DbWorker dbWorker, WebsiteCrawler websiteCrawler, SitemapCrawler sitemapCrawler, PerformanceEvaluationGetter performanceEvaluationGetter,  UserInteractionService userInteractionService)
        {
            _dbWorker = dbWorker;
            _websiteCrawler = websiteCrawler;
            _sitemapCrawler = sitemapCrawler;
            _performanceEvaluationGetter = performanceEvaluationGetter;
            _userInteractionService = userInteractionService;
        }

        public void Start()
        {
            var websiteUrl = _userInteractionService.GetUserInput();

            Console.WriteLine("Crawling website. It will take some time...");
            var websiteUrls = _websiteCrawler.Crawl(websiteUrl);
            var sitemapUrls = _sitemapCrawler.Crawl(websiteUrl);

            Console.WriteLine("Response time processing. It will take some time...");
            var performanceEvaluationResult = _performanceEvaluationGetter.PrepareLinks(websiteUrls, sitemapUrls);

            _userInteractionService.PrintUrlsDifference(performanceEvaluationResult);
            _userInteractionService.PrintPerformanceResultTable(performanceEvaluationResult);

            Console.WriteLine("Saving result...");
            _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult).Wait();

            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
