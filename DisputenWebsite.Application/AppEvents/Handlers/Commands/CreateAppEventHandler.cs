using DisputenPWA.Application.Services;
using DisputenPWA.Application.Services.Google;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands;
using DisputenPWA.Domain.Aggregates.EventAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.AppEvents.Handlers.Commands
{
    public class CreateAppEventHandler
        : IRequestHandler<CreateAppEventCommand, CreateAppEventCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAppEventConnector _appEventConnector;
        private readonly IMediator _mediator;
        private readonly IGoogleCalendarService _googleCalendarService;

        public CreateAppEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAppEventConnector appEventConnector,
            IMediator mediator,
            IGoogleCalendarService googleCalendarService
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _appEventConnector = appEventConnector;
            _mediator = mediator;
            _googleCalendarService = googleCalendarService;
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
            await AddgoogleEvent(appEvent.Id);
            await _mediator.Send(new JoinEventCommand(appEvent.Id));

            return new CreateAppEventCommandResult(appEvent);
        }

        private async Task AddgoogleEvent(Guid appEventId)
        {
            var googleEventId = await _googleCalendarService.CreateGoogleEvent(appEventId);
            await _appEventConnector.UpdateProperties(
                new Dictionary<string, object> {
                    { nameof(DalAppEvent.GoogleEventId), googleEventId }
                },
                appEventId
            );
        }
    }
}
