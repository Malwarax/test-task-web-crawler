using System;

namespace WebCrawler.Services.Exceptions
{
    public class TestNotFoundException : Exception
    {
        public TestNotFoundException() : base("Test not found") { }
    }
}
