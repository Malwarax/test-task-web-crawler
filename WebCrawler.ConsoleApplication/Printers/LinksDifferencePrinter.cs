using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class LinksDifferencePrinter
    {
        public void PrintDifference(List<PerformanceResultDto> result)
        {
            Console.WriteLine($"Urls found after crawling a website: {result.Where(r=>r.InWebsite==true).Count()}");
            Console.WriteLine($"Urls found in sitemap: {result.Where(r => r.InSitemap==true).Count()}");

            Console.WriteLine("Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:");
            PrintLinks(result.Where(r => r.InSitemap==true && r.InWebsite==false).Select(r=>r.Url).ToList());

            Console.WriteLine("Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
            PrintLinks(result.Where(r => r.InSitemap == false && r.InWebsite == true).Select(r => r.Url).ToList());
        }

        private void PrintLinks(List<string> linksToPrint)
        {

            if (linksToPrint.Count == 0)
            {
                Console.WriteLine("Nothing to print");
            }
            else
            {
                for (int i = 1; i <= linksToPrint.Count(); i++)
                {
                    Console.WriteLine($"{i}) {linksToPrint[i - 1]}");
                }
            }
        }
    }
}
