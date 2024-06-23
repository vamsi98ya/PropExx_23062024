namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserAddressDetailsVM
    {
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? Landmark { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required int PINCode { get; set; }
    }
}
