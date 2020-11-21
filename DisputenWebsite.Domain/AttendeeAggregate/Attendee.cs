using DisputenPWA.Domain.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.UserAggregate;
using System;

namespace DisputenPWA.Domain.AttendeeAggregate
{
    public class Attendee : IdModelBase
    {
        public string UserId { get; set; }
        public Guid AppEventId { get; set; }
        public bool Paid { get; set; }
        public AppEvent AppEvent { get; set; }
        public User User { get; set; }

        public DalAttendee CreateAttendee()
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
