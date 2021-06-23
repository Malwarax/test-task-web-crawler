using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Data;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.WebAPI.Models;

namespace WebCrawler.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly DbWorker _dbWorker;
        private readonly CrawlerService _crawlerService;
        private readonly InputValidator _inputValidator;

        public TestsController(DbWorker dbWorker, CrawlerService crawlerService, InputValidator inputValidator)
        {
            _dbWorker = dbWorker;
            _crawlerService = crawlerService;
            _inputValidator = inputValidator;
        }
        /// <summary>
        /// Returns all tests
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<TestDto>> GetAllTests()
        {
            return _dbWorker.GetAllTests()
                .Select(r=>new TestDto {Id=r.Id, Url=r.Url, Date=r.Date})
                .ToArray();                
        }

        /// <summary>
        /// Crawl website, save results in db and returns new test Id
        /// </summary>
        /// <param name="url"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns the newly created test id</response>
        /// <response code="400">If the url is invalid</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<int> PostTest([FromBody]string url)
        {
            string errors;
            var inputParamtersAreValid = _inputValidator.InputParameters(url, out errors);
            if (!inputParamtersAreValid)
            {
                return BadRequest(errors);
            }

            return _crawlerService.Crawl(url);
        }
    }
}
