using DisputenPWA.Domain.AttendeeAggregate.Queries.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.AttendeeAggregate.Queries
{
    public class GetAttendee : IRequest<GetAttendeeResult>
    {
        public GetAttendee(
            Guid attendeeId
            )
        {
            AttendeeId = attendeeId;
        }

        public Guid AttendeeId { get; }
    }
}
