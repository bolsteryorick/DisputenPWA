using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class MembersByGroupIdsResult : QueryResult<IReadOnlyCollection<Member>>
    {
        public MembersByGroupIdsResult(IReadOnlyCollection<Member> result) : base(result)
        {
        }
    }
}
