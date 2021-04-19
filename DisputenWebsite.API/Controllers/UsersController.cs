using DisputenPWA.API.Security;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGeneratePassword _generatePassword;
        private readonly IConfiguration _configuration;

        public UsersController(
            IMediator mediator,
            IGeneratePassword generatePassword,
            IConfiguration configuration
            )
        {
            _mediator = mediator;
            _generatePassword = generatePassword;
            _configuration = configuration;
        }

        public class UserLoginValues
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string AppInstanceId { get; set; }
        }

        public class UserRegisterValues
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class SuccessObject
        {
            public bool Success { get; set; }
        }

        [HttpPost("register")]
        public async Task<SuccessObject> Register(UserRegisterValues values)
        {
            var result = await _mediator.Send(new RegisterUserCommand(values.Email, values.Password));
            return new SuccessObject { Success = !result.Errors.Any() };
        }

        public class TokenObject
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        [HttpPost("gettoken")]
        public async Task<TokenObject> GetToken(UserLoginValues values)
        {
            var result = await _mediator.Send(new JwtTokensQuery(values.Email, values.Password, null, values.AppInstanceId));
            return new TokenObject { AccessToken = result.Result.AccessToken, RefreshToken = result.Result.RefreshToken };
        }

        public class RefreshTokenValues
        {
            public string RefreshToken { get; set; }
            public string AppInstanceId { get; set; }
        }

        [HttpPost("refreshtoken")]
        public async Task<TokenObject> GetToken(RefreshTokenValues values)
        {
            var result = await _mediator.Send(new RefreshJwtTokensQuery(values.RefreshToken, values.AppInstanceId));
            return new TokenObject { AccessToken = result.Result.AccessToken, RefreshToken = result.Result.RefreshToken };
        }

        public class GoogleRegisterValues
        {
            public string Token { get; set; }
            public string AppInstanceId { get; set; }
        }

        [HttpPost("register/google")]
        public async Task<TokenObject> RegisterWithGoogle(GoogleRegisterValues values)
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

            var tokenResponse = await googleAuthCodeFlow.ExchangeCodeForTokenAsync("UserId", values.Token, "http://localhost:4200", CancellationToken.None);
            var credential = GoogleCredential.FromAccessToken(tokenResponse.AccessToken);

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DispuutWeb",
            });

            var myEvent = new Event
            {
                Summary = "Appointment",
                Start = new EventDateTime()
                {
                    DateTime = new DateTime(2021, 4, 20, 10, 0, 0),
                    TimeZone = "America/Los_Angeles"
                },
                End = new EventDateTime()
                {
                    DateTime = new DateTime(2021, 4, 20, 11, 0, 0),
                    TimeZone = "America/Los_Angeles"
                },
            };

            try
            {
                await service.Events.Insert(myEvent, "primary").ExecuteAsync();
            }
            catch(Exception e)
            {

            }

            // move whole thing to a command
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(values.Token);
            if (validPayload == null) return new TokenObject { AccessToken = null, RefreshToken = null };

            // check if user already exists

            // if user exists, skip this step
            var passWord = _generatePassword.GenerateAPassword();
            var registerResult = await _mediator.Send(new RegisterUserCommand(validPayload.Email, passWord));
            if (registerResult.Errors.Any()) return new TokenObject { AccessToken = null, RefreshToken = null };

            var tokenQueryResult = await _mediator.Send(new JwtTokensQuery(validPayload.Email, null, values.Token, values.AppInstanceId));
            return new TokenObject { AccessToken = tokenQueryResult.Result.AccessToken, RefreshToken = tokenQueryResult.Result.RefreshToken };
        }
        //[HttpPost("googlesingin")]
        //public async Task<TokenObject> getToken()
    }
}