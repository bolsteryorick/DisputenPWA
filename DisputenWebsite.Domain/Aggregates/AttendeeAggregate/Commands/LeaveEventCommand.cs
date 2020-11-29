using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands
{
    public class LeaveEventCommand : IRequest<DeleteAttendeeCommandResult>
    {
        public LeaveEventCommand(
            Guid attendeeId
            )
        {
            AttendeeId = attendeeId;
        }

        public Guid AttendeeId { get; }
    }
}
