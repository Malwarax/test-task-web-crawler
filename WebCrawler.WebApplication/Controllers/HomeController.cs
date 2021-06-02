using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Logic;
using WebCrawler.WebApplication.Models;

namespace WebCrawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbWorker _dbWorker;
        private readonly Crawler _crawler;

        public HomeController(DbWorker dbWorker, Crawler crawler)
        {
            _dbWorker = dbWorker;
            _crawler = crawler;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TestsModel() {Tests=_dbWorker.GetAllTests()});
        }

        [HttpPost]
        public IActionResult Index(string url)
        {
            var websiteUrl = new Uri(url);

            _crawler.Crawl(new Uri(url));

            var onlySitemapLinks = _crawler.GetOnlySitemapUrls();
            var onlyWebsiteLinks = _crawler.GetOnlyWebsiteUrls();
            var performanceEvaluationResult = _crawler.GetPerformance();

            _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult, onlySitemapLinks, onlyWebsiteLinks).Wait();

            return RedirectToAction("Index");
        }

    }
}
