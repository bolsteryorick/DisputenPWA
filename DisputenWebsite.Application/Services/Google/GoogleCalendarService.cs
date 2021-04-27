using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.EntityFrameworkCore;

namespace DisputenPWA.Application.Services.Google
{
    public interface IGoogleCalendarService
    {
        Task<string> CreateGoogleEvent(Guid appEventId);
        Task DeleteGoogleEvent(Guid appEventId);
        Task UpdateGoogleEvent(Guid appEventId);
        Task JoinGoogleEvent(Guid appEventId);
        Task LeaveGoogleEvent(Guid appEventId);
    }

    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly IGoogleAccessInfoRepository _googleAccessInfoRepository;
        private readonly IAppEventRepository _appEventRepository;
        private readonly IUserService _userService;
        private readonly IMemberRepository _memberRepository;
        private const string _calendarId = "primary";

        public GoogleCalendarService(
            IGoogleAccessInfoRepository googleAccessInfoRepository,
            IAppEventRepository appEventRepository,
            IUserService userService,
            IMemberRepository memberRepository
            )
        {
            _googleAccessInfoRepository = googleAccessInfoRepository;
            _appEventRepository = appEventRepository;
            _userService = userService;
            _memberRepository = memberRepository;
        }

        public async Task<string> CreateGoogleEvent(Guid appEventId)
        {
            var service = await GetCalendarServiceForLoggedInUser();
            var calendarEvent = await CreateCalendarEvent(appEventId);
            var createdEvent = await service.Events.Insert(calendarEvent, _calendarId).ExecuteAsync();
            return createdEvent.Id;
        }

        public async Task DeleteGoogleEvent(Guid appEventId)
        {
            var appEvent = await _appEventRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == appEventId);
            var service = await GetCalendarServiceForLoggedInUser();
            var request = service.Events.Delete(_calendarId, appEvent.GoogleEventId);
            request.SendUpdates = EventsResource.DeleteRequest.SendUpdatesEnum.All;
            await request.ExecuteAsync();
        }

        public async Task UpdateGoogleEvent(Guid appEventId)
        {
            var appEvent = await _appEventRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == appEventId);
            var service = await GetCalendarServiceForLoggedInUser();
            var request = service.Events.Patch(appEvent.CreateGoogleCalendarEvent, _calendarId, appEvent.GoogleEventId);
            request.SendUpdates = EventsResource.PatchRequest.SendUpdatesEnum.All;
            await request.ExecuteAsync();
        }

        public async Task JoinGoogleEvent(Guid appEventId)
        {
            var eventOwnerUserId = await GetEventOwnerUserId(appEventId);
            var service = await GetCalendarService(eventOwnerUserId);

            var googleEventId = await GetGoogleEventId(appEventId, service);
            var googleEvent = await service.Events.Get(_calendarId, googleEventId).ExecuteAsync();
            var user = await _userService.GetUser();
            if (googleEvent.Attendees == null)
            {
                googleEvent.Attendees = new List<EventAttendee>();
            }
            googleEvent.Attendees.Add(new EventAttendee { Email = user.Email });
            await PatchEvent(service, googleEvent, googleEventId);
        }

        public async Task LeaveGoogleEvent(Guid appEventId)
        {
            var eventOwnerUserId = await GetEventOwnerUserId(appEventId);
            var service = await GetCalendarService(eventOwnerUserId);

            var googleEventId = await GetGoogleEventId(appEventId, service);
            var googleEvent = await service.Events.Get(_calendarId, googleEventId).ExecuteAsync();
            if (googleEvent.Attendees != null)
            {
                var user = await _userService.GetUser();
                googleEvent.Attendees = googleEvent.Attendees.Where(x => x.Email != user.Email).ToList();
            }

            await PatchEvent(service, googleEvent, googleEventId);
        }

        private async Task<Event> CreateCalendarEvent(Guid appEventId)
        {
            var appEvent = await _appEventRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == appEventId);
            return appEvent.CreateGoogleCalendarEvent;
        }

        private async Task<CalendarService> GetCalendarServiceForLoggedInUser()
        {
            return await GetCalendarService(_userService.GetUserId());
        }

        private async Task<CalendarService> GetCalendarService(string userId)
        {
            var googleInfo = await _googleAccessInfoRepository.GetQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
            var credential = GoogleCredential.FromAccessToken(googleInfo.Token);
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DispuutWeb",
            });
        }

        private async Task<string> GetEventOwnerUserId(Guid appEventId)
        {
            var groupId = await _appEventRepository.GetQueryable().Where(x => x.Id == appEventId).Select(x => x.GroupId).FirstOrDefaultAsync();
            return await _memberRepository.GetQueryable().Where(x => x.GroupId == groupId && x.IsAdmin).Select(x => x.UserId).FirstOrDefaultAsync();
        }

        private Task<string> GetGoogleEventId(Guid appEventId, CalendarService service)
        {
            return _appEventRepository.GetQueryable().Where(x => x.Id == appEventId).Select(x => x.GoogleEventId).FirstOrDefaultAsync();
        }

        private async Task PatchEvent(CalendarService service, Event googleEvent, string googleEventId)
        {
            var request = service.Events.Patch(googleEvent, _calendarId, googleEventId);
            request.SendUpdates = EventsResource.PatchRequest.SendUpdatesEnum.All;
            await request.ExecuteAsync();
        }
    }
}