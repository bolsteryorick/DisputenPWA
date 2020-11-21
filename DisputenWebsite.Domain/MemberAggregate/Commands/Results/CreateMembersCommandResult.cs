using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate.Commands.Results
{
    public class CreateMembersCommandResult : CommandResult<Member>
    {
        public CreateMembersCommandResult(Member member)
            : base(member)
        {

        }
    }
}
