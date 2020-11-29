using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Attendees.AttendeeById
{
    public class AttendeeByIdHandler : IRequestHandler<AttendeeByIdRequest, Attendee>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IResolveForAttendeesService _resolveForAttendeesService;

        public AttendeeByIdHandler(
            IAttendeeRepository attendeeRepository,
            IResolveForAttendeesService resolveForAttendeesService
            )
        {
            _attendeeRepository = attendeeRepository;
            _resolveForAttendeesService = resolveForAttendeesService;
        }

        public async Task<Attendee> Handle(AttendeeByIdRequest req, CancellationToken cancellationToken)
        {
            var query = _attendeeRepository.GetQueryable().Where(a => a.Id == req.AttendeeId);
            var attendeeInList = await _resolveForAttendeesService.Resolve(query, req.Helper, cancellationToken);
            return attendeeInList.FirstOrDefault();
        }
    }
}
