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
        private readonly IRepository<OnlySitemapUrl> _onlySitemapUrlRepository;
        private readonly IRepository<OnlyWebsiteUrl> _onlyWebsiteUrlRepository;

        public DbWorker(IRepository<PerformanceResult> performanceResultRepository, IRepository<Test> websiteRepository, IRepository<OnlySitemapUrl> onlySitemapUrlRepository, IRepository<OnlyWebsiteUrl> onlyWebsiteUrlRepository)
        {
            _performanceResultRepository = performanceResultRepository;
            _testRepository = websiteRepository;
            _onlySitemapUrlRepository= onlySitemapUrlRepository;
            _onlyWebsiteUrlRepository=onlyWebsiteUrlRepository;
    }

        public async Task SaveResult(Uri websiteUrl, List<PerformanceResultDTO> performanceResults, List<Uri> onlySitemapUrls, List<Uri> onlyWebsiteUrls)
        {
            var newTest = new Test { Url = websiteUrl.AbsoluteUri };
            await _testRepository.AddAsync(newTest);
            await _testRepository.SaveChangesAsync();

            _performanceResultRepository.AddRange(performanceResults.Select(p => new PerformanceResult() { Url = p.Link, ResponseTime = p.ResponseTime.Milliseconds, TestId = newTest.Id }));
            _onlySitemapUrlRepository.AddRange(onlySitemapUrls.Select(p => new OnlySitemapUrl { Url = p.AbsoluteUri, TestId = newTest.Id }));
            _onlyWebsiteUrlRepository.AddRange(onlyWebsiteUrls.Select(p => new OnlyWebsiteUrl { Url = p.AbsoluteUri, TestId = newTest.Id }));

            await _performanceResultRepository.SaveChangesAsync();
            await _onlySitemapUrlRepository.SaveChangesAsync();
            await _onlyWebsiteUrlRepository.SaveChangesAsync();
        }

        public List<Test> GetAllTests()
        {
            return _testRepository.GetAll().ToList();
        }

        public List<PerformanceResult> GetPerformanceResultsByTestId(int id)
        {
            return _performanceResultRepository.GetAll()
                .Where(p => p.TestId == id)
                .OrderBy(p => p.ResponseTime)
                .ToList();
        }

        public List<string> GetOnlySitemapUrlsByTestId(int id)
        {
            return _onlySitemapUrlRepository.GetAll()
                .Where(p => p.TestId == id)
                .OrderBy(p => p.Url)
                .Select(p => p.Url)
                .ToList();
        }

        public List<string> GetOnlyWebsiteUrlsByTestId(int id)
        {
           return _onlyWebsiteUrlRepository.GetAll()
                .Where(p => p.TestId == id)
                .OrderBy(p => p.Url)
                .Select(p => p.Url)
                .ToList();
        }

        public string GetUrlByTestId(int id)
        {
            return _testRepository.GetAll().Where(t => t.Id == id).Select(t => t.Url).FirstOrDefault();
        }

    }
}
