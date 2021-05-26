using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.Logic
{
    public class PerformanceEvaluationGetter
    {
        public virtual List<PerformanceResultModel> PrepareLinks(List<Uri> links,PerformanceEvaluator performanceEvaluator)
        {
            List<PerformanceResultModel> result = new List<PerformanceResultModel>();

            foreach (var link in links)
            {
                result.Add(new PerformanceResultModel { Link = link.AbsoluteUri, ResponseTime = performanceEvaluator.GetResponceTime(link) });
            }

            return result
                .OrderBy(r => r.ResponseTime)
                .ToList();
        }
    }
}
