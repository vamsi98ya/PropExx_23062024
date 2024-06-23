using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Infrastructure.Data.Common;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Repositories
{
    public class TenantRepo:ITenantRepo
    {
        private readonly PEDbContext _context;

        public TenantRepo(PEDbContext context)
        {
            this._context = context;
        }

        //TENANT
        async Task<TenantModel> ITenantRepo.CheckIfTenantExists(string tenantID, string propertyID)
        {
            var tenant = await this._context.tbl_pe_Tenant
                .SingleOrDefaultAsync(x => x.PropertyID == propertyID && x.TenantID == tenantID).ConfigureAwait(true);
                if (tenant != null)
                {
                    await this._context.tbl_pe_TenantLeaseDetails
                               .SingleOrDefaultAsync(e => e.TenantID == tenant.TenantID).ConfigureAwait(true);

                    await this._context.tbl_pe_TenantRentPaymentDetails
                       .Where(e => e.TenantID == tenant.TenantID)
                       .ToListAsync();
                }
            return tenant;
        }


        async Task<List<TenantModel>> ITenantRepo.CheckIfTenantDetailsExists(string propertyID)
        {
            var tenant = await this._context.tbl_pe_Tenant
                .Where(x => x.PropertyID == propertyID).ToListAsync();
            for (int i = 0; i < tenant.Count; i++)
            {
                if (tenant != null)
                {
                    await this._context.tbl_pe_TenantLeaseDetails
                               .SingleOrDefaultAsync(e => e.TenantID == tenant[i].TenantID).ConfigureAwait(true);

                    await this._context.tbl_pe_TenantRentPaymentDetails
                       .Where(e => e.TenantID == tenant[i].TenantID)
                       .ToListAsync();
                }
            }
            return tenant;
        }
        async Task<List<TenantModel>> ITenantRepo.GetTenantsByPropertyID(string propertyID)
        {
            var tenant = await this._context.tbl_pe_Tenant
                .Where(x => x.PropertyID == propertyID).ToListAsync();
            for (int i = 0; i < tenant.Count; i++)
            {
                if (tenant != null)
                {
                    await this._context.tbl_pe_TenantLeaseDetails
                               .SingleOrDefaultAsync(e => e.TenantID == tenant[i].TenantID).ConfigureAwait(true);

                    await this._context.tbl_pe_TenantRentPaymentDetails
                       .Where(e => e.TenantID == tenant[i].TenantID)
                       .ToListAsync();
                }
            }
            return tenant;
        }

        async Task<List<TenantModel>> ITenantRepo.GetAllTenantsAsync()
        {
            var res = await this._context.tbl_pe_Tenant.ToListAsync();
            if (res.Count > 0)
            {
                foreach (var tenant in res)
                {
                    await this._context.tbl_pe_TenantLeaseDetails
                       .SingleOrDefaultAsync(e => e.TenantID == tenant.TenantID).ConfigureAwait(true);

                    await this._context.tbl_pe_TenantRentPaymentDetails
                       .Where(e => e.TenantID == tenant.TenantID)
                       .ToListAsync();
                }
            }
            return res;
        }

        async Task<List<string>> ITenantRepo.AddTenantAsync(TenantModel tenant)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            tenant.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingTenant = await this._context.tbl_pe_Tenant.SingleOrDefaultAsync(e => e.TenantID == tenant.TenantID 
            && e.PropertyID == tenant.PropertyID).ConfigureAwait(true);
            if (existingTenant != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_Tenant.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            tenant.Id = count + 1;
            this._context.tbl_pe_Tenant.Add(tenant);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { tenant.TenantID, tenant.Name };
        }


        //TENANT LEASE DETAILS
        async Task<List<string>> ITenantRepo.PostTenantLeaseDetailsAsync(TenantLeaseDetailsModel tenant)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            tenant.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingTenant = await this._context.tbl_pe_TenantLeaseDetails
                .SingleOrDefaultAsync(e => (e.TenantID == tenant.TenantID
                && e.PropertyID == tenant.PropertyID
                && e.LeaseAdvanceAmount == tenant.LeaseAdvanceAmount
                && e.LeaseStartDate == tenant.LeaseStartDate
                && e.LeaseEndDate == tenant.LeaseEndDate && e.LeaseTenureInMonths == tenant.LeaseTenureInMonths
                && e.RentPerSft == tenant.RentPerSft && e.TotalAreaInSft == tenant.TotalAreaInSft
                && e.EscalationInPercentage == tenant.EscalationInPercentage && e.EscalationTenure == tenant.EscalationTenure)).ConfigureAwait(true);

            if (existingTenant != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_TenantLeaseDetails.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            tenant.Id = count + 1;
            this._context.tbl_pe_TenantLeaseDetails.Add(tenant);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { tenant.LeaseStartDate, tenant.LeaseEndDate };
        }


        //TENANT RENT PAYMENT DETAILS
        async Task<List<string>> ITenantRepo.PostTenantRentPaymentDetailsAsync(TenantRentPaymentDetailsModel tenant)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            tenant.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingTenant = await this._context.tbl_pe_TenantRentPaymentDetails
                .SingleOrDefaultAsync(e => (e.TenantID == tenant.TenantID
                && e.PropertyID == tenant.PropertyID
                && e.ExpenseType == tenant.ExpenseType
                && e.ExpenseAmount == tenant.ExpenseAmount
                && e.ExpenseIncurredOnDate == tenant.ExpenseIncurredOnDate && e.ExpenseMonth == tenant.ExpenseMonth
                && e.ExpenseYear == tenant.ExpenseYear)).ConfigureAwait(true);

            if (existingTenant != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_TenantRentPaymentDetails.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            tenant.Id = count + 1;
            this._context.tbl_pe_TenantRentPaymentDetails.Add(tenant);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { tenant.ExpenseAmount.ToString(), tenant.ExpenseIncurredOnDate };
        }  
    }
}
