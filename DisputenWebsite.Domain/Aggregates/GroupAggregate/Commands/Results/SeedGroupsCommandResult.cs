using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results
{
    public class SeedGroupsCommandResult : CommandResult<Group>
    {
        public SeedGroupsCommandResult(Group group)
            : base(group)
        {

        }
    }
}
