using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework.EntityConfigurations
{
    public class OnlyWebsiteUrlConfiguration : IEntityTypeConfiguration<OnlyWebsiteUrl>
    {
        public void Configure(EntityTypeBuilder<OnlyWebsiteUrl> builder)
        {
            builder.Property(w => w.Url)
                .HasMaxLength(1024)
                .IsRequired(true);
        }
    }
}
