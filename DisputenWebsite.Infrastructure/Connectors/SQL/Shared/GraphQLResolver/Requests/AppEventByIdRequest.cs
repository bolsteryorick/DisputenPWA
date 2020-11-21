using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests.Results;
using MediatR;
using System;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Shared.GraphQLResolver.Requests
{
    public class AppEventByIdRequest : IRequest<AppEventByIdResult>
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