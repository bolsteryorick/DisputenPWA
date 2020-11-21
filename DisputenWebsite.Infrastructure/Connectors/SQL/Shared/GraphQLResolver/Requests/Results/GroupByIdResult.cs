using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class GroupByIdResult : QueryResult<Group>
    {
        public GroupByIdResult(Group result) : base(result)
        {
        }
    }
}