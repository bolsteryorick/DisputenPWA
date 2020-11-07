using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.GroupAggregate.Queries.Results
{
    public class GetGroupQueryResult : QueryResult<Group>
    {
        public GetGroupQueryResult(Group group)
            : base(group)
        {

        }
    }
}