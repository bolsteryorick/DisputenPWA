using DisputenPWA.Domain.Aggregates.ContactAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.SQLResolver.Contacts.ContactsByUserId
{
    public class ContactsByUserIdsRequest : IRequest<IReadOnlyCollection<Contact>>
    {
        public ContactsByUserIdsRequest(
            IEnumerable<string> userIds,
            ContactPropertyHelper helper
            )
        {
            UserIds = userIds;
            Helper = helper;
        }

        public IEnumerable<string> UserIds { get; }
        public ContactPropertyHelper Helper { get; }
    }
}
