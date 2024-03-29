﻿using DisputenPWA.Domain.Aggregates.EventAggregate.Queries.Results;
using MediatR;
using System;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.Queries
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
