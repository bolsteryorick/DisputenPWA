using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class MembersByUserIdsResult : QueryResult<IReadOnlyCollection<Member>>
    {
        public MembersByUserIdsResult(IReadOnlyCollection<Member> result) : base(result)
        {
        }
    }
}
