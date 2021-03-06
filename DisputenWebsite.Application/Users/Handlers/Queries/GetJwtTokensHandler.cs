using DisputenPWA.Application.Users.Handlers.Queries.Helpers;
using DisputenPWA.Application.Users.Shared;
using DisputenPWA.DAL.Models;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class GetJwtTokensHandler : IRequestHandler<JwtTokensQuery, JwtTokensQueryResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public GetJwtTokensHandler(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<JwtTokensQueryResult> Handle(JwtTokensQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null || await PasswordInCorrect(user, request))
                return TokenResult(null, null) ;

            var refreshToken = JwtTokenGenerator.GenerateRefeshJwtToken(user.Id, _configuration);
            var accessToken = JwtTokenGenerator.GenerateAccessJwtToken(user.Id, _configuration);

            var tokenHashResult = TokenHasher.HashToken(refreshToken);
            var refreshTokenEntry = new DalRefreshToken
            {
                UserId = user.Id,
                RefreshTokenSalt = tokenHashResult.Salt,
                RefreshTokenHash = tokenHashResult.TokenHash,
                AppInstanceId = request.AppInstanceId
            };
            
            var deleteQuery = _refreshTokenRepository.GetQueryable().Where(r => r.AppInstanceId == request.AppInstanceId && r.UserId == user.Id);
            await _refreshTokenRepository.DeleteByQuery(deleteQuery);
            await _refreshTokenRepository.Add(refreshTokenEntry);

            return TokenResult(accessToken: accessToken, refreshToken: refreshToken);
        }

        private async Task<bool> PasswordInCorrect(ApplicationUser user, JwtTokensQuery request)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            return !signInResult.Succeeded;
        }

        private JwtTokensQueryResult TokenResult(string accessToken, string refreshToken)
        {
            return new JwtTokensQueryResult(new User { AccessToken = accessToken, RefreshToken = refreshToken });
        }
    }
}
