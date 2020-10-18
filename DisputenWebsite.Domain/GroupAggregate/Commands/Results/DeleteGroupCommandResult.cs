using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Commands.Results
{
    public class DeleteGroupCommandResult : CommandResult<Group>
    {
        public DeleteGroupCommandResult(Group group)
            : base(group)
        {

        }
    }
}
