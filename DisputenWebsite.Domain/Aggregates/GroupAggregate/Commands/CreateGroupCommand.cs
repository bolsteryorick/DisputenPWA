using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.Commands
{
    public class CreateGroupCommand : IRequest<CreateGroupCommandResult>
    {
        public CreateGroupCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
