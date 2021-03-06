using DisputenPWA.Application.Helpers;
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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class RefreshJwtTokensQueryHandler : IRequestHandler<RefreshJwtTokensQuery, JwtTokensQueryResult>
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshJwtTokensQueryHandler(
            IConfiguration configuration,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<JwtTokensQueryResult> Handle(RefreshJwtTokensQuery request, CancellationToken cancellationToken)
        {
            var validatedToken = SecurityTokenMaker.MakeSecurityToken(_configuration, request.RefreshToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            var tokenQuery = _refreshTokenRepository.GetQueryable().Where(r => r.AppInstanceId == request.AppInstanceId && r.UserId == userId);
            var tokenEntry = await tokenQuery.FirstOrDefaultAsync();
            if(tokenEntry?.RefreshTokenHash != TokenHasher.HashToken(request.RefreshToken, tokenEntry.RefreshTokenSalt).TokenHash)
            {
                return TokenResult(null, null);
            }

            var refreshToken = JwtTokenGenerator.GenerateRefeshJwtToken(userId, _configuration);
            var accessToken = JwtTokenGenerator.GenerateAccessJwtToken(userId, _configuration);

            var tokenHashResult = TokenHasher.HashToken(refreshToken);
            var refreshTokenEntry = new DalRefreshToken
            {
                UserId = userId,
                RefreshTokenSalt = tokenHashResult.Salt,
                RefreshTokenHash = tokenHashResult.TokenHash,
                AppInstanceId = request.AppInstanceId
            };
            
            var deleteQuery = _refreshTokenRepository.GetQueryable().Where(r => r.AppInstanceId == request.AppInstanceId && r.UserId == userId);
            await _refreshTokenRepository.DeleteByQuery(deleteQuery);
            await _refreshTokenRepository.Add(refreshTokenEntry);

            return TokenResult(accessToken: accessToken, refreshToken: refreshToken);
        }

        private JwtTokensQueryResult TokenResult(string accessToken, string refreshToken)
        {
            return new JwtTokensQueryResult(new User { AccessToken = accessToken, RefreshToken = refreshToken });
        }
    }
}
