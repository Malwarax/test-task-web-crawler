using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.ConsoleApplication
{
    public class UrlValidator
    {
        private readonly ConsoleWrapper _console;

        public UrlValidator(ConsoleWrapper console)
        {
            _console = console;
        }

        public virtual bool CheckUrl(string url)
        {
            Uri websiteUri = null;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out websiteUri);
            
            if(result==false)
            {
                _console.WriteLine("Error. Invalid Url. The format of the Url could not be determined.");
            }

            bool isThisUrlHasCorrectScheme = result == true && websiteUri.Scheme != Uri.UriSchemeHttps && websiteUri.Scheme != Uri.UriSchemeHttp;
            
            if (isThisUrlHasCorrectScheme)
            {
                _console.WriteLine("Error. Invalid Url. The Url does not contain Http or Https scheme.");
                result = false;
            }

            return result;
        }
    }
}
