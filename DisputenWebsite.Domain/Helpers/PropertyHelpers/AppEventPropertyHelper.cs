using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisputenPWA.Domain.EventAggregate.Helpers
{
    public class AppEventPropertyHelper : PropertyHelperBase
    {
        public bool GetName { get; set; }
        public bool GetDescription { get; set; }
        public bool GetStartTime { get; set; }
        public bool GetEndTime { get; set; }
        public bool GetGroup { get; set; }
        public GroupPropertyHelper GroupPropertyHelper { get; set; }

        public DateTime LowestEndDate { get; set; }
        public DateTime HighestStartDate { get; set; }

        public bool CanGetGroup()
        {
            return GetGroup && GroupPropertyHelper != null;
        }

        public AppEventPropertyHelper(
            IEnumerable<Field> fields, 
            DateTime? lowestEndDate = null,
            DateTime? highestStartDate = null
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

            var groupFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == nameof(AppEvent.Group).ToLower()));
            if(groupFields.Count > 0)
            {
                GroupPropertyHelper = new GroupPropertyHelper(groupFields, lowestEndDate, highestStartDate);
            }
        }
    }
}
