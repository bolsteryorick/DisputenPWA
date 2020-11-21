using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.UserAggregate.Queries.Results
{
    public class JwtTokenQueryResult : QueryResult<User>
    {
        public JwtTokenQueryResult(User user) : base(user)
        {

        }
    }
}
