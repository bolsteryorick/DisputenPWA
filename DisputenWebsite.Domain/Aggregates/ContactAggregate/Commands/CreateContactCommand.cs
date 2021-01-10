using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Commands
{
    public class CreateContactCommand : IRequest<CreateContactResult>
    {
        public CreateContactCommand(
            string contactId
            )
        {
            ContactId = contactId;
        }

        public string ContactId { get; }
    }
}
