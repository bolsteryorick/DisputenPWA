using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class DeleteMembersCommandResult : CommandResult<Member>
    {
        public DeleteMembersCommandResult(Member member)
            : base(member)
        {

        }
    }
}
