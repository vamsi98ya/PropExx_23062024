using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class PropertyValuationMetricsConfiguration : IEntityTypeConfiguration<PropertyValuationMetricsModel>
    {
        void IEntityTypeConfiguration<PropertyValuationMetricsModel>.Configure(EntityTypeBuilder<PropertyValuationMetricsModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.PricePerSft).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.TotalSft).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.Month).IsRequired();
            builder.Property(e => e.Year).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Property)
            .WithMany(a => a.ValuationMetrics)
            .HasForeignKey(b => b.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_PropertyValuationMetrics");

        }
    }
}
