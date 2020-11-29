using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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

        public async Task<IReadOnlyCollection<Attendee>> Handle(AttendeesByUserIdsRequest req, CancellationToken cancellationToken)
        {
            var query = _attendeeRepository.GetQueryable().Where(a => req.UserIds.Contains(a.UserId));
            return await _resolveForAttendeesService.Resolve(query, req.Helper, cancellationToken);
        }
    }
}
