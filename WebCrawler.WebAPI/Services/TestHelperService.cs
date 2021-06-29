using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.WebAPI.Models;

namespace WebCrawler.WebAPI.Services
{
    public class TestHelperService
    {
        private readonly DbWorker _dbWorker;

        public TestHelperService(DbWorker dbWorker)
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

            var querry = _dbWorker.GetPerformanceResultsByTestId(testId);
            if (request.category == "sitemap")
            {
                querry = querry.Where(r => r.InSitemap == true && r.InWebsite == false); 
            }
            else if(request.category == "website")
            {
                querry = querry.Where(r => r.InSitemap == false && r.InWebsite == true);
            }

            if (request.page != 0 && request.perPage != 0)
            {
                querry = querry.Skip((request.page - 1) * request.perPage)
                    .Take(request.perPage);
            }

            test.Results = querry.Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                .ToList();

            return test;
        }

        public IEnumerable<TestDto> GetAllTests()
        {
            return _dbWorker.GetAllTests()
                .Select(r => new TestDto { Id = r.Id, Url = r.Url, Date = r.Date });
        }
    }
}

/*
  
                 if (request.page != 0 && request.perPage != 0)
            {
                test.Results = _dbWorker.GetPerformanceResultsByTestId(testId)
                    .Skip((request.page-1) * request.perPage)
                    .Take(request.perPage)
                .Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                .ToList();
            }
            else if (request.category =="sitemap")
            {
                test.Results = _dbWorker.GetPerformanceResultsByTestId(testId)
                 .Where(r => r.InSitemap == true && r.InWebsite == false)
                 .Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                 .ToList();
            }
            else if(request.category == "website")
            {
                test.Results = _dbWorker.GetPerformanceResultsByTestId(testId)
                 .Where(r => r.InSitemap == false && r.InWebsite == true)
                 .Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                 .ToList();
            }
            else
            {
                test.Results = _dbWorker.GetPerformanceResultsByTestId(testId)
                    .Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                    .ToList();
            }  
 
 */