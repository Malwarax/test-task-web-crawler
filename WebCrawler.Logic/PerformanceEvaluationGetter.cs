using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.Logic
{
    public class PerformanceEvaluationGetter
    {
        private readonly PerformanceEvaluator _performanceEvaluator;

        public PerformanceEvaluationGetter(PerformanceEvaluator performanceEvaluator)
        {
            _performanceEvaluator = performanceEvaluator;
        }

        public virtual List<PerformanceResultDTO> PrepareLinks(List<Uri> links)
        {
            List<PerformanceResultDTO> result = new List<PerformanceResultDTO>();

            foreach (var link in links)
            {
                result.Add(new PerformanceResultDTO { Link = link.AbsoluteUri, ResponseTime = _performanceEvaluator.GetResponceTime(link) });
            }

            return result
                .OrderBy(r => r.ResponseTime)
                .ToList();
        }

        public virtual List<PerformanceResultDTO> PrepareLinks(List<Uri> websiteUrls, List<Uri> sitemapUrls)
        {
            List<PerformanceResultDTO> result = new List<PerformanceResultDTO>();

            var combinedUrls = sitemapUrls.Union(websiteUrls).ToList();

            foreach (var url in combinedUrls)
            {
                bool inSitemap = false;
                bool inWebsite = false;

                if(websiteUrls.Contains(url))
                {
                    inSitemap = true;
                }

                if(sitemapUrls.Contains(url))
                {
                    inWebsite = true;
                }

                result.Add(new PerformanceResultDTO { Link = url.AbsoluteUri, ResponseTime = _performanceEvaluator.GetResponceTime(url), InSitemap=inSitemap, InWebsite=inWebsite });
            }

            return result
                .OrderBy(r => r.ResponseTime)
                .ToList();
        }
    }
}
