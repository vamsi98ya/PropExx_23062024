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
    internal class UserDetailsConfiguration : IEntityTypeConfiguration<UserModel>
    {
        void IEntityTypeConfiguration<UserModel>.Configure(EntityTypeBuilder<UserModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasKey(e => new { e.UserEmail, e.UserPhoneNumber });
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.DOB).IsRequired();
            builder.Property(e => e.Gender).IsRequired();
            builder.Property(e => e.SecondaryEmail);
            builder.Property(e => e.SecondaryPhoneNumber);
            builder.Property(e => e.Occupation).IsRequired();
            builder.Property(e => e.IncomeRange).IsRequired();
            builder.Property(e => e.MaritalStatus).IsRequired();
            builder.Property(e => e.FatherName).IsRequired();
            builder.Property(e => e.AvailableMoneyForInvesting).HasColumnType("decimal(,2)");
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.ToTable("tbl_pe_UserDetails");
        }
    }
}
