using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class PropertyAddressDetailsConfiguration : IEntityTypeConfiguration<PropertyAddressDetailsModel>
    {
        void IEntityTypeConfiguration<PropertyAddressDetailsModel>.Configure(EntityTypeBuilder<PropertyAddressDetailsModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.AddressLine1).IsRequired();
            builder.Property(e => e.AddressLine2);
            builder.Property(e => e.AddressLine3);
            builder.Property(e => e.Landmark);
            builder.Property(e => e.City).IsRequired();
            builder.Property(e => e.State).IsRequired();
            builder.Property(e => e.Country).IsRequired();
            builder.Property(e => e.PINCode).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.ToTable("tbl_pe_PropertyAddressDetails");
        }
    }
        
}

