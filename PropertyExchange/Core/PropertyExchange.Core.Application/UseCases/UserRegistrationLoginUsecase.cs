using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Contracts.DTO;
using PropertyExchange.Core.Application.Repositories;
using PropertyExchange.Core.Domain.Exceptions;
using PropertyExchange.Core.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Application.UseCases
{
    public class UserRegistrationLoginUseCase:IUserRegistrationLoginUseCase
    {
        private readonly IUserRegistrationLoginRepo _userRepository;

        public UserRegistrationLoginUseCase(IUserRegistrationLoginRepo userRepository)
        {
            _userRepository = userRepository;
        }
        async Task<UserRegistrationLoginModel> IUserRegistrationLoginUseCase.Authenticate(UserLoginDTO user, string key)
        {
            var existingUser = await _userRepository.Authenticate(user.Email, user.PhoneNumber, "Login");
            if (existingUser != null)
            {
                user.Password = UserRegistrationLoginModel.CreatePasswordHash(user.Password, existingUser.Salt);
                if (user.Password == existingUser.UserPassword)
                {
                    //existingUser.UserPassword = UserRegistrationLoginModel.Authentication(user.Email, user.PhoneNumber, key);
                    await _userRepository.LoginUserAsync(user.Email, user.PhoneNumber);
                    return existingUser;
                }
            }
            throw new KeyNotFoundException();
        }

        async Task<List<string>> IUserRegistrationLoginUseCase.AddUserAsync(UserRegistrationLoginModel user)
        {
            var existingUser = await _userRepository.Authenticate(user.UserEmail, user.UserPhoneNumber, "Register");
            if (existingUser == null)
            {
                //var allProjects = await _userRepository.GetAllProjectsAsync();
                //employee.SetDefultData(allProjects);
                user.Salt = UserRegistrationLoginModel.CreateSalt();
                user.UserPassword = UserRegistrationLoginModel.CreatePasswordHash(user.UserPassword, user.Salt);
                
                return await _userRepository.AddUserAsync(user);
            }
            else
            {
                return new List<string> { "", "" };
            }

        }

        async Task<List<string>> IUserRegistrationLoginUseCase.ChangeUserPasswordAsync(UserChangePasswordModel user)
        {
            var existingUser = await _userRepository.Authenticate(user.UserEmail, user.UserPhoneNumber, "Login");
            if (existingUser != null)
            {
                user.OldPassword = UserRegistrationLoginModel.CreatePasswordHash(user.OldPassword, existingUser.Salt);

                if (existingUser.UserPassword == user.OldPassword)
                {
                    user.NewPassword = UserRegistrationLoginModel.CreatePasswordHash(user.NewPassword, existingUser.Salt);
                    return await _userRepository.ChangeUserPasswordAsync(user);
                }
                else
                {
                    return new List<string> { "OldPassword", "NewPassword" };
                }
            }
            else
            {
                return new List<string> { "", "" };
            }

        }
    }
}
