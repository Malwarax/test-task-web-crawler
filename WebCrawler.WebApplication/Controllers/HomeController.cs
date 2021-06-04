using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Logic;
using WebCrawler.WebApplication.Models;
using WebCrawler.WebApplication.Services;

namespace WebCrawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbWorker _dbWorker;
        private readonly CrawlerService _crawlerService;

        public HomeController(DbWorker dbWorker, CrawlerService crawlerService)
        {
            _dbWorker = dbWorker;
            _crawlerService = crawlerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TestsModel() {Tests=_dbWorker.GetAllTests()});
        }

        [HttpPost]
        public IActionResult Index(UserInputModel input)
        {
            
            if(ModelState.IsValid)
            {
                _crawlerService.Crawl(input.Url);
            }

           return View(new TestsModel() { Tests = _dbWorker.GetAllTests()});
            

        }
    }
}
