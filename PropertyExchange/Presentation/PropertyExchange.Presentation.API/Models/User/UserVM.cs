using PropertyExchange.Presentation.API.Models.Property;

namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserVM
    {
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string Name { get; set; }
        public required string DOB { get; set; }
        public required string Gender { get; set; }
        public string? SecondaryEmail { get; set; }
        public string? SecondaryPhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public string? IncomeRange { get; set; }
        public string? MaritalStatus { get; set; }
        public string? FatherName { get; set; }
        public decimal? AvailableMoneyForInvesting { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public UserRegistrationLoginVM? User { get; set; }
        public List<UserFundDetailsVM>? UserFundDetails { get; set; }
        public List<UserPassbookVM>? UserPassbook { get; set; }
        public List<UserHoldingVM>? UserPortfolio { get; set; }
    }
}
