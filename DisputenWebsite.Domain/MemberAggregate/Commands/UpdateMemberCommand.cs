using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.MemberAggregate.Commands
{
    public class UpdateMemberCommand : IRequest<UpdateMemberCommandResult>
    {
        public UpdateMemberCommand(
            Guid memberId,
            string userId,
            bool? isAdmin
            )
        {
            MemberId = memberId;
            UserId = userId;
            IsAdmin = isAdmin;
        }

        public Guid MemberId { get; }
        public string UserId { get; }
        public bool? IsAdmin { get; }
    }
}