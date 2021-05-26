using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.ConsoleApplication
{
    public class UrlValidator
    {
        public virtual bool CheckUrl(string url, ConsoleWrapper console)
        {
            Uri websiteUri = null;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out websiteUri);
            
            if(result==false)
            {
                console.WriteLine("Error. Invalid Url. The format of the Url could not be determined.");
            }

            if(result == true && websiteUri.Scheme != Uri.UriSchemeHttps && websiteUri.Scheme != Uri.UriSchemeHttp)
            {
                console.WriteLine("Error. Invalid Url. The Url does not contain Http or Https scheme.");
                result = false;
            }

            return result;
        }
    }
}
