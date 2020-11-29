using DisputenPWA.Domain.Aggregates.EventAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.AppEvents.AppEventsByIds
{
    public class AppEventsByIdsRequest : IRequest<IReadOnlyCollection<AppEvent>>
    {
        public AppEventsByIdsRequest(
            IEnumerable<Guid> appEventIds,
            AppEventPropertyHelper helper
            )
        {
            AppEventIds = appEventIds;
            Helper = helper;
        }

        public IEnumerable<Guid> AppEventIds { get; }
        public AppEventPropertyHelper Helper { get; }
    }
}
