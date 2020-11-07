using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class SeedGroupsCommandResult : CommandResult<Group>
    {
        public SeedGroupsCommandResult(Group group)
            : base(group)
        {

        }
    }
}
