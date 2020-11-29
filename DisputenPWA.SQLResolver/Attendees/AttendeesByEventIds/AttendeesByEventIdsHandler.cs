using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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

        public async Task<IReadOnlyCollection<Attendee>> Handle(AttendeesByEventIdsRequest req, CancellationToken cancellationToken)
        {
            var query = _attendeeRepository.GetQueryable().Where(a => req.EventIds.Contains(a.AppEventId));
            return await _resolveForAttendeesService.Resolve(query, req.Helper, cancellationToken);
        }
    }
}
