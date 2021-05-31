using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WebCrawler.Data;
using WebCrawler.EntityFramework;

namespace WebCrawler.Logic
{
    public class DbWorker
    {
        private readonly IRepository<PerformanceResult> _performanceResultRepository;
        private readonly IRepository<Test> _websiteRepository;

        public DbWorker(IRepository<PerformanceResult> performanceResultRepository, IRepository<Test> websiteRepository)
        {
            _performanceResultRepository = performanceResultRepository;
            _websiteRepository = websiteRepository;
        }

        public async void SaveResult(Uri websiteUrl, List<PerformanceResultDTO> performanceResults)
        {
            var newTest = new Test {Url = websiteUrl.AbsoluteUri};
            await _websiteRepository.AddAsync(newTest);
            await _websiteRepository.SaveChangesAsync();

            foreach (var result in performanceResults)
            {
                await _performanceResultRepository.AddAsync(new PerformanceResult { Url = result.Link, ResponseTime = result.ResponseTime.Milliseconds, TestId = newTest.Id });
            }

            await _performanceResultRepository.SaveChangesAsync();
        }
    }
}
