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
        private readonly WebsiteCrawler _websiteCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly PerformanceEvaluationGetter _performanceEvaluationGetter;

        public HomeController(DbWorker dbWorker, WebsiteCrawler websiteCrawler, SitemapCrawler sitemapCrawler, PerformanceEvaluationGetter performanceEvaluationGetter)
        {
            _dbWorker = dbWorker;
            _websiteCrawler = websiteCrawler;
            _sitemapCrawler = sitemapCrawler;
            _performanceEvaluationGetter = performanceEvaluationGetter;
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
            var websiteUrls = _websiteCrawler.Crawl(websiteUrl);
            var sitemapUrls = _sitemapCrawler.Crawl(websiteUrl);
            var performanceEvaluationResult = _performanceEvaluationGetter.PrepareLinks(websiteUrls, sitemapUrls);

            _dbWorker.SaveResult(websiteUrl, performanceEvaluationResult).Wait();

            return RedirectToAction("Index");
        }

    }
}
