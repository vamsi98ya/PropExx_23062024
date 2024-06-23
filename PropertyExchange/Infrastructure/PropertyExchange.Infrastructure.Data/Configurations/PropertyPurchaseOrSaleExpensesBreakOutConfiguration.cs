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
    internal class PropertyPurchaseOrSaleExpensesBreakOutConfiguration:IEntityTypeConfiguration<PropertyPurchaseOrSaleExpensesBreakOutModel>
    {
         void IEntityTypeConfiguration<PropertyPurchaseOrSaleExpensesBreakOutModel>.Configure(EntityTypeBuilder<PropertyPurchaseOrSaleExpensesBreakOutModel> builder)
         {
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.ExpenseType).IsRequired();
            builder.Property(e => e.Description);
            builder.Property(e => e.ExpenseAmount).HasColumnType("decimal(,2)").IsRequired();
            builder.Property(e => e.ExpenseIncurredOnDate).IsRequired();
            builder.Property(e => e.ExpenseMonth).IsRequired();
            builder.Property(e => e.ExpenseYear).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.LastUpdatedDate);
            builder.HasOne(b => b.Property)
            .WithMany(a => a.PurchaseOrSaleExpensesBreakOut)
            .HasForeignKey(b => b.PropertyID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
            builder.ToTable("tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut");

        }
    }
}
