namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserBankAccountDetailsVM
    {
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string BankName { get; set; }
        public required string BankAddress { get; set; }
        public required int AccountNumber { get; set; }
        public required string AccountType { get; set; }
        public required string IFSCCode { get; set; }
        public required string MICRCode { get; set; }
        public required bool IsNRIAccount { get; set; }
    }
}
