﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Services;
using WebCrawler.Services.Models.Request;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly CrawlerService _crawlerService;

        private readonly TestHelperService _testHelperService;

        public TestsController(CrawlerService crawlerService, TestHelperService testHelperService)
        {
            _crawlerService = crawlerService;
            _testHelperService = testHelperService;
        }

        /// <summary>
        /// Returns all tests
        /// </summary>
        [HttpGet]
        public ActionResult<ResponseModel> GetAllTests()
        {
            return _testHelperService.GetAllTests();
        }

        /// <summary>
        /// Returns single test details by test id
        /// </summary>
        /// <remarks>
        /// If sitemap and website are unset then returns performance result for all test urls
        /// 
        /// In second case returns performance result only for selected category
        /// 
        /// Also returns part of performance result urls for table pagination
        /// </remarks>
        /// <response code="404">If the test not found</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ResponseModel> GetTestById(int id, [FromQuery] RequestModel request)
        {
            return _testHelperService.GetTestDetails(id, request);
        }
        /// <summary>
        /// Returns test results count by test id
        /// </summary>
        /// <response code="404">If the test not found</response>  
        [HttpGet("{id}/count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ResponseModel> GetTestResultsCountById(int id)
        {
            return _testHelperService.GetTestResultsCountByTestId(id);
        }

        /// <summary>
        /// Create new test
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A newly created test id</returns>
        /// <response code="200">Returns the newly created test id</response>
        /// <response code="400">If the url is invalid</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ResponseModel> PostTest([FromBody] UserInputModel input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            return _crawlerService.Crawl(input.Url);
        }
    }
}
