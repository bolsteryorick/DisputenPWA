using DisputenPWA.Domain.Aggregates.UserAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(
            IMediator mediator
            )
        {
            _mediator = mediator;
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
            var result = await _mediator.Send(new JwtTokensQuery(values.Email, values.Password, values.AppInstanceId));
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
    }
}