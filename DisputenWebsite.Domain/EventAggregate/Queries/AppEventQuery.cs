using DisputenPWA.Domain.EventAggregate.Queries.Results;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using MediatR;
using System;

namespace DisputenPWA.Domain.EventAggregate.Queries
{
    public class AppEventQuery : IRequest<GetAppEventQueryResult>
    {
        public Guid EventId { get; }
        public AppEventPropertyHelper AppEventPropertyHelper { get; } 

        public AppEventQuery(Guid eventId, AppEventPropertyHelper appEventPropertyHelper)
        {
            EventId = eventId;
            AppEventPropertyHelper = appEventPropertyHelper;
        }
    }
}
