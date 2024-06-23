using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PropertyExchange.Core.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class UserPortfolioConfiguration : IEntityTypeConfiguration<UserHoldingModel>
    {
        void IEntityTypeConfiguration<UserHoldingModel>.Configure(EntityTypeBuilder<UserHoldingModel> builder)
        {
            //builder.HasKey(e => new { e.UserEmail, e.UserPhoneNumber });
            builder.Property(e => e.PropertyName).IsRequired();
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.UserDetails)
            .WithMany(a => a.UserPortfolio)
            .HasForeignKey(ad => new { ad.UserEmail, ad.UserPhoneNumber })
            .HasPrincipalKey(ad => new { ad.UserEmail, ad.UserPhoneNumber })
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.HasOne(p => p.Property)
            .WithMany(u => u.UserHolding)
            .HasForeignKey(ad => ad.PropertyID)
            .HasPrincipalKey(ad => ad.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_UserPortfolio");
        }
    }
}
