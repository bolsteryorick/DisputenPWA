using DisputenPWA.Domain.Aggregates.EventAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Helpers;
using DisputenPWA.Domain.Hierarchy;
using GraphQL.Language.AST;
using System.Collections.Generic;

namespace DisputenPWA.Domain.Aggregates.AttendeeAggregate
{
    public class AttendeePropertyHelper : PropertyHelperBase
    {
        public bool GetPaid { get; }
        public bool GetUser { get; }
        public bool GetAppEvent { get; }

        public UserPropertyHelper UserPropertyHelper { get; }
        public AppEventPropertyHelper AppEventPropertyHelper { get; }

        public bool CanGetUser()
        {
            return GetUser && UserPropertyHelper != null;
        }

        public bool CanGetAppEvent()
        {
            return GetAppEvent && AppEventPropertyHelper != null;
        }

        public AttendeePropertyHelper(
            IEnumerable<Field> fields,
            int depth = 1)
        {
            var propertyNames = GetPropertyNames(fields);
            foreach (var name in propertyNames)
            {
                if (Equals(name, nameof(Attendee.Paid))) GetPaid = true;
                else if (Equals(name, nameof(Attendee.User))) GetUser = true;
                else if (Equals(name, nameof(Attendee.AppEvent))) GetAppEvent = true;
            }
            if (CanGoDeeper(depth))
            {
                UserPropertyHelper = GetUserPropertyHelper(fields, nameof(Attendee.User), depth);
                AppEventPropertyHelper = GetAppEventPropertyHelper(fields, nameof(Attendee.AppEvent), null, null, depth);
            }
        }
    }
}
