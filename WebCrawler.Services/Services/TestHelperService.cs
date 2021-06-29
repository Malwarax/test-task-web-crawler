using AutoMapper;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public TestHelperService(DbWorker dbWorker, IMapper mapper)
        {
            _dbWorker = dbWorker;
            _mapper = mapper;
        }

        public ResponseModel GetTestDetails(int testId, RequestModel request)
        {
            var test = GetTestById(testId);
            
            if (GetTestById(testId) == null)
            {
                return new ResponseModel { IsSuccessful = false, Errors = "Test not found" };
            }

            test.PerformanceResults = FilterTestDetails(testId, request);
            TestDetailsDto testDetails = _mapper.Map<TestDetailsDto>(test);

            return new ResponseModel { Result = testDetails };
        }

        private List<PerformanceResult>FilterTestDetails(int testId, RequestModel request)
        {
            var query = _dbWorker.GetPerformanceResultsByTestId(testId);

            if (request.InSitemap || request.InWebsite)
            {
                query = query.Where(r => r.InSitemap == request.InSitemap && r.InWebsite == request.InWebsite);
            }

            if (request.Page != 0 && request.PageSize != 0)
            {
                query = query.GetPagination(request.Page, request.PageSize);
            }

            return query.ToList();
        }

        public ResponseModel GetAllTests()
        {
            object result = _dbWorker.GetAllTests()
                .Select(r => _mapper.Map<TestDto>(r));

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