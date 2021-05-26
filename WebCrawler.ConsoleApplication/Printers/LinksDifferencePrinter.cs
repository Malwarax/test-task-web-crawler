using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class LinksDifferencePrinter
    {
        public void PrintDifference(List<Uri> sitemapLinks, List<Uri> manuallyCrawledLinks)
        {
            Console.WriteLine($"Urls found after crawling a website: {manuallyCrawledLinks.Count}");
            Console.WriteLine($"Urls found in sitemap: {sitemapLinks.Count}");

            Console.WriteLine("Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:");
            PrintLinks(GetUniqueLinks(sitemapLinks, manuallyCrawledLinks));
            
            Console.WriteLine("Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
            PrintLinks(GetUniqueLinks(manuallyCrawledLinks, sitemapLinks));
        }

        private void PrintLinks(List<Uri> linksToPrint)
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

        private List<Uri> GetUniqueLinks(List<Uri> baseLinks, List<Uri> linksToExcept)
        {
            return baseLinks
            .Except(linksToExcept)
            .ToList(); ;
        }
    }
}
