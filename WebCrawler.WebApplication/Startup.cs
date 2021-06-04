using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebCrawler.EntityFramework;
using WebCrawler.WebApplication.Extensions;
using WebCrawler.WebApplication.Services;

namespace WebCrawler.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEfRepository<WebCrawlerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebCrawlerDB")));
            services.AddWebCrawlerLogicServices();
            services.AddScoped<CrawlerService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/");

                endpoints.MapControllerRoute(
                    name: "results",
                pattern: "{controller=Results}/{action=Index}/{id}");
            });
        }
    }
}
