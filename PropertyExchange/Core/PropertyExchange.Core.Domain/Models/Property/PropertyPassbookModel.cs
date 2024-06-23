namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyPassbookModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string TxnType { get; set; }
        public required string TxnNumber { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public required decimal TotalTxnValue { get; set; }
        public required string OrderDateTime { get; set; }
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public required PropertyModel Property { get; set; }

    }
}
