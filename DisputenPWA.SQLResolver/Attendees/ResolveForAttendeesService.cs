using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.SQLResolver.AppEvents.AppEventsByIds;
using DisputenPWA.SQLResolver.Users.UsersById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Attendees
{
    public interface IResolveForAttendeesService
    {
        Task<IList<Attendee>> GetEventsForAttendees(IList<Attendee> attendees,
             IEnumerable<Guid> eventIds, AttendeePropertyHelper helper, CancellationToken cancellationToken);
        Task<IList<Attendee>> GetUsersForAttendees(IList<Attendee> attendees, IEnumerable<string> userIds, AttendeePropertyHelper helper, CancellationToken cancellationToken);
    }

    public class ResolveForAttendeesService : IResolveForAttendeesService
    {
        private readonly IMediator _mediator;

        public ResolveForAttendeesService(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<IList<Attendee>> GetEventsForAttendees(IList<Attendee> attendees,
             IEnumerable<Guid> eventIds, AttendeePropertyHelper helper, CancellationToken cancellationToken)
        {
            var events = await _mediator.Send(new AppEventsByIdsRequest(eventIds, helper.AppEventPropertyHelper), cancellationToken);
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

        public async Task<IList<Attendee>> GetUsersForAttendees(IList<Attendee> attendees, IEnumerable<string> userIds, AttendeePropertyHelper helper, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new UsersByIdsRequest(userIds, helper.UserPropertyHelper), cancellationToken);
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
