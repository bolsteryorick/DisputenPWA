using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Commands
{
    public class DeleteAttendeeHandler : IRequestHandler<DeleteAttendeeCommand, DeleteAttendeeCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;

        public DeleteAttendeeHandler(
            IOperationAuthorizer operationAuthorizer,
            IAttendeeConnector attendeeConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _attendeeConnector = attendeeConnector;
        }

        public async Task<DeleteAttendeeCommandResult> Handle(DeleteAttendeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _operationAuthorizer.CanChangeAttendee(request.AttendeeId))
            {
                return new DeleteAttendeeCommandResult(null);
            }
            await _attendeeConnector.Delete(request.AttendeeId);
            return new DeleteAttendeeCommandResult(null);
        }
    }
}
