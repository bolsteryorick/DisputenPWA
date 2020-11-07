using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class UpdateGroupCommandResult : CommandResult<Group>
    {
        public UpdateGroupCommandResult(Group group)
            : base(group)
        {

        }
    }
}
