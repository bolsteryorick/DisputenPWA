using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results
{
    public class CreateContactsResult : CommandResult<Contact>
    {
        public CreateContactsResult(Contact result) : base(result)
        {
        }
    }
}
