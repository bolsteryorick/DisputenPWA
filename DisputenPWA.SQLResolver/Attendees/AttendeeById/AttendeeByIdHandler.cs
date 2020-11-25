using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.Domain.AttendeeAggregate.DalObject;
using DisputenPWA.SQLResolver.AppEvents.AppEventById;
using DisputenPWA.SQLResolver.Users.UserById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.SQLResolver.Attendees.AttendeeById
{
    public class AttendeeByIdHandler : IRequestHandler<AttendeeByIdRequest, Attendee>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMediator _mediator;

        public AttendeeByIdHandler(
            IAttendeeRepository attendeeRepository,
            IMediator mediator
            )
        {
            _attendeeRepository = attendeeRepository;
            _mediator = mediator;
        }

        public async Task<Attendee> Handle(AttendeeByIdRequest req, CancellationToken cancellationToken)
        {
            return await ResolveAttendeeById(req.AttendeeId, req.Helper, cancellationToken);
            throw new NotImplementedException();
        }

        private async Task<Attendee> ResolveAttendeeById(
            Guid attendeeId,
            AttendeePropertyHelper helper, 
            CancellationToken cancellationToken
            )
        {
            var attendee = await GetAttendeeById(attendeeId, helper);
            if (helper.CanGetAppEvent())
            {
                attendee.AppEvent = await _mediator.Send(new AppEventByIdRequest(attendee.AppEventId, helper.AppEventPropertyHelper), cancellationToken);
            }
            if (helper.CanGetUser())
            {
                attendee.User = await _mediator.Send(new UserByIdRequest(attendee.UserId, helper.UserPropertyHelper));
            }
            return attendee;
        }

        private async Task<Attendee> GetAttendeeById(
            Guid attendeeId,
            AttendeePropertyHelper helper
            )
        {
            var attendeeQuery = _attendeeRepository.GetQueryable().Where(a => a.Id == attendeeId);
            return await _attendeeRepository.GetFirstOrDefault(attendeeQuery, helper);
        }
    }
}
