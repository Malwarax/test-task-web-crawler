using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework
{
    public class WebCrawlerDbContext : DbContext, IEfRepositoryDbContext
    {
        public WebCrawlerDbContext(DbContextOptions<WebCrawlerDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<PerformanceResult> PerformanceResults { get; set; }
        public DbSet<OnlySitemapUrl> OnlySitemapUrls { get; set; }
        public DbSet<OnlyWebsiteUrl> OnlyWebsiteUrls { get; set; }
        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebCrawlerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}

