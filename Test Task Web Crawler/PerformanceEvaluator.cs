using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Test_Task_Web_Crawler
{
    class PerformanceEvaluator
    {
        public List<PerformanceResult> GetLinksResponseTime(List<Uri> links)
        {
            List<PerformanceResult> result = new List<PerformanceResult>();
            foreach(var link in links)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    timer.Stop();
                    TimeSpan timeTaken = timer.Elapsed;
                    result.Add(new PerformanceResult { Link=link, ResponseTime=timeTaken });
                }
                catch 
                {
                    //skip errors
                }
            }
            return result.OrderBy(r=>r.ResponseTime).ToList();
        }
    }
}
