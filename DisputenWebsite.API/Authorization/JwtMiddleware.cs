using DisputenPWA.Application.Constants;
using DisputenPWA.Application.Helpers;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.API.Authoriation
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(
            RequestDelegate next, 
            IConfiguration configuration
            )
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userManager, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, UserManager<ApplicationUser> userManager, string token)
        {
            try
            {
                Thread.Sleep(500);
                var validatedToken = SecurityTokenMaker.MakeSecurityToken(_configuration, token);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                context.Items["UserId"] = userId;

                var tokenType = jwtToken.Claims.First(x => x.Type == TokenTypes.ClaimType).Value;
                if (tokenType == TokenTypes.Refresh) return;

                var user = await userManager.FindByIdAsync(userId);
                // attach user to context on successful jwt validation
                context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
