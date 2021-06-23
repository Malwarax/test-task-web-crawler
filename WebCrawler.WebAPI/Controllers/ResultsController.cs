using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Data;
using WebCrawler.Logic;
using WebCrawler.WebAPI.Models;

namespace WebCrawler.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly DbWorker _dbWorker;

        public ResultsController(DbWorker dbWorker)
        {
            _dbWorker = dbWorker;
        }

        /// <summary>
        /// Returns a performance result by test Id
        /// </summary>
        /// <remarks>
        /// If InSitemap and InWebsite are unset then returns performance result for all test urls
        /// 
        /// In second case returns performance result only for selected case
        /// </remarks>
        [HttpGet("{testId}")]
        public ActionResult<IEnumerable<TimingResultDto>> GetTestResult(int testId, [FromQuery] RequestModel request)
        {
            List<TimingResultDto> result = new List<TimingResultDto>();
            if (!request.InSitemap && !request.InWebsite)
            {
                  result = _dbWorker.GetPerformanceResultsByTestId(testId)
                  .Select(r => new TimingResultDto { Url = r.Url, ResponseTime = r.ResponseTime })
                  .ToList();
            }
            else
            {
                result = _dbWorker.GetPerformanceResultsByTestId(testId)
                 .Where(r => r.InSitemap == request.InSitemap && r.InWebsite == request.InWebsite)
                 .Select(r => new TimingResultDto { Url = r.Url, ResponseTime = r.ResponseTime })
                 .ToList();
            }

            return result;
        }

    }
}
