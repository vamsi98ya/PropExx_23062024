using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Models;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Infrastructure.Data.Common;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Repositories
{
    public class PropertyRepo : IPropertyRepo
    {
        private readonly PEDbContext _context;

        public PropertyRepo(PEDbContext context)
        {
            this._context = context;
        }

        //PROPERTY
        async Task<PropertyModel> IPropertyRepo.CheckIfPropertyExists(string propertyID)
        {
            var property = await this._context.tbl_pe_Property.SingleOrDefaultAsync(x => x.PropertyID == propertyID).ConfigureAwait(true);
            if (property != null)
            {
                await this._context.tbl_pe_PropertyAddressDetails
                       .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);

                await this._context.tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut
                   .Where(e => e.PropertyID == property.PropertyID)
                   .ToListAsync();

                await this._context.tbl_pe_PropertyPassbook
                   .Where(e => e.PropertyID == property.PropertyID)
                   .ToListAsync();

                await this._context.tbl_pe_PropertyIncomeBreakOut
                   .Where(e => e.PropertyID == property.PropertyID)
                   .ToListAsync();

                await this._context.tbl_pe_PropertyExpensesBreakOut
                  .Where(e => e.PropertyID == property.PropertyID)
                   .ToListAsync();

                await this._context.tbl_pe_PropertyValuationMetrics
                   .Where(e => e.PropertyID == property.PropertyID)
                    .ToListAsync();

                await this._context.tbl_pe_PropertyProjectedValuationMetrics
                   .Where(e => e.PropertyID == property.PropertyID)
                    .ToListAsync();

                await this._context.tbl_pe_PropertyTradeRecords
                    .Where(e => e.PropertyID == property.PropertyID)
                    .ToListAsync();

                await this._context.tbl_pe_Tenant
                    .Where(e => e.PropertyID == property.PropertyID)
                    .ToListAsync();
            }
            return property;
        }

        async Task<List<PropertyModel>> IPropertyRepo.GetAllPropertiesAsync()
        {
            var res = await this._context.tbl_pe_Property.ToListAsync();
            if (res.Count > 0)
            {
                foreach (var property in res)
                {
                    await this._context.tbl_pe_PropertyAddressDetails
                       .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);

                    await this._context.tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut
                       .Where(e => e.PropertyID == property.PropertyID)
                       .ToListAsync();

                    await this._context.tbl_pe_PropertyPassbook
                       .Where(e => e.PropertyID == property.PropertyID)
                       .ToListAsync();

                    await this._context.tbl_pe_PropertyIncomeBreakOut
                       .Where(e => e.PropertyID == property.PropertyID)
                       .ToListAsync();

                    await this._context.tbl_pe_PropertyExpensesBreakOut
                      .Where(e => e.PropertyID == property.PropertyID)
                       .ToListAsync();

                    await this._context.tbl_pe_PropertyValuationMetrics
                       .Where(e => e.PropertyID == property.PropertyID)
                        .ToListAsync();

                    await this._context.tbl_pe_PropertyProjectedValuationMetrics
                       .Where(e => e.PropertyID == property.PropertyID)
                        .ToListAsync();

                    await this._context.tbl_pe_PropertyTradeRecords
                       .Where(e => e.PropertyID == property.PropertyID)
                        .ToListAsync();


                    await this._context.tbl_pe_Tenant
                    .Where(e => e.PropertyID == property.PropertyID)
                    .ToListAsync();
                    //if (purchaseOrSale != null)
                    //{
                    //    purchaseOrSale.Property = null;
                    //    //property.PurchaseOrSaleExpensesBreakOut.Add(purchaseOrSale) ;

                    //}
                }
            }
            return res;
        }

        async Task<List<string>> IPropertyRepo.AddPropertyAsync(PropertyModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_Property.SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_Property.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_Property.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { property.PropertyID, property.Name };
        }

        //PROPERTY ADDRESS
        async Task<PropertyAddressDetailsModel> IPropertyRepo.CheckIfPropertyAddressExists(string propertyID)
        {
            var property = await this._context.tbl_pe_PropertyAddressDetails.SingleOrDefaultAsync(x => x.PropertyID == propertyID).ConfigureAwait(true);
            return property;
        }

        async Task<List<string>> IPropertyRepo.AddPropertyAddressAsync(PropertyAddressDetailsModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyAddressDetails
                .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID
                && e.AddressLine1 == property.AddressLine1
                && e.City == property.City
                && e.State == property.State
                && e.Country == property.Country
                && e.PINCode == property.PINCode).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_PropertyAddressDetails.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_PropertyAddressDetails.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { property.PropertyID, property.AddressLine1 };
        }

        //PROPERTY PURCHASE OR SALE EXPENSES BREAKOUT
        async Task<List<string>> IPropertyRepo.AddPropertyPurchaseOrSaleExpensesBreakOutAsync(PropertyPurchaseOrSaleExpensesBreakOutModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.PurchaseOrSale == property.PurchaseOrSale
                && e.ExpenseType == property.ExpenseType
                && e.ExpenseIncurredOnDate == property.ExpenseIncurredOnDate
                && e.ExpenseAmount == property.ExpenseAmount && e.ExpenseMonth == property.ExpenseMonth
                && e.ExpenseYear == property.ExpenseYear)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);

            //Updating Property table with the refreshed initial purchase details
            if (property.PurchaseOrSale.ToUpper() == "PURCHASE" || property.PurchaseOrSale.ToUpper() == "BUY")
            {
                var purchaseAmount = await this._context.tbl_pe_PropertyPurchaseOrSaleExpensesBreakOut
                    .Where(e => (e.PropertyID == property.PropertyID
                    && (e.PurchaseOrSale.ToUpper() == "PURCHASE" || e.PurchaseOrSale.ToUpper() == "BUY"))).SumAsync(e => e.ExpenseAmount);

                var propertyToBeUpdated = await this._context.tbl_pe_Property
                    .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);
                propertyToBeUpdated.InitialTotalValuation = purchaseAmount;
                propertyToBeUpdated.IsActive = true;
                propertyToBeUpdated.LastUpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                await this._context.SaveChangesAsync().ConfigureAwait(true);
            }
            else
            {
                //To be updated: When a property is getting sold off 
                //propertyToBeUpdated.IsActive = true;

            }
            return new List<string> { property.PurchaseOrSale, property.ExpenseType };
        }

        //PROPERTY PASSBOOK
        async Task<List<string>> IPropertyRepo.AddPropertyPassbookAsync(PropertyPassbookModel property, UserPassbookModel user)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var lastUpdatedDt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyPassbook
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.TxnNumber == property.TxnNumber
                && e.TxnType == property.TxnType
                && e.Quantity == property.Quantity
                && e.Price == property.Price
                && e.TotalTxnValue == property.TotalTxnValue
                && e.OrderDateTime == property.OrderDateTime)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var propertyToBeUpdated = await this._context.tbl_pe_Property
                .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);

            var existingHolding = await this._context.tbl_pe_UserPortfolio.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber
            && x.PropertyID == property.PropertyID && x.IsActive == true).ConfigureAwait(true);

            if (propertyToBeUpdated != null)
            {
                if (property.TxnType.ToUpper() == "BUY")
                {
                    if (property.Quantity < propertyToBeUpdated.CurrentAvailableNumberOfTokens)
                    {
                        if (propertyToBeUpdated.IsActive == true && propertyToBeUpdated.IsAvailableForInvesting == true 
                            && propertyToBeUpdated.CurrentTokenPrice != 0)
                        {
                            propertyToBeUpdated.CurrentTokenPrice = property.Price * (decimal)1.0005;
                            propertyToBeUpdated.CurrentAvailableNumberOfTokens = propertyToBeUpdated.CurrentAvailableNumberOfTokens - property.Quantity;
                            propertyToBeUpdated.CurrentTotalTokensValuation = (int)(propertyToBeUpdated.CurrentTokenPrice * propertyToBeUpdated.CurrentAvailableNumberOfTokens);
                            propertyToBeUpdated.IsAvailableForInvesting = true;
                            propertyToBeUpdated.LastUpdatedDate = lastUpdatedDt;
                            await this._context.SaveChangesAsync().ConfigureAwait(true);
                        }
                    }
                    else if (property.Quantity == propertyToBeUpdated.CurrentAvailableNumberOfTokens)
                    {
                        if (propertyToBeUpdated.IsActive == true && propertyToBeUpdated.IsAvailableForInvesting == true
                             && propertyToBeUpdated.CurrentTokenPrice != 0)
                        {
                            propertyToBeUpdated.CurrentTokenPrice = property.Price * (decimal)1.001;
                            propertyToBeUpdated.CurrentAvailableNumberOfTokens = propertyToBeUpdated.CurrentAvailableNumberOfTokens - property.Quantity;
                            propertyToBeUpdated.CurrentTotalTokensValuation = (int)(propertyToBeUpdated.CurrentTokenPrice * propertyToBeUpdated.CurrentAvailableNumberOfTokens);
                            propertyToBeUpdated.IsAvailableForInvesting = false;
                            propertyToBeUpdated.LastUpdatedDate = lastUpdatedDt;
                            await this._context.SaveChangesAsync().ConfigureAwait(true);
                        }
                    }
                    else
                    {
                        return new List<string> { "", "" };
                    }

                    var count = this._context.tbl_pe_PropertyPassbook.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
                    property.Id = count + 1;
                    this._context.tbl_pe_PropertyPassbook.Add(property);
                    await this._context.SaveChangesAsync().ConfigureAwait(true);
                }
                
                if(property.TxnType.ToUpper() == "SELL")
                {
                    if(user.Quantity < existingHolding.Quantity)
                    {
                        if(propertyToBeUpdated.IsActive == true)
                        {
                            propertyToBeUpdated.CurrentTokenPrice = property.Price * (decimal)0.9995;
                            propertyToBeUpdated.CurrentAvailableNumberOfTokens = propertyToBeUpdated.CurrentAvailableNumberOfTokens + property.Quantity;
                            propertyToBeUpdated.CurrentTotalTokensValuation = (int)(propertyToBeUpdated.CurrentTokenPrice * propertyToBeUpdated.CurrentAvailableNumberOfTokens);
                            propertyToBeUpdated.IsAvailableForInvesting = true;
                            propertyToBeUpdated.LastUpdatedDate = lastUpdatedDt;
                            await this._context.SaveChangesAsync().ConfigureAwait(true);
                        }
                    }
                    else if (user.Quantity == existingHolding.Quantity)
                    {
                        if (propertyToBeUpdated.IsActive == true)
                        {
                            propertyToBeUpdated.CurrentTokenPrice = property.Price * (decimal)0.999;
                            propertyToBeUpdated.CurrentAvailableNumberOfTokens = propertyToBeUpdated.CurrentAvailableNumberOfTokens + property.Quantity;
                            propertyToBeUpdated.CurrentTotalTokensValuation = (int)(propertyToBeUpdated.CurrentTokenPrice * propertyToBeUpdated.CurrentAvailableNumberOfTokens);
                            propertyToBeUpdated.IsAvailableForInvesting = true;
                            propertyToBeUpdated.LastUpdatedDate = lastUpdatedDt;
                            await this._context.SaveChangesAsync().ConfigureAwait(true);
                        }
                    }
                    else
                    {
                        return new List<string> { "", "" };
                    }

                    var count = this._context.tbl_pe_PropertyPassbook.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
                    property.Id = count + 1;
                    this._context.tbl_pe_PropertyPassbook.Add(property);
                    await this._context.SaveChangesAsync().ConfigureAwait(true);
                }
            }
            
            return new List<string> { property.Quantity.ToString(), property.Price.ToString() };
        }


        //PROPERTY INCOME BREAKOUT
        async Task<List<string>> IPropertyRepo.AddPropertyIncomeBreakOutAsync(PropertyIncomeBreakOutModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyIncomeBreakOut
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.IncomeType == property.IncomeType
                && e.IncomeAmount == property.IncomeAmount
                && e.IncomeGeneratedOnDate == property.IncomeGeneratedOnDate
                && e.IncomeMonth == property.IncomeMonth && e.IncomeYear == property.IncomeYear)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_PropertyIncomeBreakOut.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_PropertyIncomeBreakOut.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { property.IncomeType, property.IncomeAmount.ToString() };
        }


        //PROPERTY EXPENSE BREAKOUT
        async Task<List<string>> IPropertyRepo.AddPropertyExpensesBreakOutAsync(PropertyExpensesBreakOutModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyExpensesBreakOut
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.ExpenseType == property.ExpenseType
                && e.ExpenseAmount == property.ExpenseAmount
                && e.ExpenseIncurredOnDate == property.ExpenseIncurredOnDate
                && e.ExpenseMonth == property.ExpenseMonth && e.ExpenseYear == property.ExpenseYear)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_PropertyExpensesBreakOut.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_PropertyExpensesBreakOut.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { property.ExpenseType, property.ExpenseAmount.ToString() };
        }


        //PROPERTY VALUATION METRICS
        async Task<List<string>> IPropertyRepo.AddPropertyValuationMetricsAsync(PropertyValuationMetricsModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyValuationMetrics
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.PricePerSft == property.PricePerSft
                && e.TotalSft == property.TotalSft
                && e.Month == property.Month
                && e.Year == property.Year)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            //var count = this._context.tbl_pe_PropertyValuationMetrics.Count();
            var count = this._context.tbl_pe_PropertyValuationMetrics
                        .OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_PropertyValuationMetrics.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);


            //Updating Property table with the refreshed current total price details
            var propertyToBeUpdated = await this._context.tbl_pe_Property
                .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);

            if (propertyToBeUpdated.InitialTotalValuation != (decimal)0 || propertyToBeUpdated.InitialTotalValuation != null)
            {
                var purchaseAmount = await this._context.tbl_pe_PropertyValuationMetrics
                        .Where(e => e.PropertyID == property.PropertyID).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                propertyToBeUpdated.CurrentTotalValuation = purchaseAmount.PricePerSft * purchaseAmount.TotalSft;
                propertyToBeUpdated.IsActive = true;
                propertyToBeUpdated.LastUpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                await this._context.SaveChangesAsync().ConfigureAwait(true);
            }
            return new List<string> { property.PricePerSft.ToString(), property.TotalSft.ToString() };
        }


        //PROPERTY PROJECTED VALUATION METRICS
        async Task<List<string>> IPropertyRepo.AddPropertyProjectedValuationMetricsAsync(PropertyProjectedValuationMetricsModel property)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyProjectedValuationMetrics
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.PricePerSft == property.PricePerSft
                && e.TotalSft == property.TotalSft
                && e.Month == property.Month
                && e.Year == property.Year)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_PropertyProjectedValuationMetrics.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            property.Id = count + 1;
            this._context.tbl_pe_PropertyProjectedValuationMetrics.Add(property);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { property.PricePerSft.ToString(), property.TotalSft.ToString() };
        }


        //PROPERTY TRADE RECORDS
        async Task<List<string>> IPropertyRepo.AddPropertyTradeRecordAsync(PropertyTradeRecordsModel property, string tradeType)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            property.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingProperty = await this._context.tbl_pe_PropertyTradeRecords
                .SingleOrDefaultAsync(e => (e.PropertyID == property.PropertyID
                && e.CurrentTokenPrice == property.CurrentTokenPrice
                && e.CurrentTotalNumberOfTokens == property.CurrentTotalNumberOfTokens
                && e.CurrentAvailableNumberOfTokens == property.CurrentAvailableNumberOfTokens
                && e.CurrentTotalTokensValuation == property.CurrentTotalTokensValuation)).ConfigureAwait(true);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }

            if (tradeType == "GENERALTRADE")
            {
                //Adding Trade Records for General Trading
                var count = this._context.tbl_pe_PropertyTradeRecords.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
                property.Id = count + 1;
                this._context.tbl_pe_PropertyTradeRecords.Add(property);
                await this._context.SaveChangesAsync().ConfigureAwait(true);

                var propertyToBeUpdated = await this._context.tbl_pe_Property
                        .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);

                if (propertyToBeUpdated.InitialTokenPrice == 0 || propertyToBeUpdated.InitialTotalNumberOfTokens == 0
                    || propertyToBeUpdated.InitialAvailableNumberOfTokens == 0 || propertyToBeUpdated.InitialTotalTokensValuation == 0)
                {
                    //Updating Property table with the refreshed initial token details
                    var tradeAmount = await this._context.tbl_pe_PropertyTradeRecords
                            .Where(e => e.PropertyID == property.PropertyID).OrderBy(x => x.Id).FirstOrDefaultAsync();
                    propertyToBeUpdated.InitialTokenPrice = tradeAmount.CurrentTokenPrice;
                    propertyToBeUpdated.InitialTotalNumberOfTokens = tradeAmount.CurrentTotalNumberOfTokens;
                    propertyToBeUpdated.InitialAvailableNumberOfTokens = tradeAmount.CurrentAvailableNumberOfTokens;
                    propertyToBeUpdated.InitialTotalTokensValuation = tradeAmount.CurrentTotalTokensValuation;

                    propertyToBeUpdated.CurrentTokenPrice = tradeAmount.CurrentTokenPrice;
                    propertyToBeUpdated.CurrentTotalNumberOfTokens = tradeAmount.CurrentTotalNumberOfTokens;
                    propertyToBeUpdated.CurrentAvailableNumberOfTokens = tradeAmount.CurrentAvailableNumberOfTokens;
                    propertyToBeUpdated.CurrentTotalTokensValuation = tradeAmount.CurrentTotalTokensValuation;

                    propertyToBeUpdated.IsActive = true;
                    propertyToBeUpdated.IsAvailableForInvesting = true;
                }
                else
                {
                    //Updating Property table with the refreshed current token details
                    var tradeAmount = await this._context.tbl_pe_PropertyTradeRecords
                            .Where(e => e.PropertyID == property.PropertyID).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                    propertyToBeUpdated.CurrentTokenPrice = tradeAmount.CurrentTokenPrice;
                    propertyToBeUpdated.CurrentTotalNumberOfTokens = tradeAmount.CurrentTotalNumberOfTokens;
                    propertyToBeUpdated.CurrentAvailableNumberOfTokens = tradeAmount.CurrentAvailableNumberOfTokens;
                    propertyToBeUpdated.CurrentTotalTokensValuation = tradeAmount.CurrentTotalTokensValuation;

                    propertyToBeUpdated.IsActive = true;
                    propertyToBeUpdated.IsAvailableForInvesting = true;
                }

                propertyToBeUpdated.LastUpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
                await this._context.SaveChangesAsync().ConfigureAwait(true);
            }
            
            if(tradeType == "USERTRADE")
            {
                //Adding Trading Records for User Trading
                var updatedProperty = await this._context.tbl_pe_Property
                        .SingleOrDefaultAsync(e => e.PropertyID == property.PropertyID).ConfigureAwait(true);
                property.CurrentTokenPrice = updatedProperty.CurrentTokenPrice;
                property.CurrentTotalNumberOfTokens = updatedProperty.CurrentTotalNumberOfTokens;
                property.CurrentAvailableNumberOfTokens = updatedProperty.CurrentAvailableNumberOfTokens;
                property.CurrentTotalTokensValuation = updatedProperty.CurrentTotalTokensValuation;
                var count = this._context.tbl_pe_PropertyTradeRecords.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
                property.Id = count + 1;
                this._context.tbl_pe_PropertyTradeRecords.Add(property);
                await this._context.SaveChangesAsync().ConfigureAwait(true);

            }
            return new List<string> { property.CurrentAvailableNumberOfTokens.ToString(), property.CurrentTokenPrice.ToString() };
        }
    }
}
