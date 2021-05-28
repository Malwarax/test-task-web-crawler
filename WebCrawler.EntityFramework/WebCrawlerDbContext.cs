
using Microsoft.EntityFrameworkCore;

using System;
using System.Data;
using System.Reflection;
using WebCrawler.Data;
using WebCrawler.EntityFramework.EntityConfigurations;

namespace WebCrawler.EntityFramework
{
    public class WebCrawlerDbContext : DbContext, IEfRepositoryDbContext
    {
        public WebCrawlerDbContext(DbContextOptions<WebCrawlerDbContext> options) : base(options)
        {

        }
        public DbSet<PerformanceResult> PerformanceResults { get; set; }
        public DbSet<Website> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebCrawlerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(new WebsiteConfiguration());
        }
    }

}

