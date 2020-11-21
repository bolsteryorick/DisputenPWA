using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class MembersByGroupIdsRequest : IRequest<MembersByGroupIdsResult>
    {
        public MembersByGroupIdsRequest(
            IEnumerable<Guid> groupIds, 
            MemberPropertyHelper helper
            )
        {
            GroupIds = groupIds;
            Helper = helper;
        }

        public IEnumerable<Guid> GroupIds { get; }
        public MemberPropertyHelper Helper { get; }
    }
}
