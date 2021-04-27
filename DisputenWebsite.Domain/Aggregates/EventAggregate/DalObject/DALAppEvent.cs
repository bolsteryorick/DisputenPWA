using DisputenPWA.Domain.Aggregates.AttendeeAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.GroupAggregate.DalObject;
using DisputenPWA.Domain.Hierarchy;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.EventAggregate.DalObject
{
    public class DalAppEvent : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxAttendees { get; set; }
        public Guid GroupId { get; set; }
        public string GoogleEventId { get; set; }
        public virtual DalGroup Group { get; set; }
        public virtual ICollection<DalAttendee> Attendances { get; set; }

        public AppEvent CreateAppEvent()
        {
            return new AppEvent
            {
                Id = Id,
                Name = Name,
                Description = Description,
                StartTime = StartTime,
                EndTime = EndTime,
                MaxAttendees = MaxAttendees,
                GroupId = GroupId
            };
        }

        public Event CreateGoogleCalendarEvent => new Event
        {
            Summary = Name,
            Start = new EventDateTime()
            {
                DateTime = StartTime
            },
            End = new EventDateTime()
            {
                DateTime = EndTime
            },
            Description = Description,
        };
    }
}
