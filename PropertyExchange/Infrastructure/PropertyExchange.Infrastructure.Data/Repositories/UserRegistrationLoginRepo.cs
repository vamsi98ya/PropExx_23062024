using Microsoft.EntityFrameworkCore;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Infrastructure.Data.Repositories
{
    public class UserRegistrationLoginRepo:IUserRegistrationLoginRepo
    {
        private readonly PEDbContext _context;

        public UserRegistrationLoginRepo(PEDbContext context)
        {
            this._context = context;
        }
        async Task<UserRegistrationLoginModel> IUserRegistrationLoginRepo.Authenticate(string email, string phoneNumber, string AuthType)
        {
            var user = await this._context.tbl_pe_UserRegistration.SingleOrDefaultAsync(x => (x.UserEmail == email
                                                                                          || x.UserPhoneNumber == phoneNumber)
                                                                                          && x.Role == "USER").ConfigureAwait(true);
            return user;
            //throw new NotImplementedException();
        }

        async Task<List<string>> IUserRegistrationLoginRepo.LoginUserAsync(string email, string phoneNumber)
        {
            UserLoginModel user = new UserLoginModel();
            user.UserEmail = email;
            user.UserPhoneNumber = phoneNumber;
            user.Role = "USER";
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.LoginDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var count = this._context.tbl_pe_UserLogin.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            user.Id = count + 1;
            this._context.tbl_pe_UserLogin.Add(user);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.UserEmail, user.UserPhoneNumber };
        }

        async Task<List<string>> IUserRegistrationLoginRepo.AddUserAsync(UserRegistrationLoginModel user)
        {
            user.Role = "USER";
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            user.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var exstingEmployee = await this._context.tbl_pe_UserRegistration.SingleOrDefaultAsync(e => e.UserEmail == user.UserEmail 
                                                                                                     && e.UserPhoneNumber == user.UserPhoneNumber
                                                                                                     && e.Role == "USER").ConfigureAwait(true);
            if (exstingEmployee != null)
            {
                return new List<string> { "", "" }; 
            }

            var count = this._context.tbl_pe_UserRegistration.OrderByDescending(item => item.Id)
                        .Select(item => item.Id)
                        .FirstOrDefault();
            user.Id = count + 1;
            this._context.tbl_pe_UserRegistration.Add(user);
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string>{ user.UserEmail, user.UserPhoneNumber};
        }

        async Task<List<string>> IUserRegistrationLoginRepo.ChangeUserPasswordAsync(UserChangePasswordModel user)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var lastupdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE).ToString();
            var existingUser = await this._context.tbl_pe_UserRegistration.SingleOrDefaultAsync(e => e.UserEmail == user.UserEmail
                                                                                                     && e.UserPhoneNumber == user.UserPhoneNumber
                                                                                                     && e.Role == "USER").ConfigureAwait(true);
            if (existingUser != null)
            {
                existingUser.UserPassword = user.NewPassword;
                existingUser.LastUpdatedDate = lastupdatedDate;
            }
            
            await this._context.SaveChangesAsync().ConfigureAwait(true);
            return new List<string> { user.UserEmail, user.UserPhoneNumber };
        }
    }
}
