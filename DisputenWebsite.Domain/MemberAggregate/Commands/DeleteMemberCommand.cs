using DisputenPWA.Domain.MemberAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands
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
