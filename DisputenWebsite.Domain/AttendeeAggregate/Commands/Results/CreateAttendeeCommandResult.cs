using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.AttendeeAggregate.Commands.Results
{
    public class CreateAttendeeCommandResult : CommandResult<Attendee>
    {
        public CreateAttendeeCommandResult(Attendee result) : base(result)
        {
        }
    }
}
