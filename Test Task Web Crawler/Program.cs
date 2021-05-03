using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Test_Task_Web_Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(@" Enter the website url (e.g. https://www.example.com/): ");
            string websiteLink=Console.ReadLine();

            Uri websiteUri = null;

            bool result = Uri.TryCreate(websiteLink, UriKind.Absolute, out websiteUri)
            && (websiteUri.Scheme == Uri.UriSchemeHttps || websiteUri.Scheme == Uri.UriSchemeHttp);

            if (result == false )
            {
                Console.WriteLine(" Invalid URL.");
                Console.Read();
                return;
            }

            if(CheckRedirection(websiteUri)==false)
            {
                Console.WriteLine(" Error. The server is redirecting the request for this url.");
                Console.Read();
                return;
            }
            

            try 
            {
                var crawler = new Crawler();

                Console.WriteLine(" It will take some time...");

                List<Uri> manuallyCrawledLinks = crawler.CrawlWebsiteManually(websiteUri);
                List<Uri> sitemapLinks = crawler.CrawlSitemap(websiteUri);

                Console.WriteLine("\n Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:");
                PrintUniqueLinks(sitemapLinks, manuallyCrawledLinks);

                Console.WriteLine("\n Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
                PrintUniqueLinks(manuallyCrawledLinks, sitemapLinks);

                Console.WriteLine("\n Performance test result:");
                Console.WriteLine(" It will take some time...");
                PrintLinksResponseTime(sitemapLinks.Union(manuallyCrawledLinks).ToList());

                Console.WriteLine($"\n Urls found after crawling a website: {manuallyCrawledLinks.Count}");
                Console.WriteLine($"\n Urls found in sitemap: {sitemapLinks.Count}");
            }
            catch
            {
                Console.WriteLine(" Something went wrong...");
            }

            Console.Read();
        }

        private static void PrintUniqueLinks(List<Uri> links1, List<Uri> links2)
        {
            List<Uri> uniqueLinks = links1.Except(links2).ToList();
            PrintLinks(uniqueLinks);
        }

        private static void PrintLinks(List<Uri> links)
        {
            if (links.Count == 0)
            {
                Console.WriteLine(" Nothing to print.");
            }
            else
            {
                for (int i = 1; i <= links.Count(); i++)
                {
                    Console.WriteLine($" {i}) {links[i - 1]}");
                }
            }
        }

        private static void PrintLinksResponseTime(List<Uri> links)
        {
            var performanceEvaluator = new PerformanceEvaluator();

            List<PerformanceResult> performanceResult = performanceEvaluator.GetLinksResponseTime(links);

            var table = new ConsoleTable("№", "Url", "Timing(ms)");

            for (int i = 1; i <= performanceResult.Count(); i++)
            {
                table.AddRow(i, performanceResult[i - 1].Link, performanceResult[i - 1].ResponseTime.Milliseconds+" ms");
            }
            table.Options.EnableCount = false;

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            table.Write();
        }

        private static bool CheckRedirection(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "HEAD";
            request.AllowAutoRedirect = false;
            bool result=true;
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                }
            }
            catch
            {
                result = false;
            }
            return result;            
        }
    }
}
