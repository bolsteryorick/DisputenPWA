using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Domain.EventAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

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
