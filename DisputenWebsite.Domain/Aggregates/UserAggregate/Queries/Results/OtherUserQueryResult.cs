using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results
{
    public class OtherUserQueryResult : QueryResult<User>
    {
        public OtherUserQueryResult(User user) : base(user)
        {

        }
    }
}
