using DisputenPWA.Domain.EventAggregate;
using DisputenPWA.Domain.Helpers;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisputenPWA.Domain.Helpers.PropertyHelpers
{
    public class AppEventPropertyHelper : PropertyHelperBase
    {
        public bool GetName { get; }
        public bool GetDescription { get; }
        public bool GetStartTime { get; }
        public bool GetEndTime { get; }
        public bool GetGroup { get; }
        public GroupPropertyHelper GroupPropertyHelper { get; }

        public DateTime LowestEndDate { get; }
        public DateTime HighestStartDate { get; }

        public bool CanGetGroup()
        {
            return GetGroup && GroupPropertyHelper != null;
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
                else if (Equals(name, nameof(AppEvent.Group))) GetGroup = true;
            }
            LowestEndDate = lowestEndDate ?? EventRange.LowestEndDate;
            HighestStartDate = highestStartDate ?? EventRange.HighestStartDate;

            if(CanGoDeeper(depth))
            {
                GroupPropertyHelper = GetGroupPropertyHelper(fields, nameof(AppEvent.Group), lowestEndDate, highestStartDate, depth);
            }
        }
    }
}
