using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Repositories
{
    public interface IUserRepo
    {
        //User Details
        Task<bool> CheckIfUserDetailsExists(string email, string phoneNumber);
        Task<UserModel> GetUserDetailsAsync(string email, string phoneNumber);
        Task<List<string>> AddUserDetailsAsync(UserModel user);
        Task<List<string>> UpdateUserDetailsAsync(UserModel user);

        //User Funds
        Task<List<string>> AddUserFundDetailsAsync(UserFundDetailsModel user);
        Task<bool> CheckIfUserFundsHistoryExists(string email, string phoneNumber);
        Task<List<UserFundDetailsModel>> GetUserFundsHistoryAsync(string email, string phoneNumber);

        //User Passbook
        Task<bool> CheckIfPassbookHistoryExists(string userEmail, string userPhoneNumber);
        Task<List<UserPassbookModel>> GetUserPassbookAsync(string userEmail, string userPhoneNumber);
        Task<List<string>> AddUserOrderDetailsAsync(UserPassbookModel user);

        //User Portfolio
        Task<bool> CheckIfUserHoldingExists(string email, string phoneNumber, string propertyID);
        Task<bool> CheckIfAnyUserHoldingExists(string email, string phoneNumber); 
        Task<UserHoldingModel> GetUserHolding(string email, string phoneNumber, string propertyID); 
        Task<List<UserHoldingModel>> GetAllUserHoldings(string email, string phoneNumber); 
        Task<List<string>> AddUserHoldingAsync(UserHoldingModel holding);
        Task<List<string>> UpdateUserHoldingAsync(UserHoldingModel holding, string tradeType, string txnType);

    }
}
