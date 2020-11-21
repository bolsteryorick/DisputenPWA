using DisputenPWA.Domain.UserAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class UsersByIdsRequest : IRequest<UsersByIdsResult>
    {
        public UsersByIdsRequest(
            IEnumerable<string> userIds, 
            UserPropertyHelper helper
            )
        {
            UserIds = userIds;
            Helper = helper;
        }

        public IEnumerable<string> UserIds { get; }
        public UserPropertyHelper Helper { get; }
    }
}