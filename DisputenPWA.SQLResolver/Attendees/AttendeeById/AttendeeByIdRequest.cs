using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using MediatR;
using System;

namespace DisputenPWA.SQLResolver.Attendees.AttendeeById
{
    public class AttendeeByIdRequest : IRequest<Attendee>
    {
        public AttendeeByIdRequest(
            Guid attendeeId,
            AttendeePropertyHelper helper
            )
        {
            AttendeeId = attendeeId;
            Helper = helper;
        }

        public Guid AttendeeId { get; }
        public AttendeePropertyHelper Helper { get; }
    }
}
