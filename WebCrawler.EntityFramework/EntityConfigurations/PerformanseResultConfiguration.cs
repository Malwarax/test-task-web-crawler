using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Data;

namespace WebCrawler.EntityFramework.EntityConfigurations
{
    class PerformanceResultConfiguration:IEntityTypeConfiguration<PerformanceResult>
    {
        public void Configure(EntityTypeBuilder<PerformanceResult> builder)
        {
            builder.Property(w => w.Link)
                .HasMaxLength(1024)
                .IsRequired(true);
        }
    }
}
