using DisputenPWA.Application.Base;
using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Commands
{
    public class UpdateAttendeeHandler : UpdateHandlerBase, IRequestHandler<UpdateAttendeeCommand, UpdateAttendeeCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;

        public UpdateAttendeeHandler(
            IOperationAuthorizer operationAuthorizer,
            IAttendeeConnector attendeeConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _attendeeConnector = attendeeConnector;
        }

        public async Task<UpdateAttendeeCommandResult> Handle(UpdateAttendeeCommand request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanChangeAttendee(request.Id))
            {
                return new UpdateAttendeeCommandResult(null);
            }
            var properties = GetUpdateProperties(request);
            var attendee = await _attendeeConnector.UpdateProperties(properties, request.Id);
            return new UpdateAttendeeCommandResult(attendee);
        }
    }
}
