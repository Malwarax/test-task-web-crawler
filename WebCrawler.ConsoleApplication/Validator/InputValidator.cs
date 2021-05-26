using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebCrawler.ConsoleApplication
{
    public class InputValidator
    {
        public virtual bool Validate(string url, UrlValidator urlValidator, RedirectionValidator redirectionValidator)
        {
            bool validatonResult = true;

            if (urlValidator.CheckUrl(url,new ConsoleWrapper()) == false || redirectionValidator.CheckRedirection(url,new ConsoleWrapper()) == false)
            {
                validatonResult = false;
            }
            
            return validatonResult;
        }
    }
}
