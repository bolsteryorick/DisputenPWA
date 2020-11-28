using DisputenPWA.Domain.Aggregates.UserAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserValues values)
        {
            var result = await _mediator.Send(new RegisterUserCommand(values.Email, values.Password));
            return new OkObjectResult(result.Result.JWTToken);
        }

        [HttpPost("gettoken")]
        public async Task<IActionResult> GetToken(UserValues values)
        {
            var result = await _mediator.Send(new JwtTokenQuery(values.Email, values.Password));
            return new OkObjectResult(result.Result.JWTToken);
        }
    }
}