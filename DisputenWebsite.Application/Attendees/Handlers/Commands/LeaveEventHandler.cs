using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Commands
{
    public class LeaveEventHandler : IRequestHandler<LeaveEventCommand, DeleteAttendeeCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;

        public LeaveEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAttendeeConnector attendeeConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _attendeeConnector = attendeeConnector;
        }

        public async Task<DeleteAttendeeCommandResult> Handle(LeaveEventCommand request, CancellationToken cancellationToken)
        {
            if(await _operationAuthorizer.CanLeaveEvent(request.AttendeeId))
            {
                await _attendeeConnector.Delete(request.AttendeeId);
            }
            return new DeleteAttendeeCommandResult(null);
        }
    }
}
