using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyExchange.Core.Application.Contracts.DTO
{
    public class UserLoginDTO
    {
        public  string? Email { get; set; }
        public  string? PhoneNumber { get; set; }
        public required string Password { get; set; }
    }
}
