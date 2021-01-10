using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Contacts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Contacts.Handlers.Commands
{
    public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, DeleteContactResult>
    {
        private readonly IContactConnector _contactConnector;

        public DeleteContactHandler(
            IContactConnector contactConnector
            )
        {
            _contactConnector = contactConnector;
        }

        public async Task<DeleteContactResult> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            await _contactConnector.Delete(request.ContactId);
            return new DeleteContactResult(null);
        }
    }
}
