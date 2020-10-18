using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class UpdateGroupCommandResult : CommandResult<Group>
    {
        public UpdateGroupCommandResult(Group group)
            : base(group)
        {

        }
    }
}
