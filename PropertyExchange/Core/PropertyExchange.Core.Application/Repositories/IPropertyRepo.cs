﻿using PropertyExchange.Core.Domain.Models;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Repositories
{
    public interface IPropertyRepo
    {
        //PROPERTY
        Task<PropertyModel> CheckIfPropertyExists(string propertyID);
        Task<List<PropertyModel>> GetAllPropertiesAsync();
        Task<List<string>> AddPropertyAsync(PropertyModel property);

        //PROPERTY ADDRESS
        Task<List<string>> AddPropertyAddressAsync(PropertyAddressDetailsModel property);
        Task<PropertyAddressDetailsModel> CheckIfPropertyAddressExists(string propertyID);

        //PROPERTY PURCHASE OR SALE EXPENSES BREAKOUT
        Task<List<string>> AddPropertyPurchaseOrSaleExpensesBreakOutAsync(PropertyPurchaseOrSaleExpensesBreakOutModel property);

        //PROPERTY PASSBOOK
        Task<List<string>> AddPropertyPassbookAsync(PropertyPassbookModel property, UserPassbookModel user);

        //PROPERTY INCOME BREAKOUT
        Task<List<string>> AddPropertyIncomeBreakOutAsync(PropertyIncomeBreakOutModel property);

        //PROPERTY EXPENSE BREAKOUT
        Task<List<string>> AddPropertyExpensesBreakOutAsync(PropertyExpensesBreakOutModel property);

        //PROPERTY VALUATION METRICS
        Task<List<string>> AddPropertyValuationMetricsAsync(PropertyValuationMetricsModel property);

        //PROPERTY PROJECTED VALUATION METRICS
        Task<List<string>> AddPropertyProjectedValuationMetricsAsync(PropertyProjectedValuationMetricsModel property);

        //PROPERTY TRADE RECORDS
        Task<List<string>> AddPropertyTradeRecordAsync(PropertyTradeRecordsModel property, string tradeType);
    }
}
