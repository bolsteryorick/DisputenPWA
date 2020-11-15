using DisputenPWA.Application.Users.Shared;
using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Domain.UserAggregate.Queries;
using DisputenPWA.Domain.UserAggregate.Queries.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class GetJwtTokenHandler : IRequestHandler<GetJwtTokenQuery, GetJwtTokenQueryResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public GetJwtTokenHandler(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<GetJwtTokenQueryResult> Handle(GetJwtTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if(user == null || await PasswordInCorrect(user, request))
                return TokenResult(null);

            var token = JwtTokenGenerator.GenerateJwtToken(user, _configuration.GetValue<string>("JWT:Secret"));
            return TokenResult(token);
        }

        private async Task<bool> PasswordInCorrect(ApplicationUser user, GetJwtTokenQuery request)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            return !signInResult.Succeeded;
        }

        private GetJwtTokenQueryResult TokenResult(string jwtToken)
        {
            return new GetJwtTokenQueryResult(new User { JWTToken = jwtToken });
        }
    }
}
