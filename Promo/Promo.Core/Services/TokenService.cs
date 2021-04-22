using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Promo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Promo.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(string username)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = _config["AppSettings:Secret"];
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {

                return null;
            }
            
        }
    }
}
