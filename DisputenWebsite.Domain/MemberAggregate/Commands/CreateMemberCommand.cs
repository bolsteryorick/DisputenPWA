using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands
{
    public class CreateMemberCommand : IRequest<CreateMemberCommandResult>
    {
        public CreateMemberCommand(
            string userId,
            bool isAdmin,
            Guid groupId
            )
        {
            UserId = userId;
            IsAdmin = isAdmin;
            GroupId = groupId;
        }

        public string UserId { get; }
        public bool IsAdmin { get; }
        public Guid GroupId { get; }
    }
}