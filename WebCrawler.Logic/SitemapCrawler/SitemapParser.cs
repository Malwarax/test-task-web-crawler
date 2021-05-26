using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace WebCrawler.Logic
{
    public class SitemapParser
    {
        virtual public List<Uri> GetLinks(string sitemap, Uri websiteUri)
        {
            List<Uri> uriList = new List<Uri>();
            try
            {
                XmlDocument sitemapDocument = new XmlDocument();
                sitemapDocument.LoadXml(sitemap);
                if (sitemapDocument.DocumentElement.Name.Equals("urlset"))
                {
                    XmlNodeList xnList = sitemapDocument.GetElementsByTagName("url");

                    foreach (XmlNode url in xnList)
                    {
                        if (url["loc"].InnerText.StartsWith("/") && url["loc"].InnerText.Length >= 1)
                        {
                            var baseUri = websiteUri.GetLeftPart(System.UriPartial.Authority);
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
                Console.WriteLine("Something went wrong with sitemap.xml");
            }

            return uriList
                .Distinct()
                .ToList();
        }
    }
}
