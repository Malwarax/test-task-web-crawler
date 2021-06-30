using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebCrawler.Logic;
using WebCrawler.Logic.Validators;
using WebCrawler.Services;
using WebCrawler.Services.Interfaces;
using WebCrawler.Services.Mappers;

namespace WebCrawler.WebAPI.Extensions
{
    public static class ServiceCollectionExtension
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
            //services.AddScoped<CrawlerService>();
            services.AddScoped<TestHelperService>();
            services.AddScoped<ICrawlerService, CrawlerService>();


            var mapper = GetMapper();
            services.AddSingleton(mapper);
        }
        private static IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TestsMapperProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
