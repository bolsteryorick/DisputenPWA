using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results
{
    public class DeleteAttendeeCommandResult : CommandResult<Attendee>
    {
        public DeleteAttendeeCommandResult(Attendee result) : base(result)
        {
        }
    }
}
