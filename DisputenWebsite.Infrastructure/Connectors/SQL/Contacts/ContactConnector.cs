using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate.DalObjects;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Contacts
{
    public interface IContactConnector
    {
        Task Create(Contact contact);
        Task Delete(Guid id);
        Task CreatePlatformContact(IEnumerable<Contact> contacts);
        Task CreateOutsideContact(IEnumerable<Contact> contacts);
        Task<Contact> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class ContactConnector : IContactConnector
    {
        private readonly IContactRepository _contactRepository;
        private readonly IOutsideContactRepository _outsideContactRepository;

        public ContactConnector(
            IContactRepository contactRepository,
            IOutsideContactRepository outsideContactRepository
            )
        {
            _contactRepository = contactRepository;
            _outsideContactRepository = outsideContactRepository;
        }

        // No get function in this connector, as contacts are always retrieved through user object

        public async Task Create(Contact contact)
        {
            await _contactRepository.Add(contact.CreateDalContact());
        }

        public async Task Delete(Guid id)
        {
            await _contactRepository.DeleteByObject(new DalPlatformContact { Id = id });
        }

        public async Task CreatePlatformContact(IEnumerable<Contact> contacts)
        {
            var dalContacts = contacts.Select(x => x.CreateDalContact());
            await _contactRepository.Add(dalContacts);
        }

        public async Task CreateOutsideContact(IEnumerable<Contact> contacts)
        {
            var dalContacts = contacts.Select(x => x.CreateDalOutsideContact());
            await _outsideContactRepository.Add(dalContacts);
        }

        public async Task<Contact> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var contact = await _contactRepository
                .UpdateProperties(new DalPlatformContact { Id = id }, properties);
            return contact.CreateContact();
        }
    }
}
