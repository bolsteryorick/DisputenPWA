using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DisputenPWA.Application.Users.Shared;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.GoogleAccessInfoAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DisputenPWA.Application.Users.Handlers.Commands
{
    public class LoginWithGoogleHandler : IRequestHandler<LoginWithGoogleCommand, RegisterUserCommandResult>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IGeneratePassword _generatePassword;
        private readonly IGoogleAccessInfoRepository _googleAccessInfoRepository;

        public LoginWithGoogleHandler(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IMediator mediator,
            IGeneratePassword generatePassword,
            IGoogleAccessInfoRepository googleAccessInfoRepository
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _mediator = mediator;
            _generatePassword = generatePassword;
            _googleAccessInfoRepository = googleAccessInfoRepository;
        }

        public async Task<RegisterUserCommandResult> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var tokenResponse = await GetTokenResponse(request.Token);

            // make sure user exists, if not make it
            var email = await GetEmail(tokenResponse.IdToken);
            var user = await _userManager.FindByNameAsync(email);
            if(user == null)
            {
                var passWord = _generatePassword.GenerateAPassword();
                var registerResult = await _mediator.Send(new RegisterUserCommand(email, passWord));
                if (registerResult.Errors.Any()) return new RegisterUserCommandResult(null);
                user = await _userManager.FindByNameAsync(email);
            }

            // save google access info
            var googleAccessInfo = new GoogleAccessInfo
            {
                UserId = user.Id,
                RefreshToken = tokenResponse.RefreshToken,
                Token = tokenResponse.AccessToken
            };
            var queryable = _googleAccessInfoRepository.GetQueryable();
            await _googleAccessInfoRepository.DeleteByQuery(queryable.Where(g => g.UserId == user.Id));
            await _googleAccessInfoRepository.Add(googleAccessInfo);

            var jwttokens = await _mediator.Send(new JwtTokensQuery(email, null, tokenResponse.IdToken, request.AppInstanceId));
            return new RegisterUserCommandResult(jwttokens.Result);
        }

        public async Task<string> GetEmail(string idToken)
        {
            var validPayloadTest = await GoogleJsonWebSignature.ValidateAsync(idToken);
            return validPayloadTest.Email;
        }

        public Task<TokenResponse> GetTokenResponse(string token)
        {
            var clientId = _configuration.GetValue<string>("Authentication:Google:ClientId");
            var clientSecret = _configuration.GetValue<string>("Authentication:Google:ClientSecret");

            var googleAuthCodeFlow = new GoogleAuthorizationCodeFlow(
                new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    Scopes = new[] { CalendarService.Scope.CalendarEvents, CalendarService.Scope.Calendar }
                });

            return googleAuthCodeFlow.ExchangeCodeForTokenAsync("UserId", token, "http://localhost:4200", CancellationToken.None);
        }
    }
}
