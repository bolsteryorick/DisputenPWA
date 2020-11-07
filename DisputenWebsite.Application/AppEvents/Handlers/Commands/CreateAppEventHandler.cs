using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.EventAggregate.Commands;
using DisputenPWA.Domain.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class CreateAppEventHandler
        : IRequestHandler<CreateAppEventCommand, CreateAppEventCommandResult>
    {
        private readonly IAppEventConnector _appEventConnector;

        public CreateAppEventHandler(
            IAppEventConnector appEventConnector
            )
        {
            _appEventConnector = appEventConnector;
        }

        public async Task<CreateAppEventCommandResult> Handle(CreateAppEventCommand request, CancellationToken cancellationToken)
        {
            var appEvent = new AppEvent
            {
                Name = request.Name,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                GroupId = request.GroupId
            };
            await _appEventConnector.Create(appEvent);
            return new CreateAppEventCommandResult(appEvent);
        }
    }
}
