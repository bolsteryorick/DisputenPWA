using DisputenPWA.Domain.SeedWorks.Cqrs;
using DisputenPWA.Domain.UserAggregate;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class UsersByIdsResult : QueryResult<IReadOnlyCollection<User>>
    {
        public UsersByIdsResult(IReadOnlyCollection<User> result) : base(result)
        {
        }
    }
}