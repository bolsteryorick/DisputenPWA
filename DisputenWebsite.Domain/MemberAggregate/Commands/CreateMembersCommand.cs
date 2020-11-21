using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands
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
