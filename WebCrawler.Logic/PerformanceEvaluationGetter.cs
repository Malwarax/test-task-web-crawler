﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.Logic
{
    public class PerformanceEvaluationGetter
    {
        public virtual List<PerformanceResultDTO> PrepareLinks(List<Uri> links,PerformanceEvaluator performanceEvaluator)
        {
            List<PerformanceResultDTO> result = new List<PerformanceResultDTO>();

            foreach (var link in links)
            {
                result.Add(new PerformanceResultDTO { Link = link.AbsoluteUri, ResponseTime = performanceEvaluator.GetResponceTime(link) });
            }

            return result
                .OrderBy(r => r.ResponseTime)
                .ToList();
        }
    }
}
