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
    internal class TenantConfiguration : IEntityTypeConfiguration<TenantModel>
    {
        void IEntityTypeConfiguration<TenantModel>.Configure(EntityTypeBuilder<TenantModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasKey(e => e.TenantID);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.Country).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.TenancyType).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Property)
            .WithMany(a => a.TenantDetails)
            .HasForeignKey(b => b.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_Tenant");

        }
    }
}
