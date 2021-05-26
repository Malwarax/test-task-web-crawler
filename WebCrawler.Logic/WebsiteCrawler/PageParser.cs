using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawler.Logic
{
    public class PageParser
    {
        public virtual List<Uri> GetLinks(string htmlPage, Uri websiteUri)
        {
            List<Uri> result = new List<Uri>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlPage);

            var nodesWithAnchors = doc.DocumentNode.SelectNodes("//a[@href]");
            if (nodesWithAnchors == null)
            {
                return new List<Uri>();
            }

            foreach (HtmlNode link in nodesWithAnchors)
            {
                var process = true;

                string hrefValue = link.GetAttributeValue("href", string.Empty);

                Uri linkToAdd = null;

                if (hrefValue.StartsWith("/") && hrefValue.Length >= 1)
                {
                    linkToAdd = new Uri(websiteUri, hrefValue);
                }

                if (hrefValue.StartsWith("http"))
                {
                    linkToAdd = new Uri(hrefValue);
                }

                if (hrefValue.StartsWith("www"))
                {
                    string scheme = websiteUri.Scheme + "://";
                    linkToAdd = new Uri(scheme + hrefValue);
                }

                if (linkToAdd != null)
                {
                    var domain = linkToAdd.GetLeftPart(UriPartial.Authority);
                    if (domain != websiteUri.GetLeftPart(UriPartial.Authority))
                        process = false;

                    if (process && linkToAdd != null)
                    {
                        var clearLink = linkToAdd.GetLeftPart(UriPartial.Path);
                        linkToAdd = new Uri(clearLink);
                        result.Add(linkToAdd);
                    }
                }
            }

            return result
                .Distinct()
                .ToList();
        }
    }
}
