using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebCrawler.ConsoleApplication
{
    public class RedirectionValidator
    {
        public virtual bool CheckRedirection(string url, ConsoleWrapper console)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            request.AllowAutoRedirect = false;
            bool result = true;

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                }
            }
            catch
            {
                console.WriteLine("Error. The server is redirecting the request for this url.");
                result = false;
            }

            return result;
        }
    }
}
