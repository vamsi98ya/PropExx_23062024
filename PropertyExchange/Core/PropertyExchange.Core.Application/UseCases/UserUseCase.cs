using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.UseCases
{
    public class UserUseCase:IUserUseCase
    {
        private readonly IUserRepo _userRepository;

        public UserUseCase(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }

        //User Details
        async Task<bool> IUserUseCase.CheckIfUserDetailsExists(string email, string phoneNumber)
        {
            var existingUser = await _userRepository.CheckIfUserDetailsExists(email, phoneNumber);
            return existingUser;
        }

        async Task<UserModel> IUserUseCase.GetUserDetailsAsync(string email, string phoneNumber)
        {
            var existingUser = await _userRepository.GetUserDetailsAsync(email, phoneNumber);
            if (existingUser != null)
            {
                return existingUser;
            }
            throw new KeyNotFoundException();
        }

        async Task<List<string>> IUserUseCase.AddUserDetailsAsync(UserModel user)
        {
            var existingUser = await _userRepository.CheckIfUserDetailsExists(user.UserEmail, user.UserPhoneNumber);
            if (existingUser)
            {
                return await _userRepository.UpdateUserDetailsAsync(user);
            }
            else
            {
                return await _userRepository.AddUserDetailsAsync(user);
            }
        }

        //User Funds
        async Task<List<string>> IUserUseCase.AddUserFundDetailsAsync(UserFundDetailsModel user)
        {
            var existingUser = await _userRepository.CheckIfUserDetailsExists(user.UserEmail, user.UserPhoneNumber);
            if (existingUser)
            {
                return await _userRepository.AddUserFundDetailsAsync(user); 
            }
            else
            {
                return new List<string> { "", "" };
            }
        }

        async Task<bool> IUserUseCase.CheckIfUserFundsHistoryExists(string email, string phoneNumber)
        {
            var existingUser = await _userRepository.CheckIfUserFundsHistoryExists(email, phoneNumber);
            return existingUser;
        }

        async Task<List<UserFundDetailsModel>> IUserUseCase.GetUserFundsHistoryAsync(string email, string phoneNumber)
        {
            var existingUser = await _userRepository.GetUserFundsHistoryAsync(email, phoneNumber);
            if (existingUser != null)
            {
                return existingUser;
            }
            throw new KeyNotFoundException();
        }

        //User Passbook
        async Task<bool> IUserUseCase.CheckIfPassbookHistoryExists(string email, string phoneNumber)
        {
            var existingUser = await _userRepository.CheckIfPassbookHistoryExists(email, phoneNumber);
            return existingUser;
        }

        async Task<List<UserPassbookModel>> IUserUseCase.GetUserPassbookAsync(string email, string phoneNumber)
        {
            var existingUser = await _userRepository.GetUserPassbookAsync(email, phoneNumber);
            if (existingUser != null)
            {
                return existingUser;
            }
            throw new KeyNotFoundException();
        }

        async Task<List<string>> IUserUseCase.AddUserOrderDetailsAsync(UserPassbookModel user)
        {
            var existingUser = await _userRepository.CheckIfUserDetailsExists(user.UserEmail, user.UserPhoneNumber);
            if (existingUser)
            {
                return await _userRepository.AddUserOrderDetailsAsync(user);
            }
            else
            {
                return new List<string> { "", "" };
            }
        }

        //User Portfolio
        async Task<List<string>> IUserUseCase.AddOrUpdateUserHoldingAsync(UserHoldingModel user, string tradeType, string txnType)
        {
            var existingHolding = await _userRepository.CheckIfUserHoldingExists(user.UserEmail, user.UserPhoneNumber, user.PropertyID);
            if (existingHolding)
            {
                return await _userRepository.UpdateUserHoldingAsync(user, tradeType, txnType);
            }
            else
            {
                return await _userRepository.AddUserHoldingAsync(user);
            }
        }

        async Task<UserHoldingModel> IUserUseCase.GetUserHolding(string email, string phoneNumber, string propertyID)
        {
            var isExistingHolding = await _userRepository.CheckIfUserHoldingExists(email, phoneNumber, propertyID);
            if (isExistingHolding)
            {
                var existingHolding = await _userRepository.GetUserHolding(email, phoneNumber, propertyID);
                return existingHolding;
            }
            return null;
        }

        async Task<List<UserHoldingModel>> IUserUseCase.GetAllUserHoldings(string email, string phoneNumber)
        {
            var isExistingHoldings = await _userRepository.CheckIfAnyUserHoldingExists(email, phoneNumber);
            if (isExistingHoldings)
            {
                var existingHoldings = await _userRepository.GetAllUserHoldings(email, phoneNumber);
                return existingHoldings;
            }
            return null;
        }
    }
}
