using DisputenPWA.Domain.GroupAggregate;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisputenPWA.Domain.Helpers.PropertyHelpers
{
    public class GroupPropertyHelper : PropertyHelperBase
    {
        public bool GetName { get; }
        public bool GetDescription { get; }
        public bool GetAppEvents { get; }
        public bool GetMembers { get; }

        public AppEventPropertyHelper AppEventPropertyHelper { get; }
        public MemberPropertyHelper MemberPropertyHelper { get; }

        public bool CanGetAppEvents()
        {
            return GetAppEvents && AppEventPropertyHelper != null;
        }

        public bool CanGetMembers()
        {
            return GetMembers && MemberPropertyHelper != null;
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
                else if (Equals(name, nameof(Group.Members))) GetMembers = true;
            }
            AppEventPropertyHelper = GetAppEventPropertyHelper(fields, nameof(Group.AppEvents), lowestEndDate, highestStartDate);
            MemberPropertyHelper = GetMemberPropertyHelper(fields, nameof(Group.Members));
        }
    }
}