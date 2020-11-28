using DisputenPWA.Domain.Aggregates.EventAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.SQLResolver.AppEvents.AppEventsFromGroupsIds
{
    public class AppEventsFromGroupIdsRequest : IRequest<IReadOnlyCollection<AppEvent>>
    {
        public AppEventsFromGroupIdsRequest(
            IEnumerable<Guid> groupIds,
           AppEventPropertyHelper helper)
        {
            GroupIds = groupIds;
            Helper = helper;
        }

        public IEnumerable<Guid> GroupIds { get; }
        public AppEventPropertyHelper Helper { get; }
    }
}
