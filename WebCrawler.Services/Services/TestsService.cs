using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Data;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.Services.Exceptions;
using WebCrawler.Services.Extensions;
using WebCrawler.Services.Interfaces;
using WebCrawler.Services.Models.Request;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.Services
{
    public class TestsService
    {
        private readonly DbWorker _dbWorker;
        private readonly IMapper _mapper;
        private readonly ICrawlerService _crawlerService;
        private readonly InputValidator _inputValidator;

        public TestsService(DbWorker dbWorker, IMapper mapper, ICrawlerService crawlerService, InputValidator inputValidator)
        {
            _dbWorker = dbWorker;
            _mapper = mapper;
            _crawlerService = crawlerService;
            _inputValidator = inputValidator;
        }

        public TestDetailsModel GetTestDetails(int testId, RequestModel request)
        {
            var test = _dbWorker.GetTestById(testId);

            if (test == null)
            {
                throw new TestNotFoundException();
            }

            test.PerformanceResults = FilterTestDetails(testId, request);

            return _mapper.Map<TestDetailsModel>(test);
        }

        public List<PerformanceResult> FilterTestDetails(int testId, RequestModel request)
        {
            var query = _dbWorker.GetPerformanceResultsByTestId(testId);

            query = query.Where(r => r.InSitemap == request.InSitemap && r.InWebsite == request.InWebsite);
            query = query.GetPagination(request.Page, request.PageSize);

            return query.ToList();
        }

        public List<TestModel> GetAllTests()
        {
            return _dbWorker.GetAllTests()
                .Select(r => _mapper.Map<TestModel>(r))
                .ToList(); ;
        }

        public int GetTestResultsCountByTestId(int testId)
        {
            if (_dbWorker.GetTestById(testId) == null)
            {
                throw new TestNotFoundException();
            }

            return _dbWorker.GetPerformanceResultsByTestId(testId).Count();
        }

        public async Task<int> CreateNewTest(UserInputModel input)
        {
            string errors;
            var inputParametersAreValid = _inputValidator.InputParameters(input.Url, out errors);
            if (!inputParametersAreValid)
            {
                throw new UrlValidationException(errors);
            }

            return await _crawlerService.Crawl(input.Url);
        }
    }
}