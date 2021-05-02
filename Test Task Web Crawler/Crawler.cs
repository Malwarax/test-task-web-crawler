using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Task_Web_Crawler
{
    class Crawler
    {
        private static List<HrefLink> CrawlResult { get; set; }
        private static List<HrefLink> NextLink { get; set; }
        private static List<HrefLink> BufferList { get; set; }
        private static int MaxValue { get; set; }
        private static Uri baseUri { get; set; }

        public List<Uri> CrawlWebsiteManually(Uri websiteUri)
        {
            CrawlResult = new List<HrefLink>();
            baseUri = websiteUri;

            GetLinks((new HrefLink[] { new HrefLink() { Link = baseUri, Processed = false } }).ToList());

            return CrawlResult.Select(l => l.Link).Distinct().ToList();
        }

        public List<Uri> CrawlSitemap(Uri websiteUri)
        {
            var sitemapParser = new SitemapParser();
            return sitemapParser.GetLinks(websiteUri);
        }

        private static void GetLinks(List<HrefLink> hrefLinkList)
        {
            BufferList = new List<HrefLink>();

            Parallel.ForEach(hrefLinkList, linkNext =>
            {
                try
                {
                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = new HtmlDocument();

                    doc = web.Load(linkNext.Link);

                    if (doc != null && doc.DocumentNode.SelectNodes("//a[@href]") != null)
                    {
                        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                        {
                            var process = true;

                            string hrefValue = link.GetAttributeValue("href", string.Empty);

                            Uri linkToAdd=null;

                            if (hrefValue.StartsWith("/") && hrefValue.Length >= 1)
                            {
                                linkToAdd = new Uri(baseUri, hrefValue);
                            }

                            if (hrefValue.StartsWith("http") || hrefValue.StartsWith("www"))
                            {
                                linkToAdd = new Uri(hrefValue);
                            }

                            if (linkToAdd!=null)
                            {
                                var domain =linkToAdd.GetLeftPart(UriPartial.Authority);
                                if (domain != baseUri.GetLeftPart(UriPartial.Authority)) 
                                    process = false;
                                
                                if (process && linkToAdd != null)
                                {
                                    var clearLink=linkToAdd.GetLeftPart(UriPartial.Path);
                                    linkToAdd = new Uri(clearLink);
                                    BufferList.Add(new HrefLink() { Link = linkToAdd, Processed = false });
                                }
                            }
                        }

                        BufferList = MergeWithResult(BufferList);

                        var hrefLink = CrawlResult.Where(l => l.Link == linkNext.Link).FirstOrDefault();

                        if (hrefLink != null)
                        {
                            hrefLink.Processed = true;
                        }

                        if (BufferList.Count > 0)
                        { 
                            GetLinks(BufferList); 
                        }
                    }
                }
                catch
                {
                    //skip crawl errors
                }
            });

        }

        private static List<HrefLink> MergeWithResult(List<HrefLink> listToMerge)
        {
            NextLink = new List<HrefLink>();

            foreach (var hrefUrl in listToMerge)
            {
                if (CrawlResult.Exists(l => l.Link == hrefUrl.Link) == false)
                {
                    lock (CrawlResult)
                    {
                        if (MaxValue != 0 && CrawlResult.Count > MaxValue)
                        {
                            return new List<HrefLink>();
                        }

                        CrawlResult.Add(hrefUrl);

                        lock (NextLink)
                        {
                            NextLink.Add(hrefUrl);
                        }
                    }
                }
            }
            return NextLink;
        }

    }
}
