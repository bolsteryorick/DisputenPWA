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
        private readonly IBusinessAuthorizer _businessAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;
        private readonly IUserService _userService;

        public CreateAttendeeHandler(
            IOperationAuthorizer operationAuthorizer,
            IBusinessAuthorizer businessAuthorizer,
            IAttendeeConnector attendeeConnector,
            IUserService userService
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _businessAuthorizer = businessAuthorizer;
            _attendeeConnector = attendeeConnector;
            _userService = userService;
        }

        public async Task<CreateAttendeeCommandResult> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _operationAuthorizer.CanChangeAppEvent(request.AppEventId))
            {
                return new CreateAttendeeCommandResult(null);
            }

            if (!await _businessAuthorizer.CanAddAttendee(request.AppEventId))
            {
                return new CreateAttendeeCommandResult(null);
            }

            var attendee = Attendee.ForJoiningEvent(request.UserId, request.AppEventId);

            await _attendeeConnector.Create(attendee);
            return new CreateAttendeeCommandResult(attendee);
        }
    }
}
