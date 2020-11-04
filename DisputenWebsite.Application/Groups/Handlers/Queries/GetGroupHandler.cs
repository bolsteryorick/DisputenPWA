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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var group = await GetGroup(request);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            group.Name = elapsedMs.ToString();
            return new GetGroupQueryResult(group);
        }

        public async Task<Group> GetGroup(GetGroupQuery request)
        {
            return await _groupConnector.GetGroup(
                request.GroupId, 
                request.LowestEndDate ?? EventRange.LowestEndDate, 
                request.HighestStartDate ?? EventRange.HighestStartDate, 
                request.GroupPropertyHelper
                );
        }
    }
}
