using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.SQLResolver.Attendees.AttendeesByEventIds;
using DisputenPWA.SQLResolver.Groups.GroupsByIds;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.AppEvents
{
    public interface IResolveForAppEventsService
    {
        Task<IList<AppEvent>> GetGroupsForAppEvents(IList<AppEvent> events, IEnumerable<Guid> groupIds, AppEventPropertyHelper helper, CancellationToken cancellationToken);
        Task<IList<AppEvent>> GetAttendeesForAppEvents(IList<AppEvent> events, IEnumerable<Guid> appEventIds, AppEventPropertyHelper helper, CancellationToken cancellationToken);
    }

    public class ResolveForAppEventsService : IResolveForAppEventsService
    {
        private readonly IMediator _mediator;

        public ResolveForAppEventsService(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<IList<AppEvent>> GetGroupsForAppEvents(IList<AppEvent> events, IEnumerable<Guid> groupIds, AppEventPropertyHelper helper, CancellationToken cancellationToken)
        {
            var groups = await _mediator.Send(new GroupsByIdsRequest(groupIds, helper.GroupPropertyHelper), cancellationToken);
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var appEvent in events)
            {
                if (groupsDictionary.TryGetValue(appEvent.GroupId, out var group)) appEvent.Group = group;
            }
            return events;
        }

        public async Task<IList<AppEvent>> GetAttendeesForAppEvents(IList<AppEvent> events, IEnumerable<Guid> appEventIds, AppEventPropertyHelper helper, CancellationToken cancellationToken)
        {
            var attendees = await _mediator.Send(new AttendeesByEventIdsRequest(appEventIds, helper.AttendeePropertyHelper), cancellationToken);
            var attendeesDictionary = MakeEventIdToAttendeeDict(attendees);
            foreach (var appEvent in events)
            {
                if (attendeesDictionary.TryGetValue(appEvent.GroupId, out var eventAttendees)) appEvent.Attendees = eventAttendees;
            }
            return events;
        }

        private Dictionary<Guid, List<Attendee>> MakeEventIdToAttendeeDict(IReadOnlyCollection<Attendee> items)
        {
            var dict = new Dictionary<Guid, List<Attendee>>();
            foreach (var item in items)
            {
                var appEventId = item.AppEventId;
                if (!dict.ContainsKey(appEventId))
                {
                    dict[appEventId] = new List<Attendee>();
                }
                dict[appEventId].Add(item);
            }
            return dict;
        }

    }
}
