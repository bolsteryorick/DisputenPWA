using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate
{
    public class Attendee : IdModelBase
    {
        public string UserId { get; set; }
        public Guid AppEventId { get; set; }
        public bool Paid { get; set; }
        public AppEvent AppEvent { get; set; }
        public User User { get; set; }

        public DalAttendee CreateDalAttendee()
        {
            return new DalAttendee
            {
                Id = Id,
                UserId = UserId,
                AppEventId = AppEventId,
                Paid = Paid,
            };
        }
    }
}
