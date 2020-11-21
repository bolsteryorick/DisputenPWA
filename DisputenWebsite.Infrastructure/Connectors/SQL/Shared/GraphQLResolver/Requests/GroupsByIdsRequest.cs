using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class GroupsByIdsRequest : IRequest<GroupsByIdsResult>
    {
        public GroupsByIdsRequest(
            IEnumerable<Guid> groupIds, 
            GroupPropertyHelper helper
            )
        {
            GroupIds = groupIds;
            Helper = helper;
        }

        public IEnumerable<Guid> GroupIds { get; }
        public GroupPropertyHelper Helper { get; }
    }
}
