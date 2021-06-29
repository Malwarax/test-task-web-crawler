using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WebCrawler.Logic.Models;

namespace WebCrawler.Logic
{
    public class RedirectionValidator
    {
        public virtual ValidationResultModel CheckRedirection(string url)
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            var httpClient = new HttpClient(handler);
            httpClient.Timeout = new TimeSpan(0, 0, 10);

            bool result = true;
            string message = "";
            try
            {
                var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.Moved)
                {
                    message = "Error. The server is redirecting the request for this url.";
                    result = false;
                }
            }
            catch
            {
                message = "Error. No connection could be made.";
                result = false;
            }

            return new ValidationResultModel { Result = result, Message = message };
        }
    }
}
