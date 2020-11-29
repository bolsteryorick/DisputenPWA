using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.SQLResolver.Attendees.AttendeeById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Attendees
{
    public interface IAttendeeConnector
    {
        Task<Attendee> Get(Guid id, AttendeePropertyHelper helper);
        Task Create(Attendee attendee);
        Task Create(IEnumerable<DalAttendee> dalAttendees);
        Task Delete(Guid id);
        Task<Attendee> UpdateProperties(Dictionary<string, object> properties, Guid id);
    }

    public class AttendeeConnector : IAttendeeConnector
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMediator _mediator;

        public AttendeeConnector(
            IAttendeeRepository attendeeRepository,
            IMediator mediator
            )
        {
            _attendeeRepository = attendeeRepository;
            _mediator = mediator;
        }

        public async Task<Attendee> Get(Guid id, AttendeePropertyHelper helper)
        {
            return await _mediator.Send(new AttendeeByIdRequest(id, helper));
        }

        public async Task Create(Attendee attendee)
        {
            await _attendeeRepository.Add(attendee.CreateDalAttendee());
        }

        public async Task Delete(Guid id)
        {
            await _attendeeRepository.DeleteByObject(new DalAttendee { Id = id });
        }

        public async Task Create(IEnumerable<DalAttendee> dalAttendees)
        {
            await _attendeeRepository.Add(dalAttendees);
        }

        public async Task<Attendee> UpdateProperties(Dictionary<string, object> properties, Guid id)
        {
            var dalAttendee = await _attendeeRepository
                .UpdateProperties(new DalAttendee { Id = id }, properties);
            return dalAttendee.CreateAttendee();
        }
    }
}
