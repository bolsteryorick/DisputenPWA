﻿using DisputenPWA.Domain.SeedWorks.Cqrs;

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
