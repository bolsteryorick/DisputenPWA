using DisputenPWA.Application.Services;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class RefreshJwtTokensQueryHandler : IRequestHandler<RefreshJwtTokensQuery, JwtTokensQueryResult>
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshJwtTokensQueryHandler(
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<JwtTokensQueryResult> Handle(RefreshJwtTokensQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();
            var tokenQuery = _refreshTokenRepository.GetQueryable().Where(r => r.AppInstanceId == request.AppInstanceId && r.UserId == userId);
            var tokenEntry = await tokenQuery.FirstOrDefaultAsync();
            if(tokenEntry?.RefreshTokenHash != TokenHasher.HashToken(request.RefreshToken))
            {
                return TokenResult(null, null);
            }

            var refreshToken = JwtTokenGenerator.GenerateJwtToken(userId, _configuration.GetValue<string>("JWT:Secret"), 60);
            var accessToken = JwtTokenGenerator.GenerateJwtToken(userId, _configuration.GetValue<string>("JWT:Secret"), 525600);

            var refreshTokenEntry = new DalRefreshToken
            {
                UserId = userId,
                RefreshTokenHash = TokenHasher.HashToken(refreshToken),
                AppInstanceId = request.AppInstanceId
            };
            
            var deleteQuery = _refreshTokenRepository.GetQueryable().Where(r => r.AppInstanceId == request.AppInstanceId && r.UserId == userId);
            await _refreshTokenRepository.DeleteByQuery(deleteQuery);
            await _refreshTokenRepository.Add(refreshTokenEntry);

            return TokenResult(refreshToken, accessToken);
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
