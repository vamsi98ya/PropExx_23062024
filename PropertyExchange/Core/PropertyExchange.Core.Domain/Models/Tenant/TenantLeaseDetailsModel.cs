using PropertyExchange.Presentation.API.Models.Property;

namespace PropertyExchange.Presentation.API.Models.Tenant
{
    public class TenantLeaseDetailsModel
    {
        public int Id { get; set; }
        public required string TenantID { get; set; }
        public required string PropertyID { get; set; }
        public decimal LeaseAdvanceAmount { get; set; }
        public required string LeaseStartDate { get; set; }
        public required string LeaseEndDate { get; set; }
        public int LeaseTenureInMonths { get; set; }
        public decimal RentPerSft { get; set; }
        public decimal TotalAreaInSft { get; set; }
        public decimal EscalationInPercentage { get; set; }
        public int EscalationTenure { get; set; }
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public required TenantModel Tenant { get; set; }
    }
}
