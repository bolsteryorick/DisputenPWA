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

        public class UserValues
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class SuccessObject
        {
            public bool Success { get; set; }
        }

        [HttpPost("register")]
        public async Task<SuccessObject> Register(UserValues values)
        {
            var result = await _mediator.Send(new RegisterUserCommand(values.Email, values.Password));
            return new SuccessObject { Success = !result.Errors.Any() };
        }

        public class TokenObject
        {
            public string Token { get; set; }
        }

        [HttpPost("gettoken")]
        public async Task<TokenObject> GetToken(UserValues values)
        {
            var result = await _mediator.Send(new JwtTokenQuery(values.Email, values.Password));
            return new TokenObject { Token = result.Result.JWTToken };
        }
    }
}