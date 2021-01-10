using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Contacts
{
    public interface IResolveForContactsService
    {
        Task<IReadOnlyCollection<Contact>> Resolve(
           IQueryable<DalPlatformContact> query,
           IQueryable<DalOutsideContact> outsideContactsQuery,
           ContactPropertyHelper helper,
           CancellationToken cancellationToken
           );
    }

    public class ResolveForContactsService : IResolveForContactsService
    {
        private readonly IMediator _mediator;
        private readonly IContactRepository _contactRepository;

        public ResolveForContactsService(
            IMediator mediator,
            IContactRepository contactRepository
            ) 
        {
            _mediator = mediator;
            _contactRepository = contactRepository;
        }

        public async Task<IReadOnlyCollection<Contact>> Resolve(
           IQueryable<DalPlatformContact> query,
           IQueryable<DalOutsideContact> outsideContactsQuery,
           ContactPropertyHelper helper,
           CancellationToken cancellationToken
           )
        {
            var contacts = await _contactRepository.GetAll(query, helper);
            contacts = await AddForeignObjects(contacts, helper, cancellationToken);
            contacts = await AddOutsideContacts(contacts, outsideContactsQuery);
            return contacts.ToImmutableList();
        }

        private async Task<IList<Contact>> AddOutsideContacts(
            IList<Contact> contacts,
            IQueryable<DalOutsideContact> outsideContactsQuery)
        {
            var outsideContacts = await outsideContactsQuery.ToListAsync();
            outsideContacts.ForEach(o => contacts.Add(o.CreateContact()));
            return contacts;
        }

        private async Task<IList<Contact>> AddForeignObjects(
            IList<Contact> contacts,
            ContactPropertyHelper helper,
            CancellationToken cancellationToken)
        {
            if (helper.CanGetUser())
            {
                var users = await GetUsers(contacts, helper, cancellationToken);
                contacts = AddUsersToContacts(users, contacts);
            }
            return contacts;
        }

        private async Task<IReadOnlyCollection<User>> GetUsers(
            IList<Contact> contacts,
             ContactPropertyHelper helper,
             CancellationToken cancellationToken)
        {
            var userIds = contacts.Select(x => x.ContactUserId);
            return await _mediator.Send(new UsersByIdsRequest(userIds, helper.UserPropertyHelper), cancellationToken);
        }

        private IList<Contact> AddUsersToContacts(
            IReadOnlyCollection<User> users,
            IList<Contact> contacts)
        {
            var usersDictionary = users.ToDictionary(x => x.Id);
            foreach (var contact in contacts)
            {
                if (usersDictionary.TryGetValue(contact.ContactUserId, out var user))
                {
                    contact.User = user;
                }
            }
            return contacts;
        }
    }
}
