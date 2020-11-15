using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands
{
    public class UpdateMemberCommand : IRequest<UpdateMemberCommandResult>
    {
        public UpdateMemberCommand(
            Guid memberId,
            string userId,
            bool isAdmin,
            Guid groupId
            )
        {
            MemberId = memberId;
            UserId = userId;
            IsAdmin = isAdmin;
            GroupId = groupId;
        }

        public Guid MemberId { get; }
        public string UserId { get; }
        public bool IsAdmin { get; }
        public Guid GroupId { get; }
    }
}