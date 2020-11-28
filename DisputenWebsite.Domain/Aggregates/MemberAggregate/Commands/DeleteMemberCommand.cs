using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate.Commands
{
    public class DeleteMemberCommand : IRequest<DeleteMemberCommandResult>
    {
        public DeleteMemberCommand(
            Guid memberId
            )
        {
            MemberId = memberId;
        }

        public Guid MemberId { get; }
    }
}
