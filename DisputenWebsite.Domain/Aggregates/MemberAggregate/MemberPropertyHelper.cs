using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Domain.Hierarchy;
using GraphQL.Language.AST;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.MemberAggregate
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
                GroupPropertyHelper = GetGroupPropertyHelper(fields, nameof(Member.Group), null, null, depth);
                UserPropertyHelper = GetUserPropertyHelper(fields, nameof(Member.User), depth);
            }
        }
    }
}
