﻿using DisputenPWA.Domain.EventAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.UserAggregate;
using System;

namespace DisputenPWA.Domain.AttendeeAggregate.DalObject
{
    public class DalAttendee : IdModelBase
    {
        public string UserId { get; set; }
        public Guid AppEventId { get; set; }
        public bool Paid { get; set; }
        public virtual DalAppEvent AppEvent { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Attendee CreateAttendee()
        {
            return new Attendee
            {
                Id = Id,
                UserId = UserId,
                AppEventId = AppEventId,
                Paid = Paid,
            };
        }
    }
}
