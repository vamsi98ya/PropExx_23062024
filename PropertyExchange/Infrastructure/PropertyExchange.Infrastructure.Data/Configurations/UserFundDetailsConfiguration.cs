using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyExchange.Core.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class UserFundDetailsConfiguration : IEntityTypeConfiguration<UserFundDetailsModel>
    {
        void IEntityTypeConfiguration<UserFundDetailsModel>.Configure(EntityTypeBuilder<UserFundDetailsModel> builder)
        {
            //builder.HasKey(e => new { e.UserEmail, e.UserPhoneNumber });
            builder.Property(e => e.TxnNumber).IsRequired();
            builder.Property(e => e.AddOrWithdraw).IsRequired();
            builder.Property(e => e.TxnAmount).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.ModeOfTxn).IsRequired();
            builder.Property(e => e.TxnDateTime).IsRequired();
            builder.Property(e => e.BankName).IsRequired();
            builder.Property(e => e.AccountNumber).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.UserDetails)
            .WithMany(a => a.UserFundDetails)
            .HasForeignKey(ad => new { ad.UserEmail, ad.UserPhoneNumber })
            .HasPrincipalKey(ad => new { ad.UserEmail, ad.UserPhoneNumber })
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_UserFundDetails");
        }
    }
}
