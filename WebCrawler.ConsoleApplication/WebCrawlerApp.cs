using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class WebCrawlerApp
    {
        public void Start()
        {
            var validator = new InputValidator();
            var websiteCrawler = new WebsiteCrawler();
            var sitemapCrawler = new SitemapCrawler();

            var differencePrinter = new LinksDifferencePrinter();
            var responcePrinter = new ResponsePrinter();

            var performanceEvaluationGetter = new PerformanceEvaluationGetter();

            bool inputResult = false;
            Uri WebsiteUrl = null; ;

            while (inputResult == false)
            {
                Console.WriteLine(@"Enter the website url (e.g. https://www.example.com/):");
                string websiteLink = Console.ReadLine();
                var validationResult = validator.Validate(websiteLink, new UrlValidator(), new RedirectionValidator());

                if (!validationResult)
                {
                    inputResult = false;
                }
                else
                {
                    WebsiteUrl = new Uri(websiteLink);
                    inputResult = true;
                }

            }

            Console.WriteLine("Crawling website. It will take some time...");
            var websiteLinks = websiteCrawler.Crawl(WebsiteUrl, new PageDownloader(), new PageParser());

            Console.WriteLine("Crawling sitemap. It will take some time...");
            var sitemapLinks = sitemapCrawler.Crawl(WebsiteUrl, new SitemapLinkReceiver(), new PageDownloader(), new SitemapParser());

            differencePrinter.PrintDifference(sitemapLinks, websiteLinks);

            Console.WriteLine("Response time processing. It will take some time...");
            var combinedLinks = sitemapLinks.Union(websiteLinks).ToList();
            responcePrinter.PrintTable(performanceEvaluationGetter.PrepareLinks(combinedLinks, new PerformanceEvaluator()));

            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
        }
       
    }
}
