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
            var inputReceiver = new UserInputReceiver(new ConsoleWrapper(), new InputValidator());
            var websiteCrawler = new WebsiteCrawler();
            var sitemapCrawler = new SitemapCrawler();

            var differencePrinter = new LinksDifferencePrinter();
            var responcePrinter = new ResponsePrinter();

            var performanceEvaluationGetter = new PerformanceEvaluationGetter();

            bool inputResult = false;
            
            while(inputResult==false)
                inputResult=inputReceiver.GetUserInput();

            Console.WriteLine("Crawling website. It will take some time...");
            var websiteLinks = websiteCrawler.Crawl(inputReceiver.WebsiteUrl, new PageDownloader(), new PageParser());

            Console.WriteLine("Crawling sitemap. It will take some time...");
            var sitemapLinks = sitemapCrawler.Crawl(inputReceiver.WebsiteUrl, new SitemapLinkReceiver(), new PageDownloader(), new SitemapParser());

            differencePrinter.PrintDifference(sitemapLinks, websiteLinks);

            Console.WriteLine("Response time processing. It will take some time...");
            var combinedLinks = sitemapLinks.Union(websiteLinks).ToList();
            responcePrinter.PrintTable(performanceEvaluationGetter.PrepareLinks(combinedLinks, new PerformanceEvaluator()));

            Console.WriteLine("Enter to exit.");
            Console.ReadLine();
        }
       
    }
}
