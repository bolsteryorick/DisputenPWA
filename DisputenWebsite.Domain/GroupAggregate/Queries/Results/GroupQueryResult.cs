using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.GroupAggregate.Queries.Results
{
    public class GroupQueryResult : QueryResult<Group>
    {
        public GroupQueryResult(Group group)
            : base(group)
        {

        }
    }
}