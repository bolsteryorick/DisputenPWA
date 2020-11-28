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

        public async Task<CreateAppEventCommandResult> Handle(CreateAppEventCommand request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanUpdateGroup(request.GroupId))
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
            return new CreateAppEventCommandResult(null);
        }
    }
}
