using DisputenPWA.Domain.GroupAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.GroupAggregate.Commands
{
    public class DeleteGroupCommand : IRequest<DeleteGroupCommandResult>
    {
        public Guid GroupId { get; }
        public DeleteGroupCommand(Guid groupId)
        {
            GroupId = groupId;
        }
    }
}
