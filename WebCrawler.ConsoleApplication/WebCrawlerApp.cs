using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;

namespace WebCrawler.ConsoleApplication
{
    public class WebCrawlerApp
    {
        private readonly DbWorker _dbWorker;
        private readonly WebsiteCrawler _websiteCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly PerformanceEvaluationGetter _performanceEvaluationGetter;
        private readonly LinksDifferencePrinter _linksDifferencePrinter;
        private readonly ResponsePrinter _responsePrinter;
        private readonly InputValidator _inputValidator;

       public WebCrawlerApp(DbWorker dbWorker, WebsiteCrawler websiteCrawler, SitemapCrawler sitemapCrawler, PerformanceEvaluationGetter performanceEvaluationGetter, LinksDifferencePrinter linksDifferencePrinter, ResponsePrinter responsePrinter, InputValidator inputValidator)
        {
            _dbWorker = dbWorker;
            _websiteCrawler = websiteCrawler;
            _sitemapCrawler = sitemapCrawler;
            _performanceEvaluationGetter = performanceEvaluationGetter;
            _linksDifferencePrinter = linksDifferencePrinter;
            _responsePrinter = responsePrinter;
            _inputValidator = inputValidator;
        }

        public void Start()
        {
            Uri websiteUrl;
            while (true)
            {
                Console.WriteLine(@"Enter the website url e.g. https://www.example.com/ (Enter to exit):");
                string url = Console.ReadLine();

                var userPressedEnter = String.IsNullOrEmpty(url);
                if (userPressedEnter)
                {
                    Environment.Exit(0);
                }

                string errors;
                var inputParamtersAreValid = _inputValidator.InputParameters(url, out errors);
                if (!inputParamtersAreValid)
                {
                    Console.WriteLine(errors);
                    continue;
                }

                websiteUrl = new Uri(url);
                break;
            }

            Console.WriteLine("Crawling website. It will take some time...");
            var websiteUrls = _websiteCrawler.Crawl(websiteUrl);
            var sitemapUrls = _sitemapCrawler.Crawl(websiteUrl);

            Console.WriteLine("Response time processing. It will take some time...");
            var performanceEvaluationResult = _performanceEvaluationGetter.PrepareLinks(websiteUrls, sitemapUrls);

            _linksDifferencePrinter.PrintDifference(performanceEvaluationResult);
            _responsePrinter.PrintTable(performanceEvaluationResult);

            Console.WriteLine("Saving result...");
            _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult).Wait();

            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
