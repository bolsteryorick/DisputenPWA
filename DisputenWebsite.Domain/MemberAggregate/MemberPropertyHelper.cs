using DisputenPWA.Domain.GroupAggregate;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.UserAggregate;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.MemberAggregate
{
    public class MemberPropertyHelper : PropertyHelperBase
    {
        public bool GetIsAdmin { get; }
        public bool GetUser { get; }
        public bool GetGroup { get; }

        public GroupPropertyHelper GroupPropertyHelper { get; }
        public UserPropertyHelper UserPropertyHelper { get; }

        public bool CanGetGroup()
        {
            return GetGroup && GroupPropertyHelper != null;
        }

        public bool CanGetUser()
        {
            return GetUser && UserPropertyHelper != null;
        }

        public MemberPropertyHelper(
              IEnumerable<Field> fields,
              int depth = 1
            )
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(Member.IsAdmin))) GetIsAdmin = true;
                else if (Equals(name, nameof(Member.User))) GetUser = true;
                else if (Equals(name, nameof(Member.Group))) GetGroup = true;
            }
            if (CanGoDeeper(depth))
            {
                GroupPropertyHelper = GetGroupPropertyHelper(fields, nameof(Member.Group), EventRange.LowestEndDate, EventRange.HighestStartDate, depth);
                UserPropertyHelper = GetUserPropertyHelper(fields, nameof(Member.User), depth);
            }
        }
    }
}
