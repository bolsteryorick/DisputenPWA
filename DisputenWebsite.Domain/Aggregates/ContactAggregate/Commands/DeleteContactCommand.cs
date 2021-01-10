using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate.Commands
{
    public class DeleteContactCommand : IRequest<DeleteContactResult>
    {
        public DeleteContactCommand(
            Guid contactId
            )
        {
            ContactId = contactId;
        }

        public Guid ContactId { get; }
    }
}
