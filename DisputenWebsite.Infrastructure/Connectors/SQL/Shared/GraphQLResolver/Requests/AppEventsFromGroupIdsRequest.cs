using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class AppEventsFromGroupIdsRequest : IRequest<AppEventsFromGroupIdsResult>
    {
        public AppEventsFromGroupIdsRequest(
            IEnumerable<Guid> groupIds,
           AppEventPropertyHelper helper)
        {
            GroupIds = groupIds;
            Helper = helper;
        }

        public IEnumerable<Guid> GroupIds { get; }
        public AppEventPropertyHelper Helper { get; }
    }
}
