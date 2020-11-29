using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class CreateAppEventHandler
        : IRequestHandler<CreateAppEventCommand, CreateAppEventCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAppEventConnector _appEventConnector;

        public CreateAppEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAppEventConnector appEventConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _appEventConnector = appEventConnector;
        }

        public async Task<CreateAppEventCommandResult> Handle(CreateAppEventCommand req, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanUpdateGroup(req.GroupId))
            {
                return new CreateAppEventCommandResult(null);
            }

            var appEvent = new AppEvent
            {
                Name = req.Name,
                Description = req.Description,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                MaxAttendees = req.Maxattendees,
                GroupId = req.GroupId
            };
            await _appEventConnector.Create(appEvent);
            return new CreateAppEventCommandResult(appEvent);
        }
    }
}
