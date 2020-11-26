using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.AttendeeAggregate.Queries.Results
{
    public class GetAttendeeResult : QueryResult<Attendee>
    {
        protected GetAttendeeResult(Attendee result) : base(result)
        {
        }
    }
}
