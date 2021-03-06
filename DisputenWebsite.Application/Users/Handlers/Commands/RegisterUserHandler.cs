using DisputenPWA.Application.Users.Shared;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.Contacts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IOutsideContactRepository _outsideContactRepository;
        private readonly IContactConnector _contactConnector;

        public RegisterUserHandler(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IOutsideContactRepository outsideContactRepository,
            IContactConnector contactConnector
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _outsideContactRepository = outsideContactRepository;
            _contactConnector = contactConnector;
        }

        public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                // domain error?
                return new RegisterUserCommandResult(new User { AccessToken = null });
            }

            await MoveOutsideContactsToPlatformContacts(user.Email, user.Id);

            return new RegisterUserCommandResult(user.CreateUser());
        }

        private async Task MoveOutsideContactsToPlatformContacts(string newUserEmail, string newUserId)
        {
            var contactsQuery = _outsideContactRepository.GetQueryable().Where(x => x.EmailAddress.ToLower() == newUserEmail.ToLower());

            var contacts = await _outsideContactRepository.GetAll(contactsQuery);
            contacts.ToList().ForEach(x => x.UserId = newUserId);

            await _contactConnector.DeleteOutsideContacts(contactsQuery);
            await _contactConnector.CreatePlatformContact(contacts);
        }
    }
}