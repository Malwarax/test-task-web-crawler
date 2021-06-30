using System;

namespace WebCrawler.Services.Exceptions
{
    public class UrlValidationException : Exception
    {
        public UrlValidationException(string message) : base(message) { }
    }
}
