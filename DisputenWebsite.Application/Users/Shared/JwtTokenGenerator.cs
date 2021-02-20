using DisputenPWA.Application.Constants;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DisputenPWA.Application.Users.Shared
{
    public static class JwtTokenGenerator
    {
        public static string GenerateAccessJwtToken(string userId, IConfiguration configuration)
        {
            return GenerateJwtToken(userId, configuration, 60, TokenTypes.Access);
        }

        public static string GenerateRefeshJwtToken(string userId, IConfiguration configuration)
        {
            return GenerateJwtToken(userId, configuration, 525600, TokenTypes.Refresh);
        }

        private static string GetSecret(IConfiguration configuration) => configuration.GetValue<string>("JWT:Secret");

        private static string GenerateJwtToken(string userId, IConfiguration configuration, int validForMinutes, string tokenType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = GetSecret(configuration);
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId), new Claim(TokenTypes.ClaimType, tokenType) }),
                Expires = DateTime.UtcNow.AddMinutes(validForMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}