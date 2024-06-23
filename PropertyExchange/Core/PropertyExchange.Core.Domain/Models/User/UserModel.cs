using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Domain.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
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
        public UserRegistrationLoginModel? User { get; set; }
        public List<UserFundDetailsModel>? UserFundDetails { get; set; }
        public List<UserPassbookModel>? UserPassbook { get; set; }
        public List<UserHoldingModel>? UserPortfolio { get; set; }

    }
}
