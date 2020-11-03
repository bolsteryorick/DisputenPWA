using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate.Queries;
using DisputenPWA.Domain.GroupAggregate.Queries.Results;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Infrastructure.Connectors.Groups;
using MediatR;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
            var group = await GetGroup(request);
            return new GetGroupQueryResult(group);
        }

        public async Task<Group> GetGroup(GetGroupQuery request)
        {
            //return await _groupConnector.GetGroup(request);
            return new Group();
        }
    }
}
