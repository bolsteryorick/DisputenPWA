using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate.Queries.Results
{
    public class GetAttendeeResult : QueryResult<Attendee>
    {
        public GetAttendeeResult(Attendee result) : base(result)
        {
        }
    }
}
