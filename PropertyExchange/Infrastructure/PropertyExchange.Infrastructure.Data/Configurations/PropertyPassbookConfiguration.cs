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
    internal class PropertyPassbookConfiguration : IEntityTypeConfiguration<PropertyPassbookModel>
    {
        void IEntityTypeConfiguration<PropertyPassbookModel>.Configure(EntityTypeBuilder<PropertyPassbookModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.TxnType).IsRequired();
            builder.Property(e => e.TxnNumber).IsRequired();
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Price).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.TotalTxnValue).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.OrderDateTime).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Property)
            .WithMany(a => a.Passbook)
            .HasForeignKey(b => b.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_PropertyPassbook");

        }
    }
}

