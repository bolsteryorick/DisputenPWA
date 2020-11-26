using System;

namespace DisputenPWA.Domain.AttendeeAggregate.Commands
{
    public class CreateAttendeeCommand : JoinEventCommand
    {
        public CreateAttendeeCommand(string userId, Guid appEventId) : base(userId, appEventId)
        {
        }
    }
}
