using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.WebApplication.Models;
using WebCrawler.Data;
using WebCrawler.Logic;

namespace WebApplication.Controllers
{
    public class ResultsController : Controller
    {
        private readonly DbWorker _dbWorker;

        public ResultsController(DbWorker dbWorker)
        {
            _dbWorker = dbWorker;
        }

        public IActionResult Index(int id)
        {
            var testUrl = _dbWorker.GetUrlByTestId(id);
            var performance = _dbWorker.GetPerformanceResultsByTestId(id)
                .Select(p => new PerformanceResultDTO { Link = p.Url, ResponseTime = p.ResponseTime })
                .ToList();
            var onlySitemapUrls=_dbWorker.GetUrlsFoundOnlyInSitemapByTestId(id);
            var onlyWebsiteUrls = _dbWorker.GetUrlsFoundOnlyInWebsiteByTestId(id); 

            return View(new TestResultModel() { Website = testUrl, Performance = performance, OnlySitemapUrls = onlySitemapUrls, OnlyWebsiteUrls = onlyWebsiteUrls });
        }
    }
}
