using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results
{
    public class CreateContactResult : CommandResult<Contact>
    {
        public CreateContactResult(Contact result) : base(result)
        {
        }
    }
}
