using PropertyExchange.Presentation.API.Models.Property;

namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserHoldingVM
    {
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string PropertyID { get; set; }
        public string? PropertyName { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public UserVM? UserDetails { get; set; }
        public PropertyVM? Property { get; set; }

    }
}
