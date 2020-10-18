using DisputenPWA.Domain.GroupAggregate.Queries;
using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.Groups;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Groups.Handlers.Queries
{
    public class GetGroupHandler : IRequestHandler<GetGroupQuery, GetGroupQueryResult>
    {
        private readonly IGroupConnector _groupConnector;

        public GetGroupHandler(
            IGroupConnector groupConnector
            )
        {
            _groupConnector = groupConnector;
        }

        public async Task<GetGroupQueryResult> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupConnector.GetGroup(request.GroupId);
            return new GetGroupQueryResult(group);
        }
    }
}
