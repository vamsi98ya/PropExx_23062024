using PropertyExchange.Presentation.API.Models.Property;

namespace PropertyExchange.Presentation.API.Models.Tenant
{
    public class TenantVM
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
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public List<TenantLeaseDetailsVM>? LeaseDetails { get; set; }
        public List<TenantRentPaymentDetailsVM>? RentPaymentDetails { get; set; } 
        public PropertyVM? Property { get; set; }
        
    }
}
