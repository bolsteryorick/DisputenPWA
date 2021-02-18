using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results
{
    public class JwtTokensQueryResult : QueryResult<User>
    {
        public JwtTokensQueryResult(User user) : base(user)
        {

        }
    }
}
