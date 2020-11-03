using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class SeedGroupsCommandResult : CommandResult<Group>
    {
        public SeedGroupsCommandResult(Group group)
            : base(group)
        {

        }
    }
}
