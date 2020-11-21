using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class CreateMemberCommandResult : CommandResult<Member>
    {
        public CreateMemberCommandResult(Member member)
            : base(member)
        {

        }
    }
}
