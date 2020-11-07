using DisputenPWA.Domain.EventAggregate.Queries;
using DisputenPWA.Domain.EventAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Queries
{
    public class GetAppEventHandler : IRequestHandler<AppEventQuery, GetAppEventQueryResult>
    {
        private readonly IAppEventConnector _appEventConnector;

        public GetAppEventHandler(
            IAppEventConnector appEventConnector
            )
        {
            _appEventConnector = appEventConnector;
        }

        public async Task<GetAppEventQueryResult> Handle(AppEventQuery request, CancellationToken cancellationToken)
        {
            var appEvent = await _appEventConnector.GetAppEvent(request.EventId, request.AppEventPropertyHelper);
            return new GetAppEventQueryResult(appEvent);
        }
    }
}
