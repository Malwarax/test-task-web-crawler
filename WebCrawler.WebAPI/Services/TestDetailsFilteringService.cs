using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.WebAPI.Models;

namespace WebCrawler.WebAPI.Services
{
    public class TestDetailsFilteringService
    {
        private readonly DbWorker _dbWorker;

        public TestDetailsFilteringService(DbWorker dbWorker)
        {
            _dbWorker = dbWorker;
        }

        public TestDetailsDto GetTestDetails(int testId, RequestModel request)
        {
            TestDetailsDto test = new TestDetailsDto()
            {
                TestId = testId,
                Url = _dbWorker.GetUrlByTestId(testId),
            };

            if (!request.sitemap && !request.website)
            {
                test.Results = _dbWorker.GetPerformanceResultsByTestId(testId)
                .Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                .ToList();
            }
            else
            {
                test.Results = _dbWorker.GetPerformanceResultsByTestId(testId)
                 .Where(r => r.InSitemap == request.sitemap && r.InWebsite == request.website)
                 .Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                 .ToList();
            }

            return test;
        }
    }
}
