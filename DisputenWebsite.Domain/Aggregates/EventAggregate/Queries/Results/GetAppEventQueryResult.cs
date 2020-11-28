using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Queries.Results
{
    public class GetAppEventQueryResult : QueryResult<AppEvent>
    {
        public GetAppEventQueryResult(AppEvent appEvent) : base(appEvent) { }
    }
}