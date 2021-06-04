using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;

namespace WebCrawler.WebApplication.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddWebCrawlerLogicServices(this IServiceCollection services)
        {
            services.AddScoped<DbWorker>();
            services.AddScoped<PageDownloader>();
            services.AddScoped<PageParser>();
            services.AddScoped<WebsiteCrawler>();
            services.AddScoped<SitemapLinkReceiver>();
            services.AddScoped<SitemapParser>();
            services.AddScoped<SitemapCrawler>();
            services.AddScoped<PerformanceEvaluator>();
            services.AddScoped<PerformanceEvaluationGetter>();
            services.AddScoped<UrlValidator>();
            services.AddScoped<RedirectionValidator>();
            services.AddScoped<InputValidator>();
        }
    }
}
