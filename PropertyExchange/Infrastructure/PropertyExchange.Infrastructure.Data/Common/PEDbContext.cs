using Microsoft.EntityFrameworkCore;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Infrastructure.Data.Configurations;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Common
{
    public class PEDbContext : DbContext
    {
        public PEDbContext(DbContextOptions<PEDbContext> options)
            : base(options)
        {
        }

        //USER
        public DbSet<UserRegistrationLoginModel> tbl_pe_UserRegistration { get; set; }
        public DbSet<UserLoginModel> tbl_pe_UserLogin { get; set; }
        public DbSet<UserModel> tbl_pe_UserDetails { get; set; }
        public DbSet<UserFundDetailsModel> tbl_pe_UserFundDetails { get; set; }
        public DbSet<UserPassbookModel> tbl_pe_UserPassbook { get; set; }
        public DbSet<UserHoldingModel> tbl_pe_UserPortfolio { get; set; }


        //PROPERTY
        public DbSet<PropertyModel> tbl_pe_Property { get; set; }
        public DbSet<PropertyAddressDetailsModel> tbl_pe_PropertyAddressDetails { get; set; }
        public DbSet<PropertyPurchaseOrSaleExpensesBreakOutModel> tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut { get; set; }
        public DbSet<PropertyPassbookModel> tbl_pe_PropertyPassbook { get; set; }
        public DbSet<PropertyIncomeBreakOutModel> tbl_pe_PropertyIncomeBreakOut { get; set; }
        public DbSet<PropertyExpensesBreakOutModel> tbl_pe_PropertyExpensesBreakOut { get; set; }
        public DbSet<PropertyValuationMetricsModel> tbl_pe_PropertyValuationMetrics { get; set; }
        public DbSet<PropertyProjectedValuationMetricsModel> tbl_pe_PropertyProjectedValuationMetrics { get; set; }
        public DbSet<PropertyTradeRecordsModel> tbl_pe_PropertyTradeRecords { get; set; }


        //TENANT
        public DbSet<TenantModel> tbl_pe_Tenant { get; set; }
        public DbSet<TenantLeaseDetailsModel> tbl_pe_TenantLeaseDetails { get; set; }
        public DbSet<TenantRentPaymentDetailsModel> tbl_pe_TenantRentPaymentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //USER
            modelBuilder.ApplyConfiguration(new UserRegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new UserFundDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new UserPassbookConfiguration());
            modelBuilder.ApplyConfiguration(new UserPortfolioConfiguration());


            //PROPERTY
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyAddressDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyPurchaseOrSaleExpensesBreakOutConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyPassbookConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyIncomeBreakOutConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyExpensesBreakOutConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyValuationMetricsConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyProjectedValuationMetricsConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTradeRecordsConfiguration());


            //TENANT
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new TenantLeaseDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new TenantRentPaymentDetailsConfiguration());
        }
    }
}

