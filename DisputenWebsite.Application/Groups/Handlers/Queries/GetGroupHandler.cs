using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Queries;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Queries
{
    public class GetGroupHandler : IRequestHandler<GroupQuery, GroupQueryResult>
    {
        private readonly IGroupConnector _groupConnector;
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IUserService _userService;

        public GetGroupHandler(
            IGroupConnector groupConnector,
            IOperationAuthorizer operationAuthorizer,
            IUserService userService
            )
        {
            _groupConnector = groupConnector;
            _operationAuthorizer = operationAuthorizer;
            _userService = userService;
        }

        public async Task<GroupQueryResult> Handle(GroupQuery request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanQueryGroup(request.GroupId))
            {
                return new GroupQueryResult(null);
            }
            var group = await _groupConnector.GetGroup(request.GroupId, request.GroupPropertyHelper);
            return new GroupQueryResult(group);
        }
    }
}