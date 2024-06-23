using PropertyExchange.Core.Application.Contracts.DTO;
using PropertyExchange.Core.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.Contracts
{
    public interface IUserRegistrationLoginUseCase
    {
        Task<UserRegistrationLoginModel> Authenticate(UserLoginDTO user, string key);
        Task<List<string>> AddUserAsync(UserRegistrationLoginModel employee);
        Task<List<string>> ChangeUserPasswordAsync(UserChangePasswordModel employee);
    }
}
