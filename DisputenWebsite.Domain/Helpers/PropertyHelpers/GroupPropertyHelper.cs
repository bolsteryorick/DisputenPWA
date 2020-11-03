using DisputenPWA.Domain.EventAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using GraphQL.Language.AST;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputenPWA.Domain.GroupAggregate.Helpers
{
    public class GroupPropertyHelper : PropertyHelperBase
    {
        public bool GetId { get; set; }
        public bool GetName { get; private set; }
        public bool GetDescription { get; private set; }
        public bool GetAppEvents { get; private set; }

        public AppEventPropertyHelper AppEventPropertyHelper { get; private set; }

        public GroupPropertyHelper(IEnumerable<Field> fields)
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(Group.Id))) GetId = true;
                else if (Equals(name, nameof(Group.Name))) GetName = true;
                else if (Equals(name, nameof(Group.Description))) GetDescription = true;
                else if (Equals(name, nameof(Group.AppEvents))) GetAppEvents = true;
            }

            var appEventFields = GetSubFields(fields.FirstOrDefault(x => x.Name == "appEvents"));
            if (appEventFields.Count > 0)
            {
                AppEventPropertyHelper = new AppEventPropertyHelper(appEventFields);
            }
        }
    }
}
