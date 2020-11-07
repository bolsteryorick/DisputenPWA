using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class DeleteGroupCommandResult : CommandResult<Group>
    {
        public DeleteGroupCommandResult(Group group)
            : base(group)
        {

        }
    }
}
