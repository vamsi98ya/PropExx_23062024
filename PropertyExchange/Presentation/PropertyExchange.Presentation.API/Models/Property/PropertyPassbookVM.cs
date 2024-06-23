namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyPassbookVM
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string TxnType { get; set; }
        public required string TxnNumber { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public required decimal TotalTxnValue { get; set; }
        public required string OrderDateTime { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public PropertyVM? Property { get; set; }

    }
}
