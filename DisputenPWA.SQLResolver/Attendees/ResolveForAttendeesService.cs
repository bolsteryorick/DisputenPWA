using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.SQLResolver.AppEvents.AppEventsByIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Attendees
{
    public interface IResolveForAttendeesService
    {
        Task<IReadOnlyCollection<Attendee>> Resolve(
            IQueryable<DalAttendee> query,
            AttendeePropertyHelper helper,
            CancellationToken cancellationToken
            );
    }

    public class ResolveForAttendeesService : IResolveForAttendeesService
    {
        private readonly IMediator _mediator;
        private readonly IAttendeeRepository _attendeeRepository;

        public ResolveForAttendeesService(
            IMediator mediator,
            IAttendeeRepository attendeeRepository
            )
        {
            _mediator = mediator;
            _attendeeRepository = attendeeRepository;
        }

        public async Task<IReadOnlyCollection<Attendee>> Resolve(
            IQueryable<DalAttendee> query,
            AttendeePropertyHelper helper,
            CancellationToken cancellationToken
            )
        {
            var attendees = await _attendeeRepository.GetAll(query, helper);
            attendees = await AddForeignObjects(attendees, helper, cancellationToken);
            return attendees.ToImmutableList();
        }

        private async Task<IList<Attendee>> AddForeignObjects(
            IList<Attendee> attendees,
            AttendeePropertyHelper helper, 
            CancellationToken cancellationToken)
        {
            if (helper.CanGetAppEvent())
            {
                var events = await GetEvents(attendees, helper, cancellationToken);
                attendees = AddEventsToAttendees(events, attendees);
            }
            if (helper.CanGetUser())
            {
                var users = await GetUsers(attendees, helper, cancellationToken);
                attendees = AddUsersToAttendees(users, attendees);
            }
            return attendees;
        }

        private async Task<IReadOnlyCollection<AppEvent>> GetEvents(
            IList<Attendee> attendees,
            AttendeePropertyHelper helper,
            CancellationToken cancellationToken)
        {
            var eventIds = attendees.Select(x => x.AppEventId);
            return await _mediator.Send(new AppEventsByIdsRequest(eventIds, helper.AppEventPropertyHelper), cancellationToken);
        }

        private async Task<IReadOnlyCollection<User>> GetUsers(
            IList<Attendee> attendees,
             AttendeePropertyHelper helper,
             CancellationToken cancellationToken)
        {
            var userIds = attendees.Select(x => x.UserId);
            return await _mediator.Send(new UsersByIdsRequest(userIds, helper.UserPropertyHelper), cancellationToken);
        }

        private IList<Attendee> AddEventsToAttendees(
            IReadOnlyCollection<AppEvent> events,
            IList<Attendee> attendees)
        {
            var eventsDictionary = events.ToDictionary(x => x.Id);
            foreach (var attendee in attendees)
            {
                if (eventsDictionary.TryGetValue(attendee.AppEventId, out var appEvent))
                {
                    attendee.AppEvent = appEvent;
                }
            }
            return attendees;
        }

        private IList<Attendee> AddUsersToAttendees(
            IReadOnlyCollection<User> users,
            IList<Attendee> attendees)
        {
            var usersDictionary = users.ToDictionary(x => x.Id);
            foreach (var attendee in attendees)
            {
                if (usersDictionary.TryGetValue(attendee.UserId, out var user))
                {
                    attendee.User = user;
                }
            }
            return attendees;
        }
    }
}
