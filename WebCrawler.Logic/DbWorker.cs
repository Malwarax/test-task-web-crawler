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
        private readonly IRepository<Website> _websiteRepository;

        public DbWorker(IRepository<PerformanceResult> performanceResultRepository, IRepository<Website> websiteRepository)
        {
            _performanceResultRepository = performanceResultRepository;
            _websiteRepository = websiteRepository;
        }

        public void SaveResult(Uri WebsiteUrl, List<PerformanceResultDTO> performanceResultModels)
        {
            var website = _websiteRepository.GetByUrl(WebsiteUrl.AbsoluteUri);
            if (website==null)
            {
                AddNewWebsite(WebsiteUrl.AbsoluteUri, performanceResultModels);
            }
            else
            {
                AddOrUpdateResults(website, performanceResultModels);
            }
        }

        private PerformanceResult GetPerformanceResult(string link, TimeSpan responseTime, int websiteId)
        {
            return new PerformanceResult { Link = link, ResponseTime = responseTime, WebsiteId = websiteId };
        }

        private async void AddNewWebsite(string url, List<PerformanceResultDTO> results)
        {
            var newWebsite = new Website { WebsiteLink = url };
            await _websiteRepository.AddAsync(newWebsite);
            await _websiteRepository.SaveChangesAsync();

            foreach (var result in results)
            {
                await _performanceResultRepository.AddAsync(GetPerformanceResult(result.Link, result.ResponseTime, newWebsite.Id));
            }

            await _performanceResultRepository.SaveChangesAsync();
        }

        private async void AddOrUpdateResults(Website website, List<PerformanceResultDTO> results)
        {
            foreach(var result in results)
            {
                var performanceResult = website.PerformanceResults.FirstOrDefault(r => r.Link == result.Link);//.Where(r => r.Link == result.Link).FirstOrDefault();
                if (performanceResult==null)
                {
                    await _performanceResultRepository.AddAsync(GetPerformanceResult(result.Link, result.ResponseTime, website.Id));
                }
                else
                {
                    performanceResult.ResponseTime = result.ResponseTime;
                    _performanceResultRepository.Update(performanceResult);
                }
            }

            await _performanceResultRepository.SaveChangesAsync();
        }
    }
}
