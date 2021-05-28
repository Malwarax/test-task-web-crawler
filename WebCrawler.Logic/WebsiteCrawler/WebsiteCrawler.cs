using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebCrawler.Logic
{
    public class WebsiteCrawler
    {
        //public virtual List<Uri> Crawl2(Uri websiteUri, PageDownloader pageDownloader, PageParser pageParser)
        //{
        //    List<Uri> result = new List<Uri>(); 
        //    Queue<Uri> linksToParse = new Queue<Uri>();
        //    linksToParse.Enqueue(websiteUri);
        //
        //    while (linksToParse.Count!=0)
        //    {
        //        var linkToParse = linksToParse.Dequeue();
        //        result.Add(linkToParse);
        //        string htmlPage = pageDownloader.GetPage(linkToParse);
        //        var parsedLinks = pageParser.GetLinks(htmlPage, websiteUri);
        //
        //        foreach (var link in parsedLinks)
        //        {
        //            if(!result.Contains(link)&&!linksToParse.Contains(link))
        //                linksToParse.Enqueue(link);
        //        }
        //    }
        //
        //    return result
        //        .Distinct()
        //        .ToList();
        //}

        public virtual List<Uri> Crawl(Uri websiteUri, PageDownloader pageDownloader, PageParser pageParser)
        {
            List<Uri> result = new List<Uri>();
            List<Uri> linksToParse = new List<Uri>();
            linksToParse.Add(websiteUri);

            while (linksToParse.Count != 0)
            {
                result.Add(linksToParse[0]);
                string htmlPage = pageDownloader.GetPage(linksToParse[0]);
                var parsedLinks = pageParser.GetLinks(htmlPage, websiteUri);

                foreach (var link in parsedLinks)
                {
                    if (!result.Contains(link) && !linksToParse.Contains(link))
                        linksToParse.Add(link);
                }
                linksToParse.RemoveAt(0);
            }

            return result
                .Distinct()
                .ToList();
        }
    }
}
