namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyTradeRecordsVM
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public decimal CurrentTokenPrice { get; set; }
        public int CurrentTotalNumberOfTokens { get; set; }
        public int CurrentAvailableNumberOfTokens { get; set; }
        public int CurrentTotalTokensValuation { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public PropertyVM? Property { get; set; }
    }
}
