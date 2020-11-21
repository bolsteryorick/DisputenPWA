using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class AppEventByIdResult : QueryResult<AppEvent>
    {
        public AppEventByIdResult(AppEvent appEvent) : base(appEvent) { }
    }
}