using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands.Results;
using MediatR;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.GroupAggregate.Commands
{
    public class CreateGroupCommand : IRequest<CreateGroupCommandResult>
    {
        public CreateGroupCommand(string name, string description, IEnumerable<string> userIds)
        {
            Name = name;
            Description = description;
            UserIds = userIds;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> UserIds { get; }
    }
}
