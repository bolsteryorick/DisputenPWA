using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class UpdateMemberCommandResult : CommandResult<Member>
    {
        public UpdateMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
