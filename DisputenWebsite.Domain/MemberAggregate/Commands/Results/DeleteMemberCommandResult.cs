using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class DeleteMemberCommandResult : CommandResult<Member>
    {
        public DeleteMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
