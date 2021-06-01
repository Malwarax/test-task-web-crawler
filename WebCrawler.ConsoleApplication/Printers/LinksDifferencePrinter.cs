using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class LinksDifferencePrinter
    {
        public void PrintDifference(int sitemapCount, int websiteCount,List<Uri> onlySitemapLinks, List<Uri> onlyWebsiteLinks)
        {
            Console.WriteLine($"Urls found after crawling a website: {websiteCount}");
            Console.WriteLine($"Urls found in sitemap: {sitemapCount}");

            Console.WriteLine("Urls FOUNDED IN SITEMAP.XML but not founded after crawling a web site:");
            PrintLinks(onlySitemapLinks);
            
            Console.WriteLine("Urls FOUNDED BY CRAWLING THE WEBSITE but not in sitemap.xml:");
            PrintLinks(onlyWebsiteLinks);
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

        //private List<Uri> GetUniqueLinks(List<Uri> baseLinks, List<Uri> linksToExcept)
        //{
        //    return baseLinks
        //    .Except(linksToExcept)
        //    .ToList(); ;
        //}
    }
}
