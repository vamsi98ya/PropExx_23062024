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
    internal class TenantRentPaymentDetailsConfiguration : IEntityTypeConfiguration<TenantRentPaymentDetailsModel>
    {
        void IEntityTypeConfiguration<TenantRentPaymentDetailsModel>.Configure(EntityTypeBuilder<TenantRentPaymentDetailsModel> builder)
        {
            builder.Property(e => e.PropertyID).ValueGeneratedOnAdd();
            builder.Property(e => e.ExpenseType).IsRequired();
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.ExpenseAmount).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.ExpenseIncurredOnDate).IsRequired();
            builder.Property(e => e.ExpenseMonth).IsRequired();
            builder.Property(e => e.ExpenseYear).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Tenant)
            .WithMany(a => a.RentPaymentDetails)
            .HasForeignKey(b => b.TenantID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_TenantRentPaymentDetails");

        }
    }
}
