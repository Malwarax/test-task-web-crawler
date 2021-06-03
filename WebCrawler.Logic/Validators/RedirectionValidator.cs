using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebCrawler.Logic.Models;

namespace WebCrawler.Logic
{
    public class RedirectionValidator
    {
        public virtual ValidationResultModel CheckRedirection(string url)
        {

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            request.AllowAutoRedirect = false;
            bool result = true;
            string message = "";
            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                }
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("301"))
                {
                    message="Error. The server is redirecting the request for this url.";
                }
                else
                {
                    message="Error. No connection could be made.";
                }

                result = false;
            }

            return new ValidationResultModel { Result=result, Message=message};
        }
    }
}
