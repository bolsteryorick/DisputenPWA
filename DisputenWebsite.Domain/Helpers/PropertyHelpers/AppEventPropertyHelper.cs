using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputenPWA.Domain.EventAggregate.Helpers
{
    public class AppEventPropertyHelper : PropertyHelperBase
    {
        public bool GetId { get; set; }
        public bool GetName { get; private set; }
        public bool GetDescription { get; private set; }
        public bool GetStartTime { get; set; }
        public bool GetEndTime { get; set; }
        public bool GetGroupId { get; set; }
        public bool GetGroup { get; set; }
        public GroupPropertyHelper GroupPropertyHelper { get; set; }

        public AppEventPropertyHelper(IList<Field> fields)
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(AppEvent.Id))) GetId = true;
                else if (Equals(name, nameof(AppEvent.Name))) GetName = true;
                else if (Equals(name, nameof(AppEvent.Description))) GetDescription = true;
                else if (Equals(name, nameof(AppEvent.StartTime))) GetStartTime = true;
                else if (Equals(name, nameof(AppEvent.EndTime))) GetEndTime = true;
                else if (Equals(name, nameof(AppEvent.GroupId))) GetGroupId = true;
                else if (Equals(name, nameof(AppEvent.Group))) GetGroup = true;
            }

            var groupFields = GetSubFields(fields.FirstOrDefault(x => x.Name == "group"));
            if(groupFields.Count > 0)
            {
                GroupPropertyHelper = new GroupPropertyHelper(groupFields);
            }
        }
    }
}
