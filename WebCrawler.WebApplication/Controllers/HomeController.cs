using Microsoft.AspNetCore.Mvc;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.WebApplication.Models;
using WebCrawler.WebApplication.Services;

namespace WebCrawler.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbWorker _dbWorker;
        private readonly CrawlerService _crawlerService;
        private readonly InputValidator _inputValidator;

        public HomeController(DbWorker dbWorker, CrawlerService crawlerService, InputValidator inputValidator)
        {
            _dbWorker = dbWorker;
            _crawlerService = crawlerService;
            _inputValidator = inputValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TestsModel() {Tests=_dbWorker.GetAllTests()});
        }

        [HttpPost]
        public IActionResult Index(UserInputModel input)
        {
            string errors;
            var inputParamtersAreValid = _inputValidator.InputParameters(input.Url, out errors);
            if (!inputParamtersAreValid)
            {
                ModelState.AddModelError("Url", errors);
            }

            if(ModelState.IsValid)
            {
                _crawlerService.Crawl(input.Url);
            }

           return View(new TestsModel() { Tests = _dbWorker.GetAllTests()});
            

        }
    }
}
