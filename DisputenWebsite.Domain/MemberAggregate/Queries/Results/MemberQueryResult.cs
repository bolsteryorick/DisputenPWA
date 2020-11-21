using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.MemberAggregate.Queries.Results
{
    public class MemberQueryResult : QueryResult<Member>
    {
        public MemberQueryResult(Member member)
            : base(member)
        {

        }
    }
}