using DisputenPWA.Domain.AttendeeAggregate.Commands.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.AttendeeAggregate.Commands
{
    public class JoinEventCommand : IRequest<CreateAttendeeCommandResult>
    {
        public JoinEventCommand(
            Guid appEventId
            )
        {
            AppEventId = appEventId;
        }

        public Guid AppEventId { get; }
    }
}
