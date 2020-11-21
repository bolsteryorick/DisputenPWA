using DisputenPWA.Domain.SeedWorks.Cqrs;
using DisputenPWA.Domain.UserAggregate;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class UserByIdResult : QueryResult<User>
    {
        public UserByIdResult(User result) : base(result)
        {
        }
    }
}
