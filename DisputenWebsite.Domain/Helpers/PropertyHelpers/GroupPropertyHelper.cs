using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisputenPWA.Domain.GroupAggregate.Helpers
{
    public class GroupPropertyHelper : PropertyHelperBase
    {
        public bool GetName { get; set; }
        public bool GetDescription { get; set; }
        public bool GetAppEvents { get; set; }

        public AppEventPropertyHelper AppEventPropertyHelper { get; private set; }

        public bool CanGetAppEvents()
        {
            return GetAppEvents && AppEventPropertyHelper != null;
        }

        public GroupPropertyHelper(
            IEnumerable<Field> fields,
            DateTime? lowestEndDate,
            DateTime? highestStartDate
            )
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(Group.Name))) GetName = true;
                else if (Equals(name, nameof(Group.Description))) GetDescription = true;
                else if (Equals(name, nameof(Group.AppEvents))) GetAppEvents = true;
            }

            var appEventFields = GetSubFields(fields.FirstOrDefault(x => x.Name.ToLower() == nameof(Group.AppEvents).ToLower()));
            if (appEventFields.Count > 0)
            {
                AppEventPropertyHelper = new AppEventPropertyHelper(
                    appEventFields, 
                    lowestEndDate, 
                    highestStartDate
                );
            }
        }
    }
}
