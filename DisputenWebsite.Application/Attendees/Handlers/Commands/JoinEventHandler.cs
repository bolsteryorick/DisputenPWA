using DisputenPWA.Application.Services;
using DisputenPWA.Application.Services.Google;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Infrastructure.Connectors.SQL.AppEvents;
using DisputenPWA.Infrastructure.Connectors.SQL.Attendees;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Attendees.Handlers.Commands
{
    public class JoinEventHandler : IRequestHandler<JoinEventCommand, CreateAttendeeCommandResult>
    {
        private readonly IOperationAuthorizer _operationAuthorizer;
        private readonly IBusinessAuthorizer _businessAuthorizer;
        private readonly IAttendeeConnector _attendeeConnector;
        private readonly IUserService _userService;
        private readonly IGoogleAccessInfoRepository _googleAccessInfoRepository;
        private readonly IGoogleCalendarService _googleCalendarService;
        private readonly IMemberRepository _memberRepository;

        public JoinEventHandler(
            IOperationAuthorizer operationAuthorizer,
            IBusinessAuthorizer businessAuthorizer,
            IAttendeeConnector attendeeConnector,
            IUserService userService,
            IGoogleAccessInfoRepository googleAccessInfoRepository,
            IGoogleCalendarService googleCalendarService,
            IMemberRepository memberRepository
            )
        {
            _operationAuthorizer = operationAuthorizer;
            _businessAuthorizer = businessAuthorizer;
            _attendeeConnector = attendeeConnector;
            _userService = userService;
            _googleAccessInfoRepository = googleAccessInfoRepository;
            _googleCalendarService = googleCalendarService;
            _memberRepository = memberRepository;
        }

        public async Task<CreateAttendeeCommandResult> Handle(JoinEventCommand request, CancellationToken cancellationToken)
        {
            var appEventId = request.AppEventId;
            if (!await _operationAuthorizer.CanJoinEvent(appEventId))
            {
                return new CreateAttendeeCommandResult(null);
            }

            if (!await _businessAuthorizer.CanAddAttendee(appEventId))
            {
                return new CreateAttendeeCommandResult(null);
            }

            var userId = _userService.GetUserId();
            var attendee = Attendee.ForJoiningEvent(userId, appEventId);
            await _attendeeConnector.Create(attendee);

            var googleInfo = await _googleAccessInfoRepository.GetQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
            if(googleInfo != null && await UserIsNotEventCreator(appEventId))
            {
                await _googleCalendarService.JoinGoogleEvent(appEventId);
            }

            return new CreateAttendeeCommandResult(attendee);
        }

        public async Task<bool> UserIsNotEventCreator(Guid appEventId)
        {
            return !(await _operationAuthorizer.CanChangeAppEvent(appEventId));
        }
    }
}
