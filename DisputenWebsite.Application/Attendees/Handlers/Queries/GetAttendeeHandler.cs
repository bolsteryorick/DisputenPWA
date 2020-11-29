using DisputenPWA.Application.Services;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Queries;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Queries.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Queries
{
    public class GetAttendeeHandler : IRequestHandler<GetAttendee, GetAttendeeResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;

        public GetAttendeeHandler(
            IOperationAuthorizer operationAuthorizer,
            IAttendeeConnector attendeeConnector
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _attendeeConnector = attendeeConnector;
        }

        public async Task<GetAttendeeResult> Handle(GetAttendee request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanQueryAttendee(request.AttendeeId))
            {
                return new GetAttendeeResult(null);
            }
            var attendee = await _attendeeConnector.Get(request.AttendeeId, request.Helper);
            return new GetAttendeeResult(attendee);
        }
    }
}