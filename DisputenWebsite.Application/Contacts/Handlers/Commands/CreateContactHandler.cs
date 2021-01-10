using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Contacts;
using DisputenPWA.Infrastructure.Connectors.SQL.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Contacts.Handlers.Commands
{
    public class CreateContactHandler : IRequestHandler<CreateContactCommand, CreateContactResult>
    {
        private readonly IUserService _userService;
        private readonly IContactConnector _contactConnector;
        private readonly IUserConnector _userConnector;

        public CreateContactHandler(
            IUserService userService,
            IContactConnector contactConnector,
            IUserConnector userConnector
            )
        {
            _userService = userService;
            _contactConnector = contactConnector;
            _userConnector = userConnector;
        }

        public async Task<CreateContactResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact
            {
                UserId = _userService.GetUserId(),
                ContactUserId = request.ContactId
            };

            await _contactConnector.Create(contact);
            return new CreateContactResult(contact);
        }
    }
}
