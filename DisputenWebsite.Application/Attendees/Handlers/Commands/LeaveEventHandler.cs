using DisputenPWA.Application.Services;
using DisputenPWA.Application.Services.Google;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Commands
{
    public class LeaveEventHandler : IRequestHandler<LeaveEventCommand, DeleteAttendeeCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;
        private readonly IGoogleCalendarService _googleCalendarService;
        private readonly IAttendeeRepository _attendeeRepository;

        public LeaveEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IAttendeeConnector attendeeConnector,
            IGoogleCalendarService googleCalendarService,
            IAttendeeRepository attendeeRepository
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _attendeeConnector = attendeeConnector;
            _googleCalendarService = googleCalendarService;
            _attendeeRepository = attendeeRepository;
        }

        public async Task<DeleteAttendeeCommandResult> Handle(LeaveEventCommand request, CancellationToken cancellationToken)
        {
            if(!await _operationAuthorizer.CanLeaveEvent(request.AttendeeId))
            {
                return new DeleteAttendeeCommandResult(null);
            }
            var attendee = await _attendeeRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == request.AttendeeId);
            await _attendeeConnector.Delete(request.AttendeeId);
            await _googleCalendarService.LeaveGoogleEvent(attendee.AppEventId);
            return new DeleteAttendeeCommandResult(null);
        }
    }
}
