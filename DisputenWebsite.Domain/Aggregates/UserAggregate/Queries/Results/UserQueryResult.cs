using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.UserAggregate.Queries.Results
{
    public class UserQueryResult : QueryResult<User>
    {
        public UserQueryResult(User user) : base(user)
        {

        }
    }
}
