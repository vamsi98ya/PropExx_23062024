using PropertyExchange.Presentation.API.Models.Property;

namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserFundDetailsVM
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string TxnNumber { get; set; }
        public required string AddOrWithdraw { get; set; }
        public decimal TxnAmount { get; set; }
        public string? Description { get; set; }
        public required string ModeOfTxn { get; set; }
        public required string TxnDateTime { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public UserVM? UserDetails { get; set; }
    }
}
