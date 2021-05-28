using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework.EntityConfigurations
{
    public class WebsiteConfiguration : IEntityTypeConfiguration<Website>
    {
        public void Configure(EntityTypeBuilder<Website> builder)
        {
            builder.Property(w => w.WebsiteLink)
                .HasMaxLength(1024)
                .IsRequired(true);
        }
    }
}
