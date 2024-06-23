using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Domain.Models.User
{
    public class UserRegistrationLoginModel
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string UserPassword { get; set; }
        public string? Role { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public string? Salt { get; set; }
        public UserModel? UserDetails { get; set; }


        public static string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
        public static string CreatePasswordHash(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);
            var result = Convert.ToBase64String(valueBytes);
            return result;
        }

        public static bool ValidatePasswordHash(string value, string salt, string hash)
            => CreatePasswordHash(value, salt) == hash;
        public static string Authentication(string email, string phoneNumber, string key1)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(key1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                          new Claim(ClaimTypes.Email, email.ToString()),
                          new Claim(ClaimTypes.MobilePhone, phoneNumber.ToString()),
                          new Claim(ClaimTypes.Role, "Admin"),
                          new Claim(ClaimTypes.Version, "V3.0")
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class UserLoginModel
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? LoginDateTime { get; set; }
    }

    public class UserChangePasswordModel
    {
        public required string UserEmail { get; set; }
        public required string UserPhoneNumber { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
