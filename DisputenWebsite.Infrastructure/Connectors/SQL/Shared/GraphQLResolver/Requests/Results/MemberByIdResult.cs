using DisputenPWA.Domain.MemberAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class MemberByIdResult : QueryResult<Member>
    {
        public MemberByIdResult(Member result) : base(result)
        {
        }
    }
}
