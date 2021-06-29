using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Data;
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
        private readonly TestHelperService _testHelperService;

        public TestsController(DbWorker dbWorker, CrawlerService crawlerService, InputValidator inputValidator, TestHelperService testHelperService)
        {
            _dbWorker = dbWorker;
            _crawlerService = crawlerService;
            _inputValidator = inputValidator;
            _testHelperService = testHelperService;
        }

        /// <summary>
        /// Returns all tests
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<TestDto>> GetAllTests()
        {
            return _testHelperService.GetAllTests().ToArray();
        }

        /// <summary>
        /// Returns single test details by test id
        /// </summary>
        /// <remarks>
        /// If category is unset then returns performance result for all test urls
        /// 
        /// In second case returns performance result only for selected category
        /// 
        /// Also returns part of performance result urls for table pagination
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

            return _testHelperService.GetTestDetails(id, request);
        }
        /// <summary>
        /// Returns test results count by test id
        /// </summary>
        /// <response code="404">If the test not found</response>  
        [HttpGet("{id}/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetTestResultsCountById(int id)
        {
            if (_dbWorker.GetTestById(id) == null)
            {
                return NotFound(id);
            }

            return _dbWorker.GetPerformanceResultsByTestId(id).Count();
        }

        /// <summary>
        /// Create new test
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A newly created test</returns>
        /// <response code="200">Returns the newly created test</response>
        /// <response code="400">If the url is invalid</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> PostTest([FromBody] UserInputModel input)
        {
            string errors;
            var inputParametersAreValid = _inputValidator.InputParameters(input.Url, out errors);
            if (!inputParametersAreValid)
            {
                return BadRequest(errors);
            }

            return _crawlerService.Crawl(input.Url);
        }
    }
}
