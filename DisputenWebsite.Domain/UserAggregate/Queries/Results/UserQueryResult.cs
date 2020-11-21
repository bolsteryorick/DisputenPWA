using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.UserAggregate.Queries.Results
{
    public class UserQueryResult : QueryResult<User>
    {
        public UserQueryResult(User user) : base(user)
        {

        }
    }
}
