using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Domain.Hierarchy;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.EventAggregate
{
    public class AppEventPropertyHelper : PropertyHelperBase
    {
        public bool GetName { get; }
        public bool GetDescription { get; }
        public bool GetStartTime { get; }
        public bool GetEndTime { get; }
        public bool GetGroup { get; }
        public bool GetMaxAttendees { get; }
        public bool GetAttendees { get; set; }
        public GroupPropertyHelper GroupPropertyHelper { get; }
        public AttendeePropertyHelper AttendeePropertyHelper { get; }

        public DateTime LowestEndDate { get; }
        public DateTime HighestStartDate { get; }

        public bool CanGetGroup()
        {
            return GetGroup && GroupPropertyHelper != null;
        }

        public bool CanGetAttendees()
        {
            return GetAttendees && AttendeePropertyHelper != null;
        }

        public AppEventPropertyHelper(
            IEnumerable<Field> fields,
            DateTime? lowestEndDate = null,
            DateTime? highestStartDate = null,
            int depth = 1
            )
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(AppEvent.Name))) GetName = true;
                else if (Equals(name, nameof(AppEvent.Description))) GetDescription = true;
                else if (Equals(name, nameof(AppEvent.StartTime))) GetStartTime = true;
                else if (Equals(name, nameof(AppEvent.EndTime))) GetEndTime = true;
                else if (Equals(name, nameof(AppEvent.MaxAttendees))) GetMaxAttendees = true;
                else if (Equals(name, nameof(AppEvent.Group))) GetGroup = true;
                else if (Equals(name, nameof(AppEvent.Attendees))) GetAttendees = true;
            }
            LowestEndDate = lowestEndDate ?? EventRange.LowestEndDate;
            HighestStartDate = highestStartDate ?? EventRange.HighestStartDate;

            if (CanGoDeeper(depth))
            {
                GroupPropertyHelper = GetGroupPropertyHelper(fields, nameof(AppEvent.Group), null, null, depth);
                AttendeePropertyHelper = GetAttendeePropertyHelper(fields, nameof(AppEvent.Attendees), depth);
            }
        }
    }
}
