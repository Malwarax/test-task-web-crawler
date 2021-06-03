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

        public virtual List<PerformanceResultDTO> PrepareLinks (List<Uri> websiteUrls, List<Uri> sitemapUrls)
        {
            List<PerformanceResultDTO> result = new List<PerformanceResultDTO>();

            var combinedUrls = websiteUrls.Union(sitemapUrls).ToList();

            foreach (var url in combinedUrls)
            {
                bool inSitemap = false;
                bool inWebsite = false;

                if(websiteUrls.Contains(url))
                {
                    inWebsite = true;
                }

                if(sitemapUrls.Contains(url))
                {
                    inSitemap = true;
                }

                result.Add(new PerformanceResultDTO { Url = url.AbsoluteUri, ResponseTime = _performanceEvaluator.GetResponceTime(url), InSitemap=inSitemap, InWebsite=inWebsite });
            }

            return result
                .OrderBy(r => r.ResponseTime)
                .ToList();
        }
    }
}
