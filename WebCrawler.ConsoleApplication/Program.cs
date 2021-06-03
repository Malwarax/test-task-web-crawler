﻿using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebCrawler.EntityFramework;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var app = host.Services.GetService<WebCrawlerApp>();
            app.Start();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>  
                Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddEfRepository<WebCrawlerDbContext>(options => options.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=WebCrawlerDB;Trusted_Connection=True"));
                        services.AddScoped<DbWorker>();
                        services.AddScoped<PageDownloader>();
                        services.AddScoped<ConsoleWrapper>();
                        services.AddScoped<UrlValidator>();
                        services.AddScoped<RedirectionValidator>();
                        services.AddScoped<PageParser>();
                        services.AddScoped<WebsiteCrawler>();
                        services.AddScoped<SitemapLinkReceiver>();
                        services.AddScoped<SitemapParser>();
                        services.AddScoped<SitemapCrawler>();
                        services.AddScoped<PerformanceEvaluator>();
                        services.AddScoped<PerformanceEvaluationGetter>();
                        services.AddScoped<LinksDifferencePrinter>();
                        services.AddScoped<ResponsePrinter>();
                        services.AddScoped<UserInteractionService>();
                        services.AddScoped<WebCrawlerApp>();
                    }).ConfigureLogging(options => options.SetMinimumLevel(LogLevel.Error));
    }
}
