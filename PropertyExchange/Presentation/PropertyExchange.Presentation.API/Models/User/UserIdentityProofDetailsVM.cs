namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserIdentityProofDetailsVM
    {
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string ProofName { get; set; }
        public required string ProofNumber { get; set; }
        public string? ExpiryDate { get; set; }
    }
}
