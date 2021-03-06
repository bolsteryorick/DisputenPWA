using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands
{
    public class CreateAttendeeCommand : IRequest<CreateAttendeeCommandResult>
    {
        public CreateAttendeeCommand(
            string userId,
            Guid appEventId
            )
        {
            UserId = userId;
            AppEventId = appEventId;
        }

        public string UserId { get; }
        public Guid AppEventId { get; }
    }
}