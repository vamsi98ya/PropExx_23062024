using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class PropertyIncomeBreakOutConfiguration : IEntityTypeConfiguration<PropertyIncomeBreakOutModel>
    {
        void IEntityTypeConfiguration<PropertyIncomeBreakOutModel>.Configure(EntityTypeBuilder<PropertyIncomeBreakOutModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.IncomeType).IsRequired();
            builder.Property(e => e.Description).HasColumnType("text");
            builder.Property(e => e.IncomeAmount).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.IncomeGeneratedOnDate).IsRequired();
            builder.Property(e => e.IncomeMonth).IsRequired();
            builder.Property(e => e.IncomeYear).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Property)
            .WithMany(a => a.IncomeBreakOut)
            .HasForeignKey(b => b.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_PropertyIncomeBreakOut");

        }
    }
}
