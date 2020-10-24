using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.Commands;
using DisputenPWA.Domain.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.AppEvents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class UpdateAppEventHandler 
        : IRequestHandler<UpdateAppEventCommand, UpdateAppEventCommandResult>
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
            var appEvent = new AppEvent
            {
                Name = request.Name,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };
            await _appEventConnector.UpdateAppEvent(appEvent);
            return new UpdateAppEventCommandResult(appEvent);
        }
    }
}
