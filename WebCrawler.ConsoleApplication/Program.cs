using HtmlAgilityPack;
using System;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new WebCrawlerApp();
            app.Start();
        }
    }
}
