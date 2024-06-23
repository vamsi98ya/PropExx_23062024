using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyExchange.Presentation.API.Models.Tenant;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class TenantLeaseDetailsConfiguration : IEntityTypeConfiguration<TenantLeaseDetailsModel>
    {
        void IEntityTypeConfiguration<TenantLeaseDetailsModel>.Configure(EntityTypeBuilder<TenantLeaseDetailsModel> builder)
        {
            builder.Property(e => e.PropertyID).ValueGeneratedOnAdd();
            builder.Property(e => e.LeaseAdvanceAmount).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.LeaseStartDate).IsRequired();
            builder.Property(e => e.LeaseEndDate).IsRequired();
            builder.Property(e => e.LeaseTenureInMonths).IsRequired();
            builder.Property(e => e.RentPerSft).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.TotalAreaInSft).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.EscalationInPercentage).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.EscalationTenure).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Tenant)
            .WithMany(a => a.LeaseDetails)
            .HasForeignKey(b => b.TenantID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_TenantLeaseDetails");

        }
    }
}

