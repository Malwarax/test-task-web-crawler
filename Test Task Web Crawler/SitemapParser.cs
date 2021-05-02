using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;


namespace Test_Task_Web_Crawler
{
    class SitemapParser
    {
        public List<Uri> GetLinks(Uri baseUri)
        {
            Uri sitemapUri = GetSitemapUri(baseUri);
            List<Uri> sitemapLinkList = ParseSitemapItems(sitemapUri).Distinct().ToList();
            return sitemapLinkList;
        }

        private List<Uri> ParseSitemapItems(Uri sitemapUri)
        {
            List<Uri> uriList = new List<Uri>();

            try
            {
                XmlDocument sitemapDocument = new XmlDocument();

                sitemapDocument.Load(sitemapUri.AbsoluteUri);

                if (sitemapDocument.DocumentElement.Name.Equals("sitemapindex"))
                {
                    XmlNodeList xnList = sitemapDocument.GetElementsByTagName("sitemap");
                    foreach (XmlNode url in xnList)
                    {
                        uriList.AddRange(ParseSitemapItems(new Uri(url["loc"].InnerText)));
                    }
                }
                else if (sitemapDocument.DocumentElement.Name.Equals("urlset"))
                {
                    XmlNodeList xnList = sitemapDocument.GetElementsByTagName("url");
                    foreach (XmlNode url in xnList)
                    {
                        if (url["loc"].InnerText.StartsWith("/") && url["loc"].InnerText.Length >= 1)
                        {
                            var baseUri = sitemapUri.GetLeftPart(System.UriPartial.Authority);
                            uriList.Add(new Uri(new Uri(baseUri), url["loc"].InnerText));
                        }
                        else
                        {
                            uriList.Add(new Uri(url["loc"].InnerText));
                        }
                    }
                }
            }
            catch 
            {
                Console.WriteLine(" Something went wrong with sitemap.xml");
            }
            return uriList;
        }

        private Uri GetSitemapUri(Uri baseUri)
        {
            Uri sitemapUri;
            try 
            {
                Uri robotsTxtUri = new Uri(baseUri, "/robots.txt");

                string fileContent="";
                using (WebClient client = new WebClient())
                {
                    fileContent = client.DownloadString(robotsTxtUri);
                }

                var sitemapString = fileContent.Split('\n')
                .Select(x => x.Trim())
                .Where(x => x.StartsWith("Sitemap:"))
                .FirstOrDefault();

                if (!String.IsNullOrEmpty(sitemapString))
                {
                    sitemapString = sitemapString
                    .Replace("Sitemap:", "")
                    .Trim();
                    sitemapUri = new Uri(sitemapString);
                }
                else
                {
                    sitemapUri = new Uri(baseUri, "/sitemap.xml");
                }
                return sitemapUri; 
            }
            catch 
            {
                return sitemapUri = new Uri(baseUri, "/sitemap.xml"); //default sitemap url
            }
        }

    }
}
