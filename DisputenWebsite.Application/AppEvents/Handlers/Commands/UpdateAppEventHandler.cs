using DisputenPWA.Application.Base;
using DisputenPWA.Domain.EventAggregate.Commands;
using DisputenPWA.Domain.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class UpdateAppEventHandler : UpdateHandlerBase, IRequestHandler<UpdateAppEventCommand, UpdateAppEventCommandResult>
    {
        private readonly IAppEventConnector _appEventConnector;

        public UpdateAppEventHandler(
            IAppEventConnector appEventConnector
            )
        {
            _appEventConnector = appEventConnector;
        }

        public async Task<UpdateAppEventCommandResult> Handle(UpdateAppEventCommand request, CancellationToken cancellationToken)
        {
            var updateProperties = GetUpdateProperties(request);
            var appEvent = await _appEventConnector.UpdateProperties(updateProperties, request.Id);
            return new UpdateAppEventCommandResult(appEvent);
        }
    }
}
