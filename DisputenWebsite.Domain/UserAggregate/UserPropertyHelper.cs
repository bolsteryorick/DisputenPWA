using DisputenPWA.Domain.AttendeeAggregate;
using DisputenPWA.Domain.Hierarchy;
using DisputenPWA.Domain.MemberAggregate;
using GraphQL.Language.AST;
using System.Collections.Generic;

namespace DisputenPWA.Domain.UserAggregate
{
    public class UserPropertyHelper : PropertyHelperBase
    {
        public bool GetId { get; }
        public bool GetEmail { get; }
        public bool GetUserName { get; }
        public bool GetGroupMemberships { get; }
        public bool GetAttendences { get; }
        public MemberPropertyHelper MembershipsPropertyHelper { get; }
        public AttendeePropertyHelper AttendeePropertyHelper { get; }

        public bool CanGetMembers()
        {
            return GetGroupMemberships && MembershipsPropertyHelper != null;
        }

        public bool CanGetAttendences()
        {
            return GetAttendences && AttendeePropertyHelper != null;
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
                else if (Equals(name, nameof(User.Attendences))) GetAttendences = true;
            }
            if (CanGoDeeper(depth))
            {
                MembershipsPropertyHelper = GetMemberPropertyHelper(fields, nameof(User.Memberships), depth);
                AttendeePropertyHelper = GetAttendeePropertyHelper(fields, nameof(User.Attendences), depth);
            }
        }
    }
}