﻿using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.EventAggregate.DalObject;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Hierarchy;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.EventAggregate
{
    public class AppEvent : IdModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MaxAttendees { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public IReadOnlyCollection<Attendee> Attendees { get; set; }

        public DalAppEvent CreateDALAppEvent()
        {
            return new DalAppEvent
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

        public static AppEvent WithUtcDateKind(
                Guid id, string name, string description, DateTime? startTime, DateTime? endTime, int? maxAttendees, Guid groupId
            )
        {
            if (startTime != null)
            {
                startTime = DateTime.SpecifyKind((DateTime)startTime, DateTimeKind.Utc);
            }
            if (endTime != null)
            {
                endTime = DateTime.SpecifyKind((DateTime)endTime, DateTimeKind.Utc);
            }
            return new AppEvent
            {
                Id = id,
                Name = name,
                Description = description,
                StartTime = startTime,
                EndTime = endTime,
                MaxAttendees = maxAttendees,
                GroupId = groupId
            };
        }

        // document
        // notification?
        // location
    }
}