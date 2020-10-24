using DisputenPWA.Domain.EventAggregate.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Queries
{
    public class GetAppEventQuery : IRequest<GetAppEventQueryResult>
    {
        public Guid EventId { get; }

        public GetAppEventQuery(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
