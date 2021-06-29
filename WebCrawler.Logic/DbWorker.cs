using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Data;
using WebCrawler.EntityFramework;

namespace WebCrawler.Logic
{
    public class DbWorker
    {
        private readonly IRepository<PerformanceResult> _performanceResultRepository;
        private readonly IRepository<Test> _testRepository;


        public DbWorker(IRepository<PerformanceResult> performanceResultRepository, IRepository<Test> websiteRepository)
        {
            _performanceResultRepository = performanceResultRepository;
            _testRepository = websiteRepository;
        }

        public async Task<int> SaveResult(Uri websiteUrl, List<PerformanceResultDto> performanceResults)
        {
            var newTest = new Test { Url = websiteUrl.AbsoluteUri };
            await _testRepository.AddAsync(newTest);
            await _testRepository.SaveChangesAsync();

            _performanceResultRepository.AddRange(performanceResults.Select(p => new PerformanceResult() { Url = p.Url, ResponseTime = p.ResponseTime, InSitemap=p.InSitemap, InWebsite=p.InWebsite, TestId = newTest.Id }));

            await _performanceResultRepository.SaveChangesAsync();

            return newTest.Id;
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _testRepository.GetAll();
        }

        public IQueryable<PerformanceResult> GetPerformanceResultsByTestId(int id)
        {
            return _performanceResultRepository.GetAll()
                .Where(p => p.TestId == id)
                .OrderBy(p => p.ResponseTime);
        }

        public IEnumerable<string> GetUrlsFoundOnlyInSitemapByTestId(int id)
        {
            return _performanceResultRepository.GetAll()
                .Where(p => p.TestId == id && p.InSitemap == true && p.InWebsite == false)
                .Select(p => p.Url);
        }

        public IEnumerable<string> GetUrlsFoundOnlyInWebsiteByTestId(int id)
        {
            return _performanceResultRepository.GetAll()
                .Where(p => p.TestId == id && p.InSitemap == false && p.InWebsite == true)
                .Select(p => p.Url);
        }

        public string GetUrlByTestId(int id)
        {
            return _testRepository.GetByIdAsync(id).Result.Url;
        }

        public Test GetTestById(int id)
        {
            return _testRepository.GetAll()
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }
    }
}
