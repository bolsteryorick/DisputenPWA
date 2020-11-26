using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.AttendeeAggregate.Commands.Results
{
    public class UpdateAttendeeCommandResult : CommandResult<Attendee>
    {
        protected UpdateAttendeeCommandResult(Attendee result) : base(result)
        {
        }
    }
}
