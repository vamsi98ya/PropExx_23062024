using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Repositories
{
    public interface ITenantRepo
    {
        //TENANT
        Task<TenantModel> CheckIfTenantExists(string tenantID, string propertyID);
        Task<List<TenantModel>> CheckIfTenantDetailsExists(string propertyID);
        Task<List<TenantModel>> GetTenantsByPropertyID(string propertyID);
        Task<List<TenantModel>> GetAllTenantsAsync();
        Task<List<string>> AddTenantAsync(TenantModel tenant);

        //TENANT LEASE DETAILS
        Task<List<string>> PostTenantLeaseDetailsAsync(TenantLeaseDetailsModel tenant);
        
        //TENANT RENT PAYMENT DETAILS
        Task<List<string>> PostTenantRentPaymentDetailsAsync(TenantRentPaymentDetailsModel tenant);

    }
}