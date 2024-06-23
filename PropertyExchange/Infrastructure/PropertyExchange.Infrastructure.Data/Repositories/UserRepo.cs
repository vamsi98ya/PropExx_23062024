using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Infrastructure.Data.Common;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly PEDbContext _context;

        public UserRepo(PEDbContext context)
        {
            this._context = context;
        }

        //User Details
        async Task<bool> IUserRepo.CheckIfUserDetailsExists(string email, string phoneNumber)
        {
            var user = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ConfigureAwait(true);
            if (user != null)
            {
                if (!user.Name.IsNullOrEmpty() && !user.DOB.IsNullOrEmpty() && !user.Gender.IsNullOrEmpty())
                {
                    return true;
                }
            }
            return false;
        }

        async Task<UserModel> IUserRepo.GetUserDetailsAsync(string email, string phoneNumber)
        {
            var user = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ConfigureAwait(true);
            return user;
        }

        async Task<List<string>> IUserRepo.AddUserDetailsAsync(UserModel user)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            if (user.AvailableMoneyForInvesting.ToString().IsNullOrEmpty())
            {
                user.AvailableMoneyForInvesting = 0;
            }
            var count = this._context.tbl_pe_UserDetails.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            user.Id = count + 1;
            this._context.tbl_pe_UserDetails.Add(user);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.UserEmail, user.UserPhoneNumber };
        }


        async Task<List<string>> IUserRepo.UpdateUserDetailsAsync(UserModel user)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.LastUpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingUser = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber).ConfigureAwait(true);

            existingUser.Gender = user.Gender;
            existingUser.Occupation = user.Occupation;
            existingUser.IncomeRange = user.IncomeRange;
            existingUser.MaritalStatus = user.MaritalStatus;
            existingUser.SecondaryEmail = user.SecondaryEmail;
            existingUser.SecondaryPhoneNumber = user.SecondaryPhoneNumber;
            existingUser.LastUpdatedDate = user.LastUpdatedDate;

            if (existingUser.AvailableMoneyForInvesting.ToString().IsNullOrEmpty())
            {
                existingUser.AvailableMoneyForInvesting = 0;
            }

            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.UserEmail, user.UserPhoneNumber };
        }

        async Task<List<string>> IUserRepo.AddUserFundDetailsAsync(UserFundDetailsModel user)
        {
            //Add/Withdraw User Funds
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingFund = await this._context.tbl_pe_UserFundDetails.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber
            && x.TxnNumber == user.TxnNumber
            && x.AddOrWithdraw == user.AddOrWithdraw
            && x.TxnAmount == user.TxnAmount
            && x.Description == user.Description
            && x.ModeOfTxn == user.ModeOfTxn
            && x.TxnDateTime == user.TxnDateTime
            && x.BankName == user.BankName
            && x.AccountNumber == user.AccountNumber).ConfigureAwait(true);

            if (existingFund != null)
            {
                return new List<string> { "", "" };
            }

            var count = this._context.tbl_pe_UserFundDetails.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            user.Id = count + 1;
            this._context.tbl_pe_UserFundDetails.Add(user);
            await this._context.SaveChangesAsync().ConfigureAwait(true);


            //Update User Details with Available amount for investing info
            var existingUser = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber).ConfigureAwait(true);

            if (user.AddOrWithdraw.ToUpper() == "ADD")
            {
                existingUser.AvailableMoneyForInvesting = existingUser.AvailableMoneyForInvesting + user.TxnAmount;
            }
            else if (user.AddOrWithdraw.ToUpper() == "WITHDRAW" && user.TxnAmount > 0 && user.TxnAmount <= existingUser.AvailableMoneyForInvesting)
            {
                existingUser.AvailableMoneyForInvesting = existingUser.AvailableMoneyForInvesting - user.TxnAmount;
            }
            existingUser.LastUpdatedDate = user.CreatedDate;

            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.TxnAmount.ToString(), user.TxnDateTime };
        }

        async Task<bool> IUserRepo.CheckIfUserFundsHistoryExists(string email, string phoneNumber)
        {
            var userFunds = await this._context.tbl_pe_UserFundDetails.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ToListAsync();

            if (userFunds != null && userFunds.Count > 0)
            {
                return true;
            }
            return false;
        }
        async Task<List<UserFundDetailsModel>> IUserRepo.GetUserFundsHistoryAsync(string email, string phoneNumber)
        {
            var user = await this._context.tbl_pe_UserFundDetails.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ToListAsync();
            return user;
        }

        //USER PASSBOOK
        async Task<bool> IUserRepo.CheckIfPassbookHistoryExists(string email, string phoneNumber)
        {
            var userFunds = await this._context.tbl_pe_UserPassbook.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ToListAsync();

            if (userFunds != null && userFunds.Count > 0)
            {
                return true;
            }
            return false;
        }

        async Task<List<UserPassbookModel>> IUserRepo.GetUserPassbookAsync(string email, string phoneNumber)
        {
            var user = await this._context.tbl_pe_UserPassbook.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ToListAsync();
            return user;
        }

        async Task<List<string>> IUserRepo.AddUserOrderDetailsAsync(UserPassbookModel user)
        {
            //Add/Withdraw User Funds
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var lastUpdatedDt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingOrder = await this._context.tbl_pe_UserPassbook.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber
            && x.TxnNumber == user.TxnNumber
            && x.PropertyID == user.PropertyID
            && x.TxnNumber == user.TxnNumber
            && x.TxnType == user.TxnType
            && x.Quantity == user.Quantity
            && x.Price == user.Price
            && x.TotalTxnValue == user.TotalTxnValue
            && x.OrderDateTime == user.OrderDateTime).ConfigureAwait(true);

            if (existingOrder != null)
            {
                return new List<string> { "", "" };
            }

            //Update User Details with Available amount for investing info
            var existingUser = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber).ConfigureAwait(true);

            if (user.TxnType.ToUpper() == "BUY" && user.TotalTxnValue > 0)
            {
                existingUser.AvailableMoneyForInvesting = existingUser.AvailableMoneyForInvesting - user.TotalTxnValue;
            }
            else if (user.TxnType.ToUpper() == "SELL" && user.TotalTxnValue > 0)
            {
                existingUser.AvailableMoneyForInvesting = existingUser.AvailableMoneyForInvesting + user.TotalTxnValue;
            }
            existingUser.LastUpdatedDate = user.CreatedDate;

            await this._context.SaveChangesAsync().ConfigureAwait(true);

            //Update Passbook in case of a full quantity sell order
            var existingPassbookRecord = await this._context.tbl_pe_UserPassbook.Where(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber && x.PropertyID == user.PropertyID && x.IsCurrentHolding == true).ToListAsync();

            if (!user.IsCurrentHolding)
            {
                for(int i=0; i<existingPassbookRecord.Count; i++)
                {
                    existingPassbookRecord[i].IsCurrentHolding = false;
                }
            }
            await this._context.SaveChangesAsync().ConfigureAwait(true);

            //Add User passbook record with the available info
            var count = this._context.tbl_pe_UserPassbook.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            user.Id = count + 1;
            this._context.tbl_pe_UserPassbook.Add(user);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.TotalTxnValue.ToString(), user.OrderDateTime };
        }

        //User Holdings
        async Task<bool> IUserRepo.CheckIfUserHoldingExists(string email, string phoneNumber, string propertyID)
        {
            var user = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ConfigureAwait(true);

            var existingPassbookEntry = await this._context.tbl_pe_UserPassbook.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber
            && x.PropertyID == propertyID).ToListAsync();

            var existingHolding = await this._context.tbl_pe_UserPortfolio.SingleOrDefaultAsync(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber
            && x.PropertyID == propertyID && x.IsActive == true).ConfigureAwait(true);

            if (user != null)
            {
                if (!user.Name.IsNullOrEmpty() && !user.DOB.IsNullOrEmpty() && !user.Gender.IsNullOrEmpty())
                {
                    if (existingPassbookEntry.Count > 0 && existingHolding != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        async Task<bool> IUserRepo.CheckIfAnyUserHoldingExists(string email, string phoneNumber)
        {
            var user = await this._context.tbl_pe_UserDetails.SingleOrDefaultAsync(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber).ConfigureAwait(true);

            var existingPassbookEntry = await this._context.tbl_pe_UserPassbook.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber && x.IsCurrentHolding == true).ToListAsync();

            var existingHolding = await this._context.tbl_pe_UserPortfolio.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber
            && x.IsActive == true).ToListAsync();


            if (user != null)
            {
                if (!user.Name.IsNullOrEmpty() && !user.DOB.IsNullOrEmpty() && !user.Gender.IsNullOrEmpty())
                {
                    if (existingPassbookEntry.Count > 0 && existingHolding.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        async Task<List<string>> IUserRepo.AddUserHoldingAsync(UserHoldingModel user)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var property = await this._context.tbl_pe_Property.SingleOrDefaultAsync(x => x.PropertyID == user.PropertyID).ConfigureAwait(true);

            var count = this._context.tbl_pe_UserPortfolio.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            user.Id = count + 1;
            user.PropertyName = property.Name;
            user.IsActive = true;
            this._context.tbl_pe_UserPortfolio.Add(user);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.UserEmail, user.UserPhoneNumber };
        }

        async Task<List<string>> IUserRepo.UpdateUserHoldingAsync(UserHoldingModel user, string tradeType, string txnType)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.LastUpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();

            var property = await this._context.tbl_pe_Property.SingleOrDefaultAsync(x => x.PropertyID == user.PropertyID).ConfigureAwait(true);

            var existingHolding = await this._context.tbl_pe_UserPortfolio.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail
            && x.UserPhoneNumber == user.UserPhoneNumber && x.PropertyID == user.PropertyID
            && x.IsActive == true).ConfigureAwait(true);

            //if (tradeType == "GENERALTRADE") 
            //{
            //    existingHolding.Price = user.Price;
            //    existingHolding.LastUpdatedDate = user.LastUpdatedDate;
            //}

            if (tradeType == "USERTRADE" && txnType == "BUY")
            {
                if (property.IsActive == true && user.Quantity > 0 && user.Price > 0)
                {
                    //Averaging Price for Buy Order
                    var totalValue = (existingHolding.Quantity * existingHolding.Price) + (user.Quantity * user.Price);
                    var totalQuantity = existingHolding.Quantity + user.Quantity;

                    var averageTokenPrice = totalValue / totalQuantity;

                    existingHolding.Price = averageTokenPrice;
                    existingHolding.Quantity = existingHolding.Quantity + user.Quantity;
                    existingHolding.IsActive = true;
                    existingHolding.LastUpdatedDate = user.LastUpdatedDate;
                }
            }

            if (tradeType == "USERTRADE" && txnType == "SELL")
            {
                if (property.IsActive == true && user.Quantity > 0 && user.Price > 0)
                {
                    var totalValue = (existingHolding.Quantity * existingHolding.Price) - (user.Quantity * user.Price);
                    var totalQuantity = existingHolding.Quantity - user.Quantity;
                    if(totalQuantity == 0)
                    {
                        var averageTokenPrice = user.Price;
                        existingHolding.Price = averageTokenPrice;
                        existingHolding.Quantity = existingHolding.Quantity - user.Quantity;
                        existingHolding.IsActive = false;

                    }
                    else
                    {
                        var averageTokenPrice = totalValue / totalQuantity;
                        existingHolding.Price = averageTokenPrice;
                        existingHolding.Quantity = existingHolding.Quantity - user.Quantity;
                        existingHolding.IsActive = true;
                    }

                    if(existingHolding.PropertyName != property.Name){
                        existingHolding.PropertyName = property.Name;
                    }
                    existingHolding.LastUpdatedDate = user.LastUpdatedDate;
                }
            }

            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.UserEmail, user.UserPhoneNumber };
        }

        async Task<UserHoldingModel> IUserRepo.GetUserHolding(string email, string phoneNumber, string propertyID)
        {
            var existingHolding = await this._context.tbl_pe_UserPortfolio.SingleOrDefaultAsync(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber
            && x.PropertyID == propertyID && x.IsActive == true).ConfigureAwait(true);

            return existingHolding;
        }

        async Task<List<UserHoldingModel>> IUserRepo.GetAllUserHoldings(string email, string phoneNumber)
        {
            var existingHoldings = await this._context.tbl_pe_UserPortfolio.Where(x => x.UserEmail == email
            && x.UserPhoneNumber == phoneNumber
            && x.IsActive == true).ToListAsync();

            return existingHoldings;
        }
        
    }
}
