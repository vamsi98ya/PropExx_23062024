using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Contracts
{
    public interface ITenantUseCase
    {
        //TENANT
        Task<List<TenantModel>> CheckIfTenantExists(string propertyID);
        Task<List<TenantModel>> GetAllTenantsAsync();
        Task<List<string>> AddTenantAsync(TenantModel tenant);

        //TENANT LEASE DETAILS
        Task<List<string>> PostTenantLeaseDetailsAsync(TenantLeaseDetailsModel tenant);

        //TENANT RENT PAYMENT DETAILS
        Task<List<string>> PostTenantRentPaymentDetailsAsync(TenantRentPaymentDetailsModel tenant);
    }
}
