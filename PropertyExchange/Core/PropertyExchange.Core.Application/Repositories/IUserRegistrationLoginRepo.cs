using PropertyExchange.Core.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Repositories
{
    public interface IUserRegistrationLoginRepo
    {
        Task<UserRegistrationLoginModel> Authenticate(string email, string phoneNumber, string AuthType);

        Task<List<string>> LoginUserAsync(string email, string phoneNumber);

        Task<List<string>> AddUserAsync(UserRegistrationLoginModel user);
        Task<List<string>> ChangeUserPasswordAsync(UserChangePasswordModel user);

    }
}
