using DisputenPWA.Domain.AttendeeAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.SQLResolver.Attendees.AttendeesByEventIds
{
    public class AttendeesByEventIdsRequest : IRequest<IReadOnlyCollection<Attendee>>
    {
        public AttendeesByEventIdsRequest(
            IEnumerable<Guid> eventIds,
            AttendeePropertyHelper helper
            )
        {
            EventIds = eventIds;
            Helper = helper;
        }

        public IEnumerable<Guid> EventIds { get; }
        public AttendeePropertyHelper Helper { get; }
    }
}
