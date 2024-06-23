using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Contracts
{
    public interface IUserUseCase
    {
        //User Details
        Task<bool> CheckIfUserDetailsExists(string userEmail, string userPhoneNumber);
        Task<UserModel> GetUserDetailsAsync(string userEmail, string userPhoneNumber);
        Task<List<string>> AddUserDetailsAsync(UserModel user);

        //User Funds
        Task<List<string>> AddUserFundDetailsAsync(UserFundDetailsModel user);
        Task<bool> CheckIfUserFundsHistoryExists(string userEmail, string userPhoneNumber);
        Task<List<UserFundDetailsModel>> GetUserFundsHistoryAsync(string userEmail, string userPhoneNumber);

        //User Passbook
        Task<bool> CheckIfPassbookHistoryExists(string userEmail, string userPhoneNumber);
        Task<List<UserPassbookModel>> GetUserPassbookAsync(string userEmail, string userPhoneNumber);
        Task<List<string>> AddUserOrderDetailsAsync(UserPassbookModel user);

        //User Portfolio
        Task<List<string>> AddOrUpdateUserHoldingAsync(UserHoldingModel holding, string tradeType, string txnType);
        Task<UserHoldingModel> GetUserHolding(string email, string phoneNumber, string propertyID);
        Task<List<UserHoldingModel>> GetAllUserHoldings(string email, string phoneNumber);
    }
}
