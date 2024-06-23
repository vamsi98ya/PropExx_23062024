namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyValuationMetricsVM
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public decimal PricePerSft { get; set; }
        public decimal TotalSft { get; set; }  
        public int Month { get; set; }  
        public int Year { get; set; }  
        public string? CreatedDate { get; set; }  
        public string? LastUpdatedDate { get; set; }  
        public PropertyVM? Property { get; set; }
    }
}
