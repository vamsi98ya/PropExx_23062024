using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Infrastructure.Data.Repositories;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class UserRegistrationConfiguration: IEntityTypeConfiguration<UserRegistrationLoginModel>
    {
        void IEntityTypeConfiguration<UserRegistrationLoginModel>.Configure(EntityTypeBuilder<UserRegistrationLoginModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.HasKey(e => new { e.UserEmail, e.UserPhoneNumber });
            builder.Property(e => e.UserPassword).IsRequired();
            builder.Property(e => e.Salt).IsRequired();
            builder.Property(e => e.Role).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(a => a.UserDetails)
                    .WithOne(ad => ad.User)
                    .HasForeignKey<UserModel>(ad => new { ad.UserEmail, ad.UserPhoneNumber })
                    .HasPrincipalKey<UserRegistrationLoginModel>(ad => new { ad.UserEmail, ad.UserPhoneNumber })
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            builder.ToTable("tbl_pe_UserRegistration");
        }
    }
}
