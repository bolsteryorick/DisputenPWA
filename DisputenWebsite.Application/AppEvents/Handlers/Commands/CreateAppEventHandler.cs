using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands;
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
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public CreateAppEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAppEventConnector appEventConnector,
            IUserService userService,
            IMediator mediator
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _appEventConnector = appEventConnector;
            _userService = userService;
            _mediator = mediator;
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

            await _mediator.Send(new CreateAttendeeCommand(_userService.GetUserId(), appEvent.Id));

            return new CreateAppEventCommandResult(appEvent);
        }
    }
}
