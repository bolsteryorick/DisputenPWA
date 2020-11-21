using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.MemberAggregate;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.UserAggregate
{
    public class UserPropertyHelper : PropertyHelperBase
    {
        public bool GetId { get; }
        public bool GetEmail { get; }
        public bool GetUserName { get; }
        public bool GetGroupMemberships { get; }
        public MemberPropertyHelper MembershipsPropertyHelper { get; }

        public bool CanGetMembers()
        {
            return GetGroupMemberships && MembershipsPropertyHelper != null;
        }

        // for seeding
        public UserPropertyHelper()
        {
            GetId = true;
        }

        public UserPropertyHelper(
            IEnumerable<Field> fields,
            int depth = 1
            )
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(User.Id))) GetId = true;
                else if (Equals(name, nameof(User.Email))) GetEmail = true;
                else if (Equals(name, nameof(User.UserName))) GetUserName = true;
                else if (Equals(name, nameof(User.Memberships))) GetGroupMemberships = true;
            }
            if (CanGoDeeper(depth))
            {
                MembershipsPropertyHelper = GetMemberPropertyHelper(fields, nameof(User.Memberships), depth);
            }
        }
    }
}