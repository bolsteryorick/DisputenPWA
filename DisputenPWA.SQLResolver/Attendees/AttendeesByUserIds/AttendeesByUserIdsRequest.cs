using DisputenPWA.Domain.AttendeeAggregate;
using MediatR;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.Attendees.AttendeesByUserIds
{
    public class AttendeesByUserIdsRequest : IRequest<IReadOnlyCollection<Attendee>>
    {
        public AttendeesByUserIdsRequest(
            IEnumerable<string> userIds,
            AttendeePropertyHelper helper
            ) 
        {
            UserIds = userIds;
            Helper = helper;
        }

        public IEnumerable<string> UserIds { get; }
        public AttendeePropertyHelper Helper { get; }
    }
}
