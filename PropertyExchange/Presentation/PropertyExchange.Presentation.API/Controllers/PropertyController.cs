using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Contracts.DTO;
using PropertyExchange.Core.Domain.Models;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Presentation.API.Models;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using PropertyExchange.Presentation.API.Models.User;

namespace PropertyExchange.Presentation.API.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        private readonly IPropertyUseCase _propertyUsecase;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string projectKey = "allProperties";

        public PropertyController(IPropertyUseCase propertyUsecase, IMapper mapper, IOptions<AppSettings> appSettings, IMemoryCache cache)
        {
            this._propertyUsecase = propertyUsecase;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;
            this._cache = cache;
        }


        //PROPERTY
        [HttpGet("{propertyID}")]
        public async Task<IActionResult> CheckIfPropertyExists(string propertyID)
        {
            if (!propertyID.IsNullOrEmpty())
            {
                var autUser = this._mapper.Map<PropertyVM>(await this._propertyUsecase.CheckIfPropertyExists(propertyID.ToString(), this._appSettings.Key));
                return this.Ok(autUser);
            }
           return this.NotFound("Property does not exist");
        }

        [HttpGet("getallproperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var allProperties = await this._propertyUsecase.GetAllPropertiesAsync().ConfigureAwait(false);
            if (allProperties == null)
            {
                return this.NotFound();
            }

            var properties = this._mapper.Map<List<PropertyVM>>(allProperties);
            this._cache.Set(this.projectKey, properties);
            return this.Ok(properties);
        }

        [HttpPost("postproperty")]
        public async Task<IActionResult> PostProperty([FromBody] PropertyVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid property");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.Name.IsNullOrEmpty()
                || property.Description.IsNullOrEmpty() || property.Location.IsNullOrEmpty()
                || property.PropertyType.IsNullOrEmpty() || property.HoldingType.IsNullOrEmpty()
                || property.OverallAreaInSft.ToString().IsNullOrEmpty() || property.IsActive.ToString().IsNullOrEmpty()
                || property.IsAvailableForInvesting.ToString().IsNullOrEmpty() || property.InitialTotalValuation.ToString().IsNullOrEmpty()
                || property.InitialTokenPrice.ToString().IsNullOrEmpty() || property.InitialTotalNumberOfTokens.ToString().IsNullOrEmpty()
                || property.InitialAvailableNumberOfTokens.ToString().IsNullOrEmpty() || property.InitialTotalTokensValuation.ToString().IsNullOrEmpty()
                || property.CurrentTotalValuation.ToString().IsNullOrEmpty()
                || property.CurrentTokenPrice.ToString().IsNullOrEmpty() || property.CurrentTotalNumberOfTokens.ToString().IsNullOrEmpty()
                || property.CurrentAvailableNumberOfTokens.ToString().IsNullOrEmpty() || property.CurrentTotalTokensValuation.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Property details are required");
            }

            var property1 = this._mapper.Map<PropertyModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("A property already exists with the given PropertyID and Name");
            }
            return this.Created("Created a new property", new { url = "Created a new property with PropertyID : " + newProperty[0] + " and Name : " + newProperty[1] });
        }


        //PROPERTY ADDRESS
        [HttpPost("postpropertyaddress")]
        public async Task<IActionResult> PostPropertyAddress([FromBody] PropertyAddressDetailsVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid property address");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.AddressLine1.IsNullOrEmpty()
                || property.City.IsNullOrEmpty() || property.State.IsNullOrEmpty()
                || property.Country.IsNullOrEmpty() || property.PINCode.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Address details are required");
            }

            var property1 = this._mapper.Map<PropertyAddressDetailsModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyAddressAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("An address already exists for the given property");
            }
            return this.Created("Created a new address", new { url = "Created a new address for the given property" });
        }


        //PROPERTY PURCHASE OR SALE EXPENSES BREAKOUT
        [HttpPost("postpropertypurchaseorsaleexpensesbreakout")]
        public async Task<IActionResult> PostPropertyPurchaseOrSaleExpensesBreakOut([FromBody] PropertyPurchaseOrSaleExpensesBreakOutVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid property address");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.PurchaseOrSale.IsNullOrEmpty()
                || property.ExpenseType.IsNullOrEmpty() || property.ExpenseAmount.ToString().IsNullOrEmpty()
                || property.ExpenseIncurredOnDate.IsNullOrEmpty() || property.ExpenseMonth.ToString().IsNullOrEmpty()
                || property.ExpenseYear.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Purchase Or Sale details are required");
            }

            var property1 = this._mapper.Map<PropertyPurchaseOrSaleExpensesBreakOutModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyPurchaseOrSaleExpensesBreakOutAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
            }
            return this.Created("Created a new transaction", new { url = "Transaction logged in successfully" });
        }


        //PROPERTY PASSBOOK
        //[HttpPost("postpropertypassbook")]
        //public async Task<IActionResult> PostPropertyPassbook([FromBody] PropertyPassbookVM property)
        //{
        //    if (property == null)
        //    {
        //        return this.BadRequest("Invalid transaction");
        //    }

        //    if (property.PropertyID.IsNullOrEmpty() || property.TxnType.IsNullOrEmpty()
        //        || property.Quantity.ToString().IsNullOrEmpty() || property.Price.ToString().IsNullOrEmpty()
        //        || property.OrderDateTime.IsNullOrEmpty())
        //    {
        //        return this.BadRequest("Transaction details are required");
        //    }

        //    var property1 = this._mapper.Map<PropertyPassbookModel>(property);
        //    var newProperty = await this._propertyUsecase.AddPropertyPassbookAsync(property1).ConfigureAwait(false);
        //    if (newProperty[0] == "" && newProperty[1] == "")
        //    {
        //        return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
        //    }
        //    return this.Created("Created a new transaction", new { url = "Transaction logged in successfully" });
        //}


        //PROPERTY INCOME BREAKOUT
        [HttpPost("postpropertyincomebreakout")]
        public async Task<IActionResult> PostPropertyIncomeBreakOut([FromBody] PropertyIncomeBreakOutVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid transaction");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.IncomeType.IsNullOrEmpty()
                || property.IncomeAmount.ToString().IsNullOrEmpty() || property.IncomeGeneratedOnDate.IsNullOrEmpty()
                || property.IncomeMonth.ToString().IsNullOrEmpty() || property.IncomeYear.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Transaction details are required");
            }

            var property1 = this._mapper.Map<PropertyIncomeBreakOutModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyIncomeBreakOutAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
            }
            return this.Created("Created a new transaction", new { url = "Transaction logged in successfully" });
        }


        //PROPERTY INCOME BREAKOUT
        [HttpPost("postpropertyexpensesbreakout")]
        public async Task<IActionResult> PostPropertyExpensesBreakOut([FromBody] PropertyExpensesBreakOutVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid transaction");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.ExpenseType.IsNullOrEmpty()
                || property.ExpenseAmount.ToString().IsNullOrEmpty() || property.ExpenseIncurredOnDate.IsNullOrEmpty()
                | property.ExpenseMonth.ToString().IsNullOrEmpty() || property.ExpenseYear.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Transaction details are required");
            }

            var property1 = this._mapper.Map<PropertyExpensesBreakOutModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyExpensesBreakOutAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
            }
            return this.Created("Created a new transaction", new { url = "Transaction logged in successfully" });
        }


        //PROPERTY VALUATION METRICS
        [HttpPost("postpropertyvaluationmetrics")]
        public async Task<IActionResult> PostPropertyValuationMetrics([FromBody] PropertyValuationMetricsVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid transaction");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.PricePerSft.ToString().IsNullOrEmpty()
                || property.TotalSft.ToString().IsNullOrEmpty() || property.Month.ToString().IsNullOrEmpty()
                || property.Year.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Property details are required");
            }

            var property1 = this._mapper.Map<PropertyValuationMetricsModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyValuationMetricsAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or valuation metrics is not available for the given property");
            }
            return this.Created("Created a new transaction", new { url = "Metrics added successfully" });
        }


        //PROPERTY PROJECTED VALUATION METRICS
        [HttpPost("postpropertyprojectedvaluationmetrics")]
        public async Task<IActionResult> PostPropertyProjectedValuationMetrics([FromBody] PropertyProjectedValuationMetricsVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid transaction");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.PricePerSft.ToString().IsNullOrEmpty()
                || property.TotalSft.ToString().IsNullOrEmpty() || property.Month.ToString().IsNullOrEmpty()
                || property.Year.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Property details are required");
            }

            var property1 = this._mapper.Map<PropertyProjectedValuationMetricsModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyProjectedValuationMetricsAsync(property1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or valuation metrics is not available for the given property");
            }
            return this.Created("Created a new transaction", new { url = "Metrics added successfully" });
        }


        //PROPERTY TRADE RECORDS
        [HttpPost("postpropertytraderecords")]
        public async Task<IActionResult> PostPropertyTradeRecords([FromBody] PropertyTradeRecordsVM property)
        {
            if (property == null)
            {
                return this.BadRequest("Invalid transaction");
            }

            if (property.PropertyID.IsNullOrEmpty() || property.CurrentTokenPrice.ToString().IsNullOrEmpty()
                || property.CurrentTotalNumberOfTokens.ToString().IsNullOrEmpty() || property.CurrentAvailableNumberOfTokens.ToString().IsNullOrEmpty()
                || property.CurrentTotalTokensValuation.ToString().IsNullOrEmpty())
            {
                return this.BadRequest("Trade details are required");
            }

            var property1 = this._mapper.Map<PropertyTradeRecordsModel>(property);
            var newProperty = await this._propertyUsecase.AddPropertyTradeRecordAsync(property1, "GENERALTRADE").ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
            }
            return this.Created("Created a new transaction", new { url = "Transaction logged in successfully" });
        }
    }
}

 