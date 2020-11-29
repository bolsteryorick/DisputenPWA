using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Queries.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Queries
{
    public class GetAttendee : IRequest<GetAttendeeResult>
    {
        public GetAttendee(
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
