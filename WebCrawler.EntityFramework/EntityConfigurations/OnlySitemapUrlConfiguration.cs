using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework.EntityConfigurations
{
    public class OnlySitemapUrlConfiguration : IEntityTypeConfiguration<OnlySitemapUrl>
    {
        public void Configure(EntityTypeBuilder<OnlySitemapUrl> builder)
        {
            builder.Property(w => w.Url)
                .HasMaxLength(1024)
                .IsRequired(true);
        }
    }
}
