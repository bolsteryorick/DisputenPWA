using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class CreateGroupCommandResult : CommandResult<Group>
    {
        public CreateGroupCommandResult(Group group)
            : base(group)
        {

        }
    }
}