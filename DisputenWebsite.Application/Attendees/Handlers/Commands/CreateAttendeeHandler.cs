using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Commands
{
    public class CreateAttendeeHandler : IRequestHandler<CreateAttendeeCommand, CreateAttendeeCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;
        private readonly IUserService _userService;

        public CreateAttendeeHandler(
            IOperationAuthorizer operationAuthorizer,
            IAttendeeConnector attendeeConnector,
            IUserService userService
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _attendeeConnector = attendeeConnector;
            _userService = userService;
        }

        public async Task<CreateAttendeeCommandResult> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
        {
            if (await _operationAuthorizer.CanChangeAppEvent(request.AppEventId))
            {
                var attendee = new Attendee
                {
                    AppEventId = request.AppEventId,
                    UserId = request.UserId
                };
                await _attendeeConnector.Create(attendee);
                return new CreateAttendeeCommandResult(attendee);
            }
            return new CreateAttendeeCommandResult(null);
        }
    }
}
