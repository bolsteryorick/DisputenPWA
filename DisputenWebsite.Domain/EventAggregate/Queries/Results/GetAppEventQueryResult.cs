using DisputenPWA.Domain.SeedWorks.Cqrs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Queries.Results
{
    public class GetAppEventQueryResult : QueryResult<AppEvent>
    {
        public GetAppEventQueryResult(AppEvent appEvent) : base(appEvent) { }

    }
}