using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.SQLResolver.AppEvents.AppEventsByIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Attendees.AttendeesByEventIds
{
    public class AttendeesByEventIdsHandler : IRequestHandler<AttendeesByEventIdsRequest, IReadOnlyCollection<Attendee>>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IResolveForAttendeesService _resolveForAttendeesService;

        public AttendeesByEventIdsHandler(
            IAttendeeRepository attendeeRepository,
            IResolveForAttendeesService resolveForAttendeesService
            )
        {
            _attendeeRepository = attendeeRepository;
            _resolveForAttendeesService = resolveForAttendeesService;
        }

        public async Task<IReadOnlyCollection<Attendee>> Handle(AttendeesByEventIdsRequest request, CancellationToken cancellationToken)
        {
            return await ResolveAttendeesByEventIds(request.EventIds, request.Helper, cancellationToken);
        }

        private async Task<IReadOnlyCollection<Attendee>> ResolveAttendeesByEventIds(
            IEnumerable<Guid> eventIds, AttendeePropertyHelper helper, CancellationToken cancellationToken
            )
        {
            var attendees = await GetAttendeesByEventIds(eventIds, helper);
            if (helper.CanGetAppEvent())
            {
                attendees = await _resolveForAttendeesService.GetEventsForAttendees(attendees, eventIds, helper, cancellationToken);
            }
            if (helper.CanGetUser())
            {
                var userIds = attendees.Select(x => x.UserId);
                attendees = await _resolveForAttendeesService.GetUsersForAttendees(attendees, userIds, helper, cancellationToken);
            }
            return attendees.ToImmutableList();
        }

        private async Task<IList<Attendee>> GetAttendeesByEventIds(
            IEnumerable<Guid> eventIds, AttendeePropertyHelper helper
            )
        {
            var attendeesQuery = _attendeeRepository.GetQueryable().Where(a => eventIds.Contains(a.AppEventId));
            return await _attendeeRepository.GetAll(attendeesQuery, helper);
        }
    }
}
