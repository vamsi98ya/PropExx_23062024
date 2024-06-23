namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyProjectedValuationMetricsModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public decimal PricePerSft { get; set; }
        public decimal TotalSft { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public required PropertyModel Property { get; set; }
    }
}
