using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.ContactAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Hierarchy;
using GraphQL.Language.AST;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.UserAggregate
{
    public class UserPropertyHelper : PropertyHelperBase
    {
        public bool GetId { get; }
        public bool GetEmail { get; }
        public bool GetUserName { get; }
        public bool GetGroupMemberships { get; }
        public bool GetAttendences { get; }
        public bool GetContacts { get; }
        public MemberPropertyHelper MembershipsPropertyHelper { get; }
        public AttendeePropertyHelper AttendeePropertyHelper { get; }
        public ContactPropertyHelper ContactPropertyHelper { get; }

        public bool CanGetMembers()
        {
            return GetGroupMemberships && MembershipsPropertyHelper != null;
        }

        public bool CanGetAttendences()
        {
            return GetAttendences && AttendeePropertyHelper != null;
        }

        public bool CanGetContacts()
        {
            return GetContacts && ContactPropertyHelper != null;
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
                else if (Equals(name, nameof(User.Contacts))) GetContacts = true;
            }
            if (CanGoDeeper(depth))
            {
                MembershipsPropertyHelper = GetMemberPropertyHelper(fields, nameof(User.Memberships), depth);
                AttendeePropertyHelper = GetAttendeePropertyHelper(fields, nameof(User.Attendences), depth);
                ContactPropertyHelper = GetContactPropertyHelper(fields, nameof(User.Contacts), depth);
            }
        }
    }
}