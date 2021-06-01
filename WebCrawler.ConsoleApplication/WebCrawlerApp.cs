using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class WebCrawlerApp
    {
        private readonly DbWorker _dbWorker;

        public WebCrawlerApp(DbWorker dbWorker)
        {
            _dbWorker = dbWorker;
        }

        public void Start()
        {
            var consoleWrapper = new ConsoleWrapper();
            var pageDownloader = new PageDownloader();

            var urlValidator=new UrlValidator(consoleWrapper);
            var redirectionValidator=new RedirectionValidator(consoleWrapper);

            var pageParser = new PageParser();
            var websiteCrawler = new WebsiteCrawler(pageDownloader, pageParser);

            var sitemapLinkReceiver = new SitemapLinkReceiver();
            var sitemapParser = new SitemapParser();
            var sitemapCrawler = new SitemapCrawler(pageDownloader, sitemapLinkReceiver, sitemapParser);

            var differencePrinter = new LinksDifferencePrinter();
            var responcePrinter = new ResponsePrinter();

            var performanceEvaluationGetter = new PerformanceEvaluationGetter(new PerformanceEvaluator());

            bool isThisUrlHasValidationErrors;
            Uri websiteUrl = null;

            do
            {
                Console.WriteLine(@"Enter the website url (e.g. https://www.example.com/):");
                string url = Console.ReadLine();
                isThisUrlHasValidationErrors = urlValidator.CheckUrl(url) == false || redirectionValidator.CheckRedirection(url) == false;

                if (!isThisUrlHasValidationErrors)
                { 
                    websiteUrl = new Uri(url);
                }

            } 
            while (isThisUrlHasValidationErrors);

            Console.WriteLine("Crawling website. It will take some time...");
            var websiteLinks = websiteCrawler.Crawl(websiteUrl);

            Console.WriteLine("Crawling sitemap. It will take some time...");
            var sitemapLinks = sitemapCrawler.Crawl(websiteUrl);
            var onlySitemapLinks = GetUniqueLinks(sitemapLinks, websiteLinks);
            var onlyWebsiteLinks = GetUniqueLinks(websiteLinks, sitemapLinks);

            differencePrinter.PrintDifference(sitemapLinks.Count, websiteLinks.Count, onlySitemapLinks, onlyWebsiteLinks);

            Console.WriteLine("Response time processing. It will take some time...");
            var combinedLinks = sitemapLinks.Union(websiteLinks).ToList();
            var performanceEvaluationResult = performanceEvaluationGetter.PrepareLinks(combinedLinks);
            responcePrinter.PrintTable(performanceEvaluationResult);

            Console.WriteLine("Saving result...");
            _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult, onlySitemapLinks, onlyWebsiteLinks);

            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private List<Uri> GetUniqueLinks(List<Uri> baseLinks, List<Uri> linksToExcept)
        {
            return baseLinks
            .Except(linksToExcept)
            .ToList();
        }
    }
}
