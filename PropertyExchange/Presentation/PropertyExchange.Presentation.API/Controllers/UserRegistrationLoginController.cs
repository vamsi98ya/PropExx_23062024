using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Automapper;
using PropertyExchange.Core.Application.Contracts.DTO;
using PropertyExchange.Presentation.API.Models;
using AutoMapper;
using PropertyExchange.Presentation.API.Models.User;
using Microsoft.IdentityModel.Tokens;
using PropertyExchange.Core.Domain.Models.User;
using System;

namespace PropertyExchange.Presentation.API.Controllers
{
    [Route("api/userregistrationlogin")]
    [ApiController]
    public class UserRegistrationLoginController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        private readonly IUserRegistrationLoginUseCase _userUsecase;
        private readonly IMapper _mapper;


        public UserRegistrationLoginController(IUserRegistrationLoginUseCase userUsecase, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            this._userUsecase = userUsecase;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserLoginDTO user)
        {
            if (user == null)
            {
                return this.BadRequest("Invalid credentials");
            }

            var autUser = this._mapper.Map<UserRegistrationLoginVM>(await this._userUsecase.Authenticate(user, this._appSettings.Key));
            return this.Ok(autUser);
        }

        [HttpPost("postuser")]
        public async Task<IActionResult> PostUser([FromBody]UserRegistrationLoginVM user)
        {
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            if(user.UserEmail.IsNullOrEmpty() || user.UserPhoneNumber.IsNullOrEmpty() || user.UserPassword.IsNullOrEmpty())
            {
                return this.BadRequest("User Email, Phone Number and Password are required");
            }

            var user1 = this._mapper.Map<UserRegistrationLoginModel>(user);
            var newUser = await this._userUsecase.AddUserAsync(user1).ConfigureAwait(false);
            if (newUser[0] == "" && newUser[1] == "")
            {
                return this.Ok("A user already exists with the given email or phone number");
            }
            return this.Created("created a new user", new { url = "created a new user with email : " + newUser[0] + " and phone number : " + newUser[1] });
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordVM user)
        {
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            if (user.UserEmail.IsNullOrEmpty() || user.UserPhoneNumber.IsNullOrEmpty() 
                || user.OldPassword.IsNullOrEmpty() || user.NewPassword.IsNullOrEmpty())
            {
                return this.BadRequest("User Email, Phone Number and Password are required");
            }

            var user1 = this._mapper.Map<UserChangePasswordModel>(user);
            var newUser = await this._userUsecase.ChangeUserPasswordAsync(user1).ConfigureAwait(false);
            if (newUser[0] == "OldPassword" && newUser[1] == "NewPassword")
            {
                return this.Ok("Invalid old password");
            }
            if (newUser[0] == "" && newUser[1] == "")
            {
                return this.Ok("User does not exist");
            }
            return this.Created("Password changed successfully", new { url = "Password changed successfully" });
        }
    }
}
