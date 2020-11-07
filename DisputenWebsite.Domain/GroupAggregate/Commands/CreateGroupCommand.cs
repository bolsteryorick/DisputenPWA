using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using MediatR;

namespace DisputenPWA.Domain.GroupAggregate.Commands
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
