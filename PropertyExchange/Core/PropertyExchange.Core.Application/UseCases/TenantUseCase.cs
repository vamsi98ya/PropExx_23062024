using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.UseCases
{
    public class TenantUseCase:ITenantUseCase
    {
        private readonly ITenantRepo _tenantRepository;

        public TenantUseCase(ITenantRepo tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        //TENANT
        async Task<List<TenantModel>> ITenantUseCase.CheckIfTenantExists(string propertyID)
        {
            var existingTenant = await _tenantRepository.CheckIfTenantDetailsExists(propertyID);
            if (existingTenant != null)
            {
                var existingHolding = await _tenantRepository.GetTenantsByPropertyID(propertyID);
                return existingTenant;
            }
            throw new KeyNotFoundException();
        }

        Task<List<TenantModel>> ITenantUseCase.GetAllTenantsAsync()
        {
            return _tenantRepository.GetAllTenantsAsync();
        }

        async Task<List<string>> ITenantUseCase.AddTenantAsync(TenantModel tenant)
        {
            var existingTenant = await _tenantRepository.CheckIfTenantExists(tenant.TenantID, tenant.PropertyID);
            if (existingTenant == null)
            {
                return await _tenantRepository.AddTenantAsync(tenant);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }


        //TENANT LEASE DETAILS
        async Task<List<string>> ITenantUseCase.PostTenantLeaseDetailsAsync(TenantLeaseDetailsModel tenant)
        {
            var existingTenant = await _tenantRepository.CheckIfTenantExists(tenant.TenantID, tenant.PropertyID);
            if (existingTenant != null)
            {
                return await _tenantRepository.PostTenantLeaseDetailsAsync(tenant);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }


        //TENANT RENT PAYMENT DETAILS
        async Task<List<string>> ITenantUseCase.PostTenantRentPaymentDetailsAsync(TenantRentPaymentDetailsModel tenant)
        {
            var existingTenant = await _tenantRepository.CheckIfTenantExists(tenant.TenantID, tenant.PropertyID);
            if (existingTenant != null)
            {
                return await _tenantRepository.PostTenantRentPaymentDetailsAsync(tenant);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }
    }
}
