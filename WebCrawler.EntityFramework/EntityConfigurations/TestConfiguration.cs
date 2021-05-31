using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework.EntityConfigurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property(w => w.Url)
                .HasMaxLength(1024)
                .IsRequired(true);
        }
    }
}
