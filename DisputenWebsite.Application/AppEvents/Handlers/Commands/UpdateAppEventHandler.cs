using DisputenPWA.Application.Base;
using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class UpdateAppEventHandler : UpdateHandlerBase, IRequestHandler<UpdateAppEventCommand, UpdateAppEventCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAppEventConnector _appEventConnector;

        public UpdateAppEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAppEventConnector appEventConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _appEventConnector = appEventConnector;
        }

        public async Task<UpdateAppEventCommandResult> Handle(UpdateAppEventCommand request, CancellationToken cancellationToken)
        {
            if (await _operationAuthorizer.CanChangeAppEvent(request.Id))
            {
                var updateProperties = GetUpdateProperties(request);
                var appEvent = await _appEventConnector.UpdateProperties(updateProperties, request.Id);
                return new UpdateAppEventCommandResult(appEvent);
            }
            return new UpdateAppEventCommandResult(null);
        }
    }
}
