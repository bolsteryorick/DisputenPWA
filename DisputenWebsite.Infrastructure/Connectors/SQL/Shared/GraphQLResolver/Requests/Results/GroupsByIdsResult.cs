using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class GroupsByIdsResult : QueryResult<IReadOnlyCollection<Group>>
    {
        public GroupsByIdsResult(IReadOnlyCollection<Group> result) : base(result)
        {
        }
    }
}
