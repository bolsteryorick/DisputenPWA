using DisputenPWA.Domain.Aggregates.EventAggregate;
using MediatR;
using System;

namespace DisputenPWA.SQLResolver.AppEvents.AppEventById
{
    public class AppEventByIdRequest : IRequest<AppEvent>
    {
        public AppEventByIdRequest(
            Guid eventId,
            AppEventPropertyHelper helper
            )
        {
            EventId = eventId;
            Helper = helper;
        }

        public Guid EventId { get; }
        public AppEventPropertyHelper Helper { get; }
    }
}