using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands;
using DisputenPWA.Domain.Aggregates.ContactAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Contacts;
using DisputenPWA.Infrastructure.Connectors.SQL.Users;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Contacts.Handlers.Commands
{
    public class CreateContactsHandler : IRequestHandler<CreateOutsideContactsCommand, CreateContactsResult>
    {
        private readonly IUserService _userService;
        private readonly IContactConnector _contactConnector;
        private readonly IUserConnector _userConnector;

        public CreateContactsHandler(
            IUserService userService,
            IContactConnector contactConnector,
            IUserConnector userConnector
            )
        {
            _userService = userService;
            _contactConnector = contactConnector;
            _userConnector = userConnector;
        }

        public async Task<CreateContactsResult> Handle(CreateOutsideContactsCommand request, CancellationToken cancellationToken)
        {
            var contacts = request.EmailAddresses.Select(x => new Contact
            {
                UserId = _userService.GetUserId(),
                EmailAddress = x
            });

            var platformContacts = new List<Contact>();
            var outsideContacts = new List<Contact>();
            var usersForEmails = await _userConnector.GetUsersByEmail(request.EmailAddresses);
            foreach(var contact in contacts)
            {
                var userForEmail = usersForEmails.FirstOrDefault(u => u.Email.ToLower() == contact.EmailAddress.ToLower());
                if (userForEmail != null)
                {
                    contact.UserId = userForEmail.Id;
                    contact.EmailAddress = null;
                    platformContacts.Add(contact);
                }
                else
                {
                    outsideContacts.Add(contact);
                }
            }

            await _contactConnector.CreatePlatformContact(platformContacts);
            await _contactConnector.CreateOutsideContact(contacts);
            return new CreateContactsResult(null);
        }
    }
}