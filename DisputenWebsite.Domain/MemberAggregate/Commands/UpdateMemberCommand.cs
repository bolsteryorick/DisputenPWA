using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.MemberAggregate.Commands
{
    public class UpdateMemberCommand : IRequest<UpdateMemberCommandResult>
    {
        public UpdateMemberCommand(
            Guid memberId,
            bool? isAdmin
            )
        {
            MemberId = memberId;
            IsAdmin = isAdmin;
        }

        public Guid MemberId { get; }
        public bool? IsAdmin { get; }
    }
}