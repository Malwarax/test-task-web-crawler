
using Microsoft.EntityFrameworkCore;

using System;
using System.Data;
using WebCrawler.Data;

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
            //   optionsBuilder.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=WebCrawlerDB;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebCrawlerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}

