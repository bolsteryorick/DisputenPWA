using DisputenPWA.Domain.AttendeeAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.AttendeeAggregate.Commands
{
    public class DeleteAttendeeCommand : IRequest<DeleteAttendeeCommandResult>
    {
        public DeleteAttendeeCommand(
            Guid attendeeId
            )
        {
            AttendeeId = attendeeId;
        }

        public Guid AttendeeId { get; }
    }
}
