using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results
{
    public class DeleteContactResult : CommandResult<Contact>
    {
        public DeleteContactResult(Contact result) : base(result)
        {
        }
    }
}
