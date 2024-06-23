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
    internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLoginModel>
    {
        void IEntityTypeConfiguration<UserLoginModel>.Configure(EntityTypeBuilder<UserLoginModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasKey(e => new { e.UserEmail, e.UserPhoneNumber, e.LoginDateTime });
            builder.Property(e => e.Role).IsRequired();
            //builder.Property(e => e.LoginDateTime);
            builder.ToTable("tbl_pe_UserLogin");
        }
    }
}
