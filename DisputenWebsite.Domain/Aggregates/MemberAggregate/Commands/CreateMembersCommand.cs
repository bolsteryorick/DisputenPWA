using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Commands
{
    public class CreateMembersCommand : IRequest<CreateMembersCommandResult>
    {
        public CreateMembersCommand(
            IEnumerable<string> userIds,
            Guid groupId
            )
        {
            UserIds = userIds;
            GroupId = groupId;
        }

        public IEnumerable<string> UserIds { get; }
        public Guid GroupId { get; }
    }
}
