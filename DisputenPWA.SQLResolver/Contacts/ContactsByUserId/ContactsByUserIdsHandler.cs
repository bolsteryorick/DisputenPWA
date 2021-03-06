using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Contacts.ContactsByUserId
{
    public class ContactsByUserIdsHandler : IRequestHandler<ContactsByUserIdsRequest, IReadOnlyCollection<Contact>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IOutsideContactRepository _outsideContactRepository;
        private readonly IResolveForContactsService _resolveForContactsService;

        public ContactsByUserIdsHandler(
            IContactRepository contactRepository,
            IOutsideContactRepository outsideContactRepository,
            IResolveForContactsService resolveForContactsService
            )
        {
            _contactRepository = contactRepository;
            _outsideContactRepository = outsideContactRepository;
            _resolveForContactsService = resolveForContactsService;
        }

        public async Task<IReadOnlyCollection<Contact>> Handle(ContactsByUserIdsRequest request, CancellationToken cancellationToken)
        {
            var query = _contactRepository.GetQueryable().Where(c => request.UserIds.Contains(c.UserId));
            var outsideContactsQuery = _outsideContactRepository.GetQueryable().Where(c => request.UserIds.Contains(c.UserId));
            return await _resolveForContactsService.Resolve(query, outsideContactsQuery, request.Helper, cancellationToken);
        }
    }
}
