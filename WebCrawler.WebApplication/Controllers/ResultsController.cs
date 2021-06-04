using Microsoft.AspNetCore.Mvc;
using WebCrawler.WebApplication.Models;
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
            var performance = _dbWorker.GetPerformanceResultsByTestId(id);
            var urlsFoundOnlyInSitemap=_dbWorker.GetUrlsFoundOnlyInSitemapByTestId(id);
            var urlsFoundOnlyInWebsite = _dbWorker.GetUrlsFoundOnlyInWebsiteByTestId(id); 

            return View(new TestResultModel() { Url = testUrl, Performance = performance, UrlsFoundOnlyInSitemap = urlsFoundOnlyInSitemap, UrlsFoundOnlyInWebsite = urlsFoundOnlyInWebsite });
        }
    }
}
