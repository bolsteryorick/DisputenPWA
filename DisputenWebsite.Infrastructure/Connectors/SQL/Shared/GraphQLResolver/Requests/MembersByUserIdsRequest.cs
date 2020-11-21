using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class MembersByUserIdsRequest : IRequest<MembersByUserIdsResult>
    {
        public MembersByUserIdsRequest(
            IEnumerable<string> userIds, 
            MemberPropertyHelper helper
            )
        {
            UserIds = userIds;
            Helper = helper;
        }

        public IEnumerable<string> UserIds { get; }
        public MemberPropertyHelper Helper { get; }
    }
}
