using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Contracts.DTO;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Models;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.UseCases
{
    public class PropertyUseCase:IPropertyUseCase
    {
        private readonly IPropertyRepo _propertyRepository;

        public PropertyUseCase(IPropertyRepo propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        //PROPERTY
        async Task<PropertyModel> IPropertyUseCase.CheckIfPropertyExists(string propertyID, string key)
        {
            var existingProprty = await _propertyRepository.CheckIfPropertyExists(propertyID);
            if (existingProprty != null)
            {
               return existingProprty;
            }
            throw new KeyNotFoundException();
        }

        Task<List<PropertyModel>> IPropertyUseCase.GetAllPropertiesAsync()
        {
            return _propertyRepository.GetAllPropertiesAsync();
        }

        async Task<List<string>> IPropertyUseCase.AddPropertyAsync(PropertyModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty == null)
            {
                return await _propertyRepository.AddPropertyAsync(property);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }

        //PROPERTY ADDRESS
        async Task<List<string>> IPropertyUseCase.AddPropertyAddressAsync(PropertyAddressDetailsModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyAddressExists(property.PropertyID);
            if (existingProperty != null)
            {
                return new List<string> { "", "" };
            }
            else
            {
                return await _propertyRepository.AddPropertyAddressAsync(property);
            }

        }

        //PROPERTY PURCHASE OR SALE EXPENSES BREAKOUT
        async Task<List<string>> IPropertyUseCase.AddPropertyPurchaseOrSaleExpensesBreakOutAsync(PropertyPurchaseOrSaleExpensesBreakOutModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyPurchaseOrSaleExpensesBreakOutAsync(property);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }

        //PROPERTY PASSBOOK
        async Task<List<string>> IPropertyUseCase.AddPropertyPassbookAsync(PropertyPassbookModel property, UserPassbookModel user)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyPassbookAsync(property, user);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }
        
        //PROPERTY INCOME BREAKOUT
        async Task<List<string>> IPropertyUseCase.AddPropertyIncomeBreakOutAsync(PropertyIncomeBreakOutModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyIncomeBreakOutAsync(property);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }


        //PROPERTY EXPENSES BREAKOUT
        async Task<List<string>> IPropertyUseCase.AddPropertyExpensesBreakOutAsync(PropertyExpensesBreakOutModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyExpensesBreakOutAsync(property);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }


        //PROPERTY VALUATION METRICS
        async Task<List<string>> IPropertyUseCase.AddPropertyValuationMetricsAsync(PropertyValuationMetricsModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyValuationMetricsAsync(property);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }


        //PROPERTY PROJECTED VALUATION METRICS
        async Task<List<string>> IPropertyUseCase.AddPropertyProjectedValuationMetricsAsync(PropertyProjectedValuationMetricsModel property)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyProjectedValuationMetricsAsync(property);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }


        //PROPERTY TRADE RECORDS
        async Task<List<string>> IPropertyUseCase.AddPropertyTradeRecordAsync(PropertyTradeRecordsModel property, string tradeType)
        {
            var existingProperty = await _propertyRepository.CheckIfPropertyExists(property.PropertyID);
            if (existingProperty != null)
            {
                return await _propertyRepository.AddPropertyTradeRecordAsync(property, tradeType);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }
    }
}

