using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Commands
{
    public class LeaveGroupCommand : IRequest<DeleteMemberCommandResult>
    {
        public LeaveGroupCommand(
            Guid memberId
            )
        {
            MemberId = memberId;
        }

        public Guid MemberId { get; }
    }
}
