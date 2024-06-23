using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Configurations
{
    internal class PropertyTradeRecordsConfiguration : IEntityTypeConfiguration<PropertyTradeRecordsModel>
    {
        void IEntityTypeConfiguration<PropertyTradeRecordsModel>.Configure(EntityTypeBuilder<PropertyTradeRecordsModel> builder)
        {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.CurrentTokenPrice).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.CurrentTotalNumberOfTokens).IsRequired();
            builder.Property(e => e.CurrentAvailableNumberOfTokens).IsRequired();
            builder.Property(e => e.CurrentTotalTokensValuation).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Property)
            .WithMany(a => a.TradeRecords)
            .HasForeignKey(b => b.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_PropertyTradeRecords");
        }
    }
}

