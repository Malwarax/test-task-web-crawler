using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework.EntityConfigurations
{
    class PerformanceResultConfiguration:IEntityTypeConfiguration<PerformanceResult>
    {
        public void Configure(EntityTypeBuilder<PerformanceResult> builder)
        {
            builder.Property(w => w.Url)
                .HasMaxLength(1024);
                
        }
    }
}
