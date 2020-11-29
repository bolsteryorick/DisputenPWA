using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.SQLResolver.Attendees.AttendeesByEventIds;
using DisputenPWA.SQLResolver.Groups.GroupsByIds;
using DisputenPWA.SQLResolver.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.AppEvents
{
    public interface IResolveForAppEventsService
    {
        Task<IReadOnlyCollection<AppEvent>> Resolve(
            IQueryable<DalAppEvent> query,
            AppEventPropertyHelper helper,
            CancellationToken cancellationToken
            );
    }

    public class ResolveForAppEventsService : IResolveForAppEventsService
    {
        private readonly IMediator _mediator;
        private readonly IAppEventRepository _appEventRepository;

        public ResolveForAppEventsService(
            IMediator mediator,
            IAppEventRepository appEventRepository
            )
        {
            _mediator = mediator;
            _appEventRepository = appEventRepository;
        }

        public async Task<IReadOnlyCollection<AppEvent>> Resolve(
            IQueryable<DalAppEvent> query,
            AppEventPropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var events = await _appEventRepository.GetAll(query, helper);
            events = await AddForeignObjects(events, helper, cancellationToken);
            return events.ToImmutableList();
        }

        private async Task<IList<AppEvent>> AddForeignObjects(
            IList<AppEvent> events, 
            AppEventPropertyHelper helper, 
            CancellationToken cancellationToken
            )
        {
            if (helper.CanGetGroup())
            {
                var groups = await GetGroups(events, helper, cancellationToken);
                events = AddGroupsToAppEvents(groups, events);
            }
            if (helper.CanGetAttendees())
            {
                var attendees = await GetAttendees(events, helper, cancellationToken);
                events = AddAttendeesToAppEvents(attendees, events);
            }
            return events;
        }

        private async Task<IReadOnlyCollection<Group>> GetGroups(
            IList<AppEvent> events, 
            AppEventPropertyHelper helper, 
            CancellationToken cancellationToken
            )
        {
            var groupIds = events.Select(x => x.GroupId).Distinct();
            return await _mediator.Send(new GroupsByIdsRequest(groupIds, helper.GroupPropertyHelper), cancellationToken);
        }

        private async Task<IReadOnlyCollection<Attendee>> GetAttendees(
            IList<AppEvent> events, 
            AppEventPropertyHelper helper, 
            CancellationToken cancellationToken
            )
        {
            var appEventIds = events.Select(x => x.Id);
            return await _mediator.Send(new AttendeesByEventIdsRequest(appEventIds, helper.AttendeePropertyHelper), cancellationToken);
        }

        private IList<AppEvent> AddGroupsToAppEvents(
            IReadOnlyCollection<Group> groups, 
            IList<AppEvent> events
            )
        {
            var groupsDictionary = groups.ToDictionary(x => x.Id);
            foreach (var appEvent in events)
            {
                if (groupsDictionary.TryGetValue(appEvent.GroupId, out var group)) appEvent.Group = group;
            }
            return events;
        }

        private IList<AppEvent> AddAttendeesToAppEvents(
            IReadOnlyCollection<Attendee> attendees, 
            IList<AppEvent> events
            )
        {
            var attendeesDictionary = DictionaryMaker.MakeDictionary<Guid, Attendee>(nameof(Attendee.AppEventId), attendees);
            foreach (var appEvent in events)
            {
                if (attendeesDictionary.TryGetValue(appEvent.GroupId, out var eventAttendees)) appEvent.Attendees = eventAttendees;
            }
            return events;
        }
    }
}
