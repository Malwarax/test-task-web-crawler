using System.Linq;
using WebCrawler.Data;
using WebCrawler.Logic;
using WebCrawler.Services.Models.Request;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.Services
{
    public class TestHelperService
    {
        private readonly DbWorker _dbWorker;

        public TestHelperService(DbWorker dbWorker)
        {
            _dbWorker = dbWorker;
        }

        public ResponseModel GetTestDetails(int testId, RequestModel request)
        {
            if (GetTestById(testId) == null)
            {
                return new ResponseModel { IsSuccessful = false, Errors = "Test not found" };
            }

            TestDetailsDto test = new TestDetailsDto()
            {
                TestId = testId,
                Url = _dbWorker.GetUrlByTestId(testId),
            };

            var querry = _dbWorker.GetPerformanceResultsByTestId(testId);

            if (request.InSitemap || request.InWebsite)
            {
                querry = querry.Where(r => r.InSitemap == request.InSitemap && r.InWebsite == request.InWebsite);
            }

            if (request.Page != 0 && request.PageSize != 0)
            {
                querry = querry.GetPagination(request.Page, request.PageSize);
            }

            test.Results = querry.Select(r => new PerformanceResultModel { Url = r.Url, ResponseTime = r.ResponseTime })
                .ToArray();

            return new ResponseModel { Result = test };
        }

        public ResponseModel GetAllTests()
        {
            object result = _dbWorker.GetAllTests()
                .Select(r => new TestDto { Id = r.Id, Url = r.Url, Date = r.Date });

            return new ResponseModel { Result = result };
        }

        public Test GetTestById(int id)
        {
            return _dbWorker.GetTestById(id);
        }

        public ResponseModel GetTestResultsCountByTestId(int testId)
        {
            if (GetTestById(testId) == null)
            {
                return new ResponseModel { IsSuccessful = false, Errors = "Test not found" };
            }

            return new ResponseModel
            {
                Result = _dbWorker.GetPerformanceResultsByTestId(testId).Count()
            };
        }
    }
}