using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCrawler.Logic
{
    public class WebsiteCrawler
    {
        private readonly PageDownloader _pageDownloader;
        private readonly PageParser _pageParser;

        public WebsiteCrawler(PageDownloader pageDownloader, PageParser pageParser)
        {
            _pageDownloader = pageDownloader;
            _pageParser = pageParser;
        }

        public virtual List<Uri> Crawl(Uri websiteUri)
        {
            List<Uri> result = new List<Uri>();
            List<Uri> linksToParse = new List<Uri>();
            linksToParse.Add(websiteUri);

            while (linksToParse.Count != 0)
            {
                result.Add(linksToParse[0]);
                string htmlPage = _pageDownloader.GetPage(linksToParse[0]);
                var parsedLinks = _pageParser.GetLinks(htmlPage, websiteUri);

                foreach (var link in parsedLinks)
                {
                    var isThisLinkNotBeenFoundYet = !result.Contains(link) && !linksToParse.Contains(link);

                    if (isThisLinkNotBeenFoundYet)
                    {
                        linksToParse.Add(link);
                    }
                }

                linksToParse.RemoveAt(0);
            }

            return result
                .Distinct()
                .ToList();
        }
    }
}
