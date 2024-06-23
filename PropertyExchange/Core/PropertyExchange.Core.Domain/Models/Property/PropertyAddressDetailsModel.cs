namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyAddressDetailsModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? Landmark { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required int PINCode { get; set; }
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public required PropertyModel Property { get; set; }

    }   
}
