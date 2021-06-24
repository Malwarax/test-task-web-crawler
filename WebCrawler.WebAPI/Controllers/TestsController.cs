using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.WebAPI.Models;
using WebCrawler.WebAPI.Services;

namespace WebCrawler.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly DbWorker _dbWorker;
        private readonly CrawlerService _crawlerService;
        private readonly InputValidator _inputValidator;
        private readonly TestDetailsFilteringService _testDetailsFilteringService;

        public TestsController(DbWorker dbWorker, CrawlerService crawlerService, InputValidator inputValidator, TestDetailsFilteringService testDetailsFilteringService)
        {
            _dbWorker = dbWorker;
            _crawlerService = crawlerService;
            _inputValidator = inputValidator;
            _testDetailsFilteringService = testDetailsFilteringService;
        }

        /// <summary>
        /// Returns all tests
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<TestDto>> GetAllTests()
        {
            return _dbWorker.GetAllTests()
                .Select(r => new TestDto { Id = r.Id, Url = r.Url, Date = r.Date })
                .ToArray();
        }

        /// <summary>
        /// Returns single test details by test id
        /// </summary>
        /// <remarks>
        /// If sitemap and website are unset then returns performance result for all test urls
        /// 
        /// In second case returns performance result only for selected urls
        /// </remarks>
        /// <response code="404">If the test not found</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestDetailsDto> GetTestById(int id, [FromQuery] RequestModel request)
        {
            if (_dbWorker.GetTestById(id) == null)
            {
                return NotFound(id);
            }

            return _testDetailsFilteringService.GetTestDetails(id,request);
        }

        /// <summary>
        /// Create new test
        /// </summary>
        /// <param name="url"></param>
        /// <returns>A newly created test id</returns>
        /// <response code="200">Returns the newly created test id</response>
        /// <response code="400">If the url is invalid</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> PostTest([FromBody] string url)
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
