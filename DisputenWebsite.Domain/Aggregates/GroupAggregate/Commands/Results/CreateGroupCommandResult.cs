using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results
{
    public class CreateGroupCommandResult : CommandResult<Group>
    {
        public CreateGroupCommandResult(Group group)
            : base(group)
        {

        }
    }
}