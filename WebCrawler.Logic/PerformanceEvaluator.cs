using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace WebCrawler.Logic
{
    public class PerformanceEvaluator
    {
        public virtual int GetResponceTime(Uri link)
        {
            TimeSpan result = new TimeSpan();
            try 
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
                Stopwatch timer = new Stopwatch();
                timer.Start();
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    timer.Stop();
                }
                result = timer.Elapsed;
            }
            catch
            {
                Console.WriteLine($"Error with getting the responce from {link}. The result for this url will be 0");
            }

            return result.Milliseconds;
        }
    }
}
