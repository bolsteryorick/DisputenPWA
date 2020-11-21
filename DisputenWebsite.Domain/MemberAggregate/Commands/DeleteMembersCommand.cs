using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.MemberAggregate.Commands
{
    public class DeleteMembersCommand : IRequest<DeleteMembersCommandResult>
    {
        public DeleteMembersCommand(
            Guid groupId
            )
        {
            GroupId = groupId;
        }

        public Guid GroupId { get; }
    }
}
