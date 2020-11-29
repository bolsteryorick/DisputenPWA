using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.EventAggregate.Queries;
using DisputenPWA.Domain.Aggregates.EventAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Queries
{
    public class GetAppEventHandler : IRequestHandler<AppEventQuery, GetAppEventQueryResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAppEventConnector _appEventConnector;

        public GetAppEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAppEventConnector appEventConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _appEventConnector = appEventConnector;
        }

        public async Task<GetAppEventQueryResult> Handle(AppEventQuery request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanQueryAppEvent(request.EventId))
            {
                return new GetAppEventQueryResult(null);
            }
            var appEvent = await _appEventConnector.GetAppEvent(request.EventId, request.AppEventPropertyHelper);
            return new GetAppEventQueryResult(appEvent);
        }
    }
}
