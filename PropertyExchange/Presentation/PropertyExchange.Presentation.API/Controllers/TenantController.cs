using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Presentation.API.Models;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using PropertyExchange.Presentation.API.Models.User;

namespace PropertyExchange.Presentation.API.Controllers
{
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : Controller
    {
        private readonly AppSettings _appSettings;

        private readonly ITenantUseCase _tenantUsecase;
        private readonly IPropertyUseCase _propertyUseCase;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string projectKey = "allTenants";

        public TenantController(ITenantUseCase tenantUsecase, IPropertyUseCase propertyUseCase, IMapper mapper, IOptions<AppSettings> appSettings, IMemoryCache cache)
        {
            this._tenantUsecase = tenantUsecase;
            this._propertyUseCase = propertyUseCase;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;
            this._cache = cache;
        }

        //TENANT
        [HttpGet("{propertyID}")]
        public async Task<IActionResult> CheckIfTenantExists(string propertyID)
        {
            if (!propertyID.IsNullOrEmpty())
            {
                var tenant = this._mapper.Map<List<TenantVM>>(await this._tenantUsecase.CheckIfTenantExists(propertyID));
                return this.Ok(tenant);
            }

            return this.NotFound("Property does not have any active tenants");
        }

        [HttpGet("getalltenants")]
        public async Task<IActionResult> GetAllTenants()
        {
            var allTenants = await this._tenantUsecase.GetAllTenantsAsync().ConfigureAwait(false);
            if (allTenants == null)
            {
                return this.NotFound();
            }

            var tenants = this._mapper.Map<List<TenantVM>>(allTenants);
            this._cache.Set(this.projectKey, tenants);
            return this.Ok(tenants);
        }

        [HttpPost("posttenant")]
        public async Task<IActionResult> PostTenant([FromBody] TenantVM tenant)
        {
            if (tenant == null)
            {
                return this.BadRequest("Invalid tenant");
            }

            if (tenant.PropertyID.IsNullOrEmpty() || tenant.TenantID.IsNullOrEmpty()
                || tenant.Name.IsNullOrEmpty() || tenant.Description.IsNullOrEmpty()
                || tenant.Email.IsNullOrEmpty() || tenant.PhoneNumber.IsNullOrEmpty()
                || tenant.Country.IsNullOrEmpty() || tenant.IsActive.ToString().IsNullOrEmpty()
                || tenant.TenancyType.IsNullOrEmpty())
            {
                return this.BadRequest("Tenant details are required");
            }

            var tenant1 = this._mapper.Map<TenantModel>(tenant);
            var newTenant = await this._tenantUsecase.AddTenantAsync(tenant1).ConfigureAwait(false);
            if (newTenant[0] == "" && newTenant[1] == "")
            {
                return this.Ok("A property already exists with the given PropertyID and Name");
            }
            return this.Created("Created a new tenant", new { url = "Created a new tenant with TenantID : " + newTenant[0] + " and Name : " + newTenant[1] });
        }


        //TENANT LEASE DETAILS
        [HttpPost("posttenantleasedetails")]
        public async Task<IActionResult> PostTenantLeaseDetailsAsync([FromBody] TenantLeaseDetailsVM tenant)
        {
            if (tenant == null)
            {
                return this.BadRequest("Invalid tenant lease details");
            }

            if (tenant.PropertyID.IsNullOrEmpty() || tenant.TenantID.IsNullOrEmpty() 
                || tenant.LeaseAdvanceAmount.ToString().IsNullOrEmpty() || tenant.LeaseStartDate.IsNullOrEmpty() 
                || tenant.LeaseEndDate.IsNullOrEmpty() || tenant.LeaseTenureInMonths.ToString().IsNullOrEmpty() 
                || tenant.RentPerSft.ToString().IsNullOrEmpty() || tenant.TotalAreaInSft.ToString().IsNullOrEmpty() 
                || tenant.EscalationInPercentage.ToString().IsNullOrEmpty() || tenant.EscalationTenure.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Lease details are required");
            }

            var tenant1 = this._mapper.Map<TenantLeaseDetailsModel>(tenant);
            var newTenant = await this._tenantUsecase.PostTenantLeaseDetailsAsync(tenant1).ConfigureAwait(false);
            if (newTenant[0] == "" && newTenant[1] == "")
            {
                return this.Ok("Either the requested tenant does not exists or similar lease data has already been logged in for the given property");
            }
            return this.Created("Created a new lease details record", new { url = "Created tenant lease details successfully" });
        }


        //TENANT RENT PAYMENT DETAILS
        [HttpPost("posttenantrentpaymentdetails")]
        public async Task<IActionResult> PostTenantRentPaymentDetailsAsync([FromBody] TenantRentPaymentDetailsVM tenant)
        {
            if (tenant == null)
            {
                return this.BadRequest("Invalid tenant rent payment details");
            }

            if (tenant.PropertyID.IsNullOrEmpty() || tenant.TenantID.IsNullOrEmpty()
                || tenant.ExpenseType.IsNullOrEmpty() || tenant.ExpenseAmount.ToString().IsNullOrEmpty()
                || tenant.ExpenseIncurredOnDate.IsNullOrEmpty() || tenant.ExpenseMonth.ToString().IsNullOrEmpty()
                || tenant.ExpenseYear.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Rent payment details are required");
            }

            var tenant1 = this._mapper.Map<TenantRentPaymentDetailsModel>(tenant);
            var newTenant = await this._tenantUsecase.PostTenantRentPaymentDetailsAsync(tenant1).ConfigureAwait(false);
            if (newTenant[0] == "" && newTenant[1] == "")
            {
                return this.Ok("Either the requested tenant does not exists or similar rent payment data has already been logged in for the given property");
            }

            PropertyIncomeBreakOutVM property = new PropertyIncomeBreakOutVM
            {
                PropertyID = tenant.PropertyID,
                IncomeType = tenant.ExpenseType,
                IncomeAmount = tenant.ExpenseAmount,
                IncomeGeneratedOnDate = tenant.ExpenseIncurredOnDate,
                IncomeMonth = tenant.ExpenseMonth,
                IncomeYear = tenant.ExpenseYear,
                CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString(),
                Property = null
            };
            var propertyModel = this._mapper.Map<PropertyIncomeBreakOutModel>(property);
            await this._propertyUseCase.AddPropertyIncomeBreakOutAsync(propertyModel).ConfigureAwait(false);
            return this.Created("Created a new lease details record", new { url = "Created tenant rent payment details successfully" });
        }
    }
}
