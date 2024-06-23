using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PropertyExchange.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Core.Domain.Models.User;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class PropertyConfiguration : IEntityTypeConfiguration<PropertyModel>
    {
        void IEntityTypeConfiguration<PropertyModel>.Configure(EntityTypeBuilder<PropertyModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasKey(e => e.PropertyID);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasColumnType("text");
            builder.Property(e => e.Location).IsRequired();
            builder.Property(e => e.PropertyType).IsRequired();
            builder.Property(e => e.HoldingType).IsRequired();
            builder.Property(e => e.OverallAreaInSft).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.IsAvailableForInvesting).IsRequired();
            builder.Property(e => e.InitialTotalValuation).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.InitialTokenPrice).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.InitialTotalNumberOfTokens).IsRequired();
            builder.Property(e => e.InitialAvailableNumberOfTokens).IsRequired();
            builder.Property(e => e.CurrentTotalValuation).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.CurrentTokenPrice).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.CurrentTotalNumberOfTokens).IsRequired();
            builder.Property(e => e.CurrentAvailableNumberOfTokens).IsRequired();
            builder.Property(e => e.CurrentTotalTokensValuation).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(a => a.AddressDetails)
                    .WithOne(ad => ad.Property)
                    .HasForeignKey<PropertyAddressDetailsModel>(ad => ad.PropertyID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            builder.ToTable("tbl_pe_Property");
        }
    }
}


