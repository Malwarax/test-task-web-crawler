using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Logic.Models;

namespace WebCrawler.Logic
{
    public class UrlValidator
    {
        public virtual ValidationResultModel CheckUrl(string url)
        {
            Uri websiteUri = null;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out websiteUri);
            string message = "";

            if (result == false)
            {
                message ="Error. Invalid Url. The format of the Url could not be determined.";
            }

            bool isThisUrlHasCorrectScheme = result == true && websiteUri.Scheme != Uri.UriSchemeHttps && websiteUri.Scheme != Uri.UriSchemeHttp;

            if (isThisUrlHasCorrectScheme)
            {
                message = "Error. Invalid Url. The Url does not contain Http or Https scheme.";
                result = false;
            }

            return new ValidationResultModel { Result = result, Message = message };
        }
    }
}
