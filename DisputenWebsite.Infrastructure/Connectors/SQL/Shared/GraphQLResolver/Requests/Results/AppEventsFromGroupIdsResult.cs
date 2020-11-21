using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.SeedWorks.Cqrs;
using System.Collections.Generic;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results
{
    public class AppEventsFromGroupIdsResult : QueryResult<IReadOnlyCollection<AppEvent>>
    {
        public AppEventsFromGroupIdsResult(IReadOnlyCollection<AppEvent> result) : base(result)
        {
        }
    }
}
