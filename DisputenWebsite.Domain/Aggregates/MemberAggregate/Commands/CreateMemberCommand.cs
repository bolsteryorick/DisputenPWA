using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Commands
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