using DisputenPWA.DAL.Repositories;
using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.Domain.AttendeeAggregate.DalObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisputenPWA.Infrastructure.Connectors.SQL.Attendees
{
    public interface IAttendeeConnector
    {

    }

    public class AttendeeConnector : IAttendeeConnector
    {
        private readonly IAttendeeRepository _attendeeRepository;

        public AttendeeConnector(
            IAttendeeRepository attendeeRepository
            )
        {
            _attendeeRepository = attendeeRepository;
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
