using PropertyExchange.Presentation.API.Models.Property;

namespace PropertyExchange.Presentation.API.Models.Tenant
{
    public class TenantModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string TenantID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Country { get; set; }
        public required bool IsActive { get; set; }
        public required string TenancyType { get; set; }
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public List<TenantLeaseDetailsModel>? LeaseDetails { get; set; }
        public List<TenantRentPaymentDetailsModel>? RentPaymentDetails { get; set; } 
        public PropertyModel? Property { get; set; }

    }
}
