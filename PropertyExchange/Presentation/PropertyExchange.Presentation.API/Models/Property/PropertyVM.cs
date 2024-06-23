using PropertyExchange.Presentation.API.Models.Tenant;
using PropertyExchange.Presentation.API.Models.User;

namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyVM
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
        public bool IsAvailableForInvesting { get; set; }
        public decimal InitialTotalValuation { get; set; } //Will get it only once from PropertyPurchaseOrSaleExpensesBreakout (Original Property Valuation)
        public decimal InitialTokenPrice { get; set; } //Will get it only once from PropertyTradeRecords (0.01% of InitialTotalTokensValuation)
        public int InitialTotalNumberOfTokens { get; set; } //Will get it only once from PropertyTradeRecords (InitialTotalTokensValuation/InitialTokenPrice)
        public int InitialAvailableNumberOfTokens { get; set; } //Will get it only once from PropertyTradeRecords (InitialTotalNumberOfTokens)
        public int InitialTotalTokensValuation { get; set; } //Will get it only once from PropertyTradeRecords (Total valuation of the portion which is brought into trading)
        public decimal CurrentTotalValuation { get; set; } //Will get it from PropertyValuationMetrics (Current Property Valuation)
        public decimal CurrentTokenPrice { get; set; } //Will get it from PropertyTradeRecords (0.01% of CurrentTotalTokensValuation)
        public int CurrentTotalNumberOfTokens { get; set; } //Will get it from PropertyTradeRecords
        public int CurrentAvailableNumberOfTokens { get; set; } //Will get it from PropertyTradeRecords
        public int CurrentTotalTokensValuation { get; set; } //Will get it from PropertyTradeRecords
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public  PropertyAddressDetailsVM? AddressDetails { get; set; }
        public  List<PropertyPurchaseOrSaleExpensesBreakOutVM>? PurchaseOrSaleExpensesBreakOut { get; set; }
        public List<PropertyPassbookVM>? Passbook { get; set; }
        public List<PropertyIncomeBreakOutVM>? IncomeBreakOut { get; set; }
        public  List<PropertyExpensesBreakOutVM>? ExpensesBreakOut { get; set; }
        public  List<PropertyValuationMetricsVM>? ValuationMetrics { get; set; }
        public  List<PropertyProjectedValuationMetricsVM>? ProjectedValuationMetrics { get; set; }
        public  List<PropertyTradeRecordsVM>? TradeRecords { get; set; }
        public List<TenantVM>? TenantDetails { get; set; }
        public List<UserPassbookVM>? UserPassbook { get; set; }
        public List<UserHoldingVM>? UserHolding { get; set; }

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
