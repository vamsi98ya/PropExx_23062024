using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Tenant;

namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string Name { get; set; }
        public required string Description{ get; set; }
        public required string Location { get; set; }
        public required string PropertyType { get; set; }
        public required string HoldingType { get; set; }
        public decimal OverallAreaInSft { get; set; }
        public bool IsActive { get; set; }
        //public bool IsAvailableForInvesting { get; set; }
        //public decimal TokenPrice { get; set; }
        //public decimal CurrentTotalPrice { get; set; }
        //public int TotalNumberOfTokens { get; set; }
        //public int AvailableNumberOfTokens { get; set; }
        public bool IsAvailableForInvesting { get; set; }
        public decimal InitialTotalValuation { get; set; } //Will get it only once from PropertyPurchaseOrSaleExpensesBreakout
        public decimal InitialTokenPrice { get; set; } //Will get it only once from PropertyTradeRecords
        public int InitialTotalNumberOfTokens { get; set; } //Will get it only once from PropertyTradeRecords
        public int InitialAvailableNumberOfTokens { get; set; } //Will get it only once from PropertyTradeRecords
        public int InitialTotalTokensValuation { get; set; } //Will get it only once from PropertyTradeRecords
        public decimal CurrentTotalValuation { get; set; } //Will get it from PropertyValuationMetrics
        public decimal CurrentTokenPrice { get; set; } //Will get it from PropertyTradeRecords
        public int CurrentTotalNumberOfTokens { get; set; } //Will get it from PropertyTradeRecords
        public int CurrentAvailableNumberOfTokens { get; set; } //Will get it from PropertyTradeRecords
        public int CurrentTotalTokensValuation { get; set; } //Will get it from PropertyTradeRecords
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public required PropertyAddressDetailsModel AddressDetails { get; set; }
        public required List<PropertyPurchaseOrSaleExpensesBreakOutModel> PurchaseOrSaleExpensesBreakOut { get; set; }
        public required List<PropertyPassbookModel> Passbook { get; set; }
        public List<PropertyIncomeBreakOutModel>? IncomeBreakOut { get; set; }
        public required List<PropertyExpensesBreakOutModel> ExpensesBreakOut { get; set; }
        public required List<PropertyValuationMetricsModel> ValuationMetrics { get; set; }
        public required List<PropertyProjectedValuationMetricsModel> ProjectedValuationMetrics { get; set; }
        public List<PropertyTradeRecordsModel>? TradeRecords { get; set; }
        public List<TenantModel>? TenantDetails { get; set; }
        public List<UserPassbookModel>? UserPassbook { get; set; }
        public List<UserHoldingModel>? UserHolding { get; set; }

    }

    public enum PropertyType
    {
        Residential = 0, 
        Commercial = 1, 
        Industrial = 2, 
        RawLand = 3,
        SpecialPurpose = 4
    }

    public enum HoldingType
    {
        Owned = 0,
        Leased = 1
    }
}
