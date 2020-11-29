using DisputenPWA.Application.Users.Shared;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class GetJwtTokenHandler : IRequestHandler<JwtTokenQuery, JwtTokenQueryResult>
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

        public async Task<JwtTokenQueryResult> Handle(JwtTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if(user == null || await PasswordInCorrect(user, request))
                return TokenResult(null);

            var token = JwtTokenGenerator.GenerateJwtToken(user, _configuration.GetValue<string>("JWT:Secret"));
            return TokenResult(token);
        }

        private async Task<bool> PasswordInCorrect(ApplicationUser user, JwtTokenQuery request)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            return !signInResult.Succeeded;
        }

        private JwtTokenQueryResult TokenResult(string jwtToken)
        {
            return new JwtTokenQueryResult(new User { JWTToken = jwtToken });
        }
    }
}
