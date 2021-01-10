using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Hierarchy;
using GraphQL.Language.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.Aggregates.ContactAggregate
{
    public class ContactPropertyHelper : PropertyHelperBase
    {
        public bool GetEmailAddress { get; set; }
        public bool GetUser { get; set; }

        public UserPropertyHelper UserPropertyHelper { get; }

        public bool CanGetUser()
        {
            return GetUser && UserPropertyHelper != null;
        }

        public ContactPropertyHelper(
            IEnumerable<Field> fields,
            int depth = 1
            )
        {
            var propertyNames = GetPropertyNames(fields);
            foreach(var name in propertyNames)
            {
                if (Equals(name, nameof(Contact.EmailAddress))) GetEmailAddress = true;
                else if (Equals(name, nameof(Contact.User))) GetUser = true;
            }
            if (CanGoDeeper(depth))
            {
                UserPropertyHelper = GetUserPropertyHelper(fields, nameof(Contact.User), depth);
            }
        }
    }
}
