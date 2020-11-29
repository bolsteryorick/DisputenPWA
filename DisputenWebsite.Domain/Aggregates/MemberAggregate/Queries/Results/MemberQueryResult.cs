using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Queries.Results
{
    public class MemberQueryResult : QueryResult<Member>
    {
        public MemberQueryResult(Member member)
            : base(member)
        {

        }
    }
}