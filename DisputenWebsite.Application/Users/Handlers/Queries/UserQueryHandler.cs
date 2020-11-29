using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Queries
{
    public class UserQueryHandler : IRequestHandler<UserQuery, UserQueryResult>
    {
        private readonly IUserService _userService;
        private readonly IUserConnector _userConnector;

        public UserQueryHandler(
            IUserService userService,
            IUserConnector userConnector
            )
        {
            _userService = userService;
            _userConnector = userConnector;
        }

        public async Task<UserQueryResult> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();
            var user = await _userConnector.GetUser(userId, request.UserPropertyHelper);
            return new UserQueryResult(user);
        }
    }
}
