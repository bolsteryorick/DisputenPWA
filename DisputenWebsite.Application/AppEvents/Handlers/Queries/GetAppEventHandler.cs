using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.Queries;
using DisputenPWA.Domain.EventAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.AppEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Queries
{
    public class GetAppEventHandler : IRequestHandler<GetAppEventQuery, GetAppEventQueryResult>
    {
        private readonly IAppEventConnector _appEventConnector;

        public GetAppEventHandler(
            IAppEventConnector appEventConnector
            )
        {
            _appEventConnector = appEventConnector;
        }

        public async Task<GetAppEventQueryResult> Handle(GetAppEventQuery request, CancellationToken cancellationToken)
        {
            //var appEvent = await _appEventConnector.GetAppEvent(request.EventId);
            var appEvent = new AppEvent();
            return new GetAppEventQueryResult(appEvent);
        }
    }
}
