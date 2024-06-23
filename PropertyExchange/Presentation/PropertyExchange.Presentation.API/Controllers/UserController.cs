using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PropertyExchange.Core.Application.Contracts;
using PropertyExchange.Core.Application.Contracts.DTO;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using PropertyExchange.Presentation.API.Models.User;
using System.Diagnostics.CodeAnalysis;

namespace PropertyExchange.Presentation.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        private readonly IUserUseCase _userUseCase;
        private readonly IPropertyUseCase _propertyUseCase;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly string projectKey = "allUsers";

        public UserController(IUserUseCase userUseCase, IPropertyUseCase propertyUseCase, IMapper mapper, IOptions<AppSettings> appSettings, IMemoryCache cache)
        {
            this._userUseCase = userUseCase;
            _propertyUseCase = propertyUseCase;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;
            this._cache = cache;
        }

        //User Details
        [HttpGet("getuserdetails")]
        public async Task<IActionResult> Authenticate(string email, string phoneNumber)
        {
            if (!email.IsNullOrEmpty() && !email.IsNullOrEmpty())
            {
                var userDetailsExists = await this._userUseCase.CheckIfUserDetailsExists(email, phoneNumber);
                if (userDetailsExists) 
                {
                    var autUser = this._mapper.Map<UserVM>(await this._userUseCase.GetUserDetailsAsync(email, phoneNumber));
                    return this.Ok(autUser);
                }
            }
            return this.Ok(null);
        }

        [HttpPost("postuserdetails")]
        public async Task<IActionResult> PostUserDetails([FromBody] UserVM user)
        {
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            if (user.UserEmail.IsNullOrEmpty() || user.UserPhoneNumber.IsNullOrEmpty()
                || user.Name.IsNullOrEmpty() || user.DOB.IsNullOrEmpty()
                || user.Gender.IsNullOrEmpty() || user.Occupation.IsNullOrEmpty()
                || user.IncomeRange.IsNullOrEmpty() || user.MaritalStatus.IsNullOrEmpty()
                || user.FatherName.IsNullOrEmpty())
            {
                return this.BadRequest("User details are required");
            }

            var user1 = this._mapper.Map<UserModel>(user);
            var newUser = await this._userUseCase.AddUserDetailsAsync(user1).ConfigureAwait(false);
            if (newUser[0] == "" && newUser[1] == "")
            {
                return this.Ok("The user's details already exists");
            }
            return this.Created("Updated the user's details successfully", new { url = "Updated the user's details successfully" });
        }

        //User Fund Details
        [HttpPost("postuserfunddetails")]
        public async Task<IActionResult> PostUserFundDetails([FromBody] UserFundDetailsVM user)
        {
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            if (user.UserEmail.IsNullOrEmpty() || user.UserPhoneNumber.IsNullOrEmpty()
                || user.TxnNumber.IsNullOrEmpty() || user.AddOrWithdraw.IsNullOrEmpty()
                || user.TxnAmount.ToString().IsNullOrEmpty() || user.Description.IsNullOrEmpty()
                || user.ModeOfTxn.IsNullOrEmpty() || user.TxnDateTime.IsNullOrEmpty()
                || user.BankName.IsNullOrEmpty() ||user.AccountNumber.IsNullOrEmpty())
            {
                return this.BadRequest("User fund details are required");
            }

            var user1 = this._mapper.Map<UserFundDetailsModel>(user);
            var newUser = await this._userUseCase.AddUserFundDetailsAsync(user1).ConfigureAwait(false);
            if (newUser[0] == "" && newUser[1] == "")
            {
                return this.Ok("The user doesn't exist");
            }
            return this.Created("Updated the user's fund details succesfully", new { url = "Updated the user's fund details succesfully" });
        }

        //User Funds History
        [HttpGet("getuserfundshistory")]
        public async Task<IActionResult> GetUserFundsHistory(string email, string phoneNumber)
        {
            if (email.IsNullOrEmpty() || phoneNumber.IsNullOrEmpty())
            {
                return this.BadRequest("User details are required");
            }
            else
            {
                var userFundHistoryExists = await this._userUseCase.CheckIfUserFundsHistoryExists(email, phoneNumber);
                if (userFundHistoryExists)
                {
                    var autUser = this._mapper.Map< List<UserFundDetailsModel>>(await this._userUseCase.GetUserFundsHistoryAsync(email, phoneNumber));
                    autUser.Reverse();
                    return this.Ok(autUser);
                }
            }
            return this.Ok(null);
        }

        //User Passbook Details

        [HttpGet("getledgerhistory")]
        public async Task<IActionResult> GetLedgerHistory(string email, string phoneNumber)
        {
            if (email.IsNullOrEmpty() || phoneNumber.IsNullOrEmpty())
            {
                return this.BadRequest("User details are required");
            }
            else
            {
                var userFundHistoryExists = await this._userUseCase.CheckIfPassbookHistoryExists(email, phoneNumber);
                if (userFundHistoryExists)
                {
                    var autUser = this._mapper.Map<List<UserPassbookModel>>(await this._userUseCase.GetUserPassbookAsync(email, phoneNumber));
                    autUser.Reverse();
                    return this.Ok(autUser);
                }
            }
            return this.Ok(null);
        }
        [HttpPost("postuserorderdetails")]
        public async Task<IActionResult> PostUserOrderDetails([FromBody] UserPassbookVM user)
        {
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            if (user.UserEmail.IsNullOrEmpty() || user.UserPhoneNumber.IsNullOrEmpty()
                || user.PropertyID.IsNullOrEmpty() || user.TxnNumber.IsNullOrEmpty()
                || user.TxnType.ToString().IsNullOrEmpty() || user.Quantity.ToString().IsNullOrEmpty()
                || user.Price.ToString().IsNullOrEmpty() || user.TotalTxnValue.ToString().IsNullOrEmpty()
                || user.OrderDateTime.IsNullOrEmpty())
            {
                return this.BadRequest("User order details are required");
            }

            //Updating User Details and Adding a record in User Passbook
            var user1 = this._mapper.Map<UserPassbookModel>(user);
            var newUser = await this._userUseCase.AddUserOrderDetailsAsync(user1).ConfigureAwait(false);
            if (newUser[0] == "" && newUser[1] == "")
            {
                return this.Ok("The user doesn't exist");
            }

            //Updating Property Model and Adding a record in Property passbook
            PropertyPassbookVM property = new PropertyPassbookVM
            {
                PropertyID = user.PropertyID,
                TxnType = user.TxnType,
                TxnNumber = user.TxnNumber,
                Quantity = user.Quantity,
                Price = user.Price,
                TotalTxnValue = user.TotalTxnValue,
                OrderDateTime = user.OrderDateTime
            };
            var property1 = this._mapper.Map<PropertyPassbookModel>(property);
            var newProperty = await this._propertyUseCase.AddPropertyPassbookAsync(property1, user1).ConfigureAwait(false);
            if (newProperty[0] == "" && newProperty[1] == "")
            {
                return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
            }

            //Updating Property Model and Adding a record in Property Trade Records
            PropertyTradeRecordsVM trade = new PropertyTradeRecordsVM
            {
                PropertyID = user.PropertyID
            };
            var trade1 = this._mapper.Map<PropertyTradeRecordsModel>(trade);
            var newTrade = await this._propertyUseCase.AddPropertyTradeRecordAsync(trade1, "USERTRADE").ConfigureAwait(false);
            if (newTrade[0] == "" && newTrade[1] == "")
            {
                return this.Ok("Either the requested property does not exists or a similar transaction has already been logged in for the given property");
            }


            ////Adding or Updating User Portfolio
            UserHoldingVM holding = new UserHoldingVM 
            {
                PropertyID = user.PropertyID,
                UserEmail = user.UserEmail,
                UserPhoneNumber = user.UserPhoneNumber,
                Quantity = user.Quantity,
                Price = user.Price
            };
            var holding1 = this._mapper.Map<UserHoldingModel>(holding);
            if (user.TxnType.ToUpper() == "BUY")
            {
                var newHolding = await this._userUseCase.AddOrUpdateUserHoldingAsync(holding1, "USERTRADE", "BUY").ConfigureAwait(false);
                if (newHolding[0] == "" && newHolding[1] == "")
                {
                    return this.Ok("Either the requested user does not exists or a similar transaction has already been logged in for the given property");
                }
            }
            if(user.TxnType.ToUpper() == "SELL")
            {
                var newHolding = await this._userUseCase.AddOrUpdateUserHoldingAsync(holding1, "USERTRADE", "SELL").ConfigureAwait(false);
                if (newHolding[0] == "" && newHolding[1] == "")
                {
                    return this.Ok("Either the requested user does not exists or a similar transaction has already been logged in for the given property");
                }
            }
            return this.Created("Updated the user's order details successfully", new { url = "Updated the user's order details successfully" });
        }

        [HttpGet("getuserholding")]
        public async Task<IActionResult> GetUserHolding(string email, string phoneNumber, string propertyID)
        {
            if(!email.IsNullOrEmpty() || !phoneNumber.IsNullOrEmpty()
                || !propertyID.IsNullOrEmpty())
            {
                var autUser = this._mapper.Map<UserHoldingVM>(await this._userUseCase.GetUserHolding(email, phoneNumber, propertyID));
                return this.Ok(autUser);
            }

            return this.NotFound("User does not have any holdings for the requested property");
        }

        [HttpGet("getuserportfolio")]
        public async Task<IActionResult> GetAllUserHoldings(string email, string phoneNumber)
        {
            if (!email.IsNullOrEmpty() && !email.IsNullOrEmpty())
            {
                    var autUser = this._mapper.Map<List<UserHoldingVM>>(await this._userUseCase.GetAllUserHoldings(email, phoneNumber));
                    return this.Ok(autUser);
            }
            return this.NotFound("User Portfolio does not exist");
        }
    }
}
