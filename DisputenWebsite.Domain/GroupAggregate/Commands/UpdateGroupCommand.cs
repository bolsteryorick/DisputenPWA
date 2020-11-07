using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.GroupAggregate.Commands
{
    public class UpdateGroupCommand : IRequest<UpdateGroupCommandResult>
    {
        public UpdateGroupCommand(
            Guid id,
            string name, 
            string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}
