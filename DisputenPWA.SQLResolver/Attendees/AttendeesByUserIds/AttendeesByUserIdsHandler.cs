using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.AttendeeAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Attendees.AttendeesByUserIds
{
    public class AttendeesByUserIdsHandler : IRequestHandler<AttendeesByUserIdsRequest, IReadOnlyCollection<Attendee>>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IResolveForAttendeesService _resolveForAttendeesService;

        public AttendeesByUserIdsHandler(
            IAttendeeRepository attendeeRepository,
            IResolveForAttendeesService resolveForAttendeesService
            )
        {
            _attendeeRepository = attendeeRepository;
            _resolveForAttendeesService = resolveForAttendeesService;
        }

        public async Task<IReadOnlyCollection<Attendee>> Handle(AttendeesByUserIdsRequest request, CancellationToken cancellationToken)
        {
            return await ResolveAttendeesByUserIds(request.UserIds, request.Helper, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Attendee>> ResolveAttendeesByUserIds(
            IEnumerable<string> userIds, AttendeePropertyHelper helper, CancellationToken cancellationToken
            )
        {
            var attendees = await GetAttendeesByUserIds(userIds, helper);
            if (helper.CanGetAppEvent())
            {
                var eventIds = attendees.Select(x => x.AppEventId);
                attendees = await _resolveForAttendeesService.GetEventsForAttendees(attendees, eventIds, helper, cancellationToken);
            }
            if (helper.CanGetUser())
            {
                attendees = await _resolveForAttendeesService.GetUsersForAttendees(attendees, userIds, helper, cancellationToken);
            }
            return attendees.ToImmutableList();
        }

        private async Task<IList<Attendee>> GetAttendeesByUserIds(
           IEnumerable<string> userIds, AttendeePropertyHelper helper
           )
        {
            var attendeesQuery = _attendeeRepository.GetQueryable().Where(a => userIds.Contains(a.UserId));
            return await _attendeeRepository.GetAll(attendeesQuery, helper);
        }
    }
}
