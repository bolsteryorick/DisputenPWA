using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Domain.EventAggregate.Queries.Results
{
    public class GetAppEventQueryResult : QueryResult<AppEvent>
    {
        public GetAppEventQueryResult(AppEvent appEvent) : base(appEvent) { }

    }
}