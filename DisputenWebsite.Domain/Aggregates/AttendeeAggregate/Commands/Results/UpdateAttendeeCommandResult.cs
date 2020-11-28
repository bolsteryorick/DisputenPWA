using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results
{
    public class UpdateAttendeeCommandResult : CommandResult<Attendee>
    {
        public UpdateAttendeeCommandResult(Attendee result) : base(result)
        {
        }
    }
}
