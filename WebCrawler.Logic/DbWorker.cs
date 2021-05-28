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

        public async void SaveResult(Uri WebsiteUrl, List<PerformanceResultModel> performanceResultModels)
        {
            var website = _websiteRepository.GetByUrl(WebsiteUrl.AbsoluteUri);
            if (website==null)
            {
                var newWebsite = new Website { WebsiteLink = WebsiteUrl.AbsoluteUri };
                await _websiteRepository.AddAsync(newWebsite);
                await _websiteRepository.SaveChangesAsync();

                foreach (var result in performanceResultModels)
                {
                    await _performanceResultRepository.AddAsync(new PerformanceResult { Link = result.Link, ResponseTime = result.ResponseTime, WebsiteId = newWebsite.Id });
                }

                await _performanceResultRepository.SaveChangesAsync();
            }
            else
            {
                foreach(var result in performanceResultModels)
                {
                    var performanceResult=website.PerformanceResults.Where(r => r.Link == result.Link).FirstOrDefault();
                    if (performanceResult==null)
                    {
                        await _performanceResultRepository.AddAsync(new PerformanceResult { Link = result.Link, ResponseTime = result.ResponseTime, WebsiteId = website.Id });
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
}
