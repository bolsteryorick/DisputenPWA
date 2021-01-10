using DisputenPWA.Domain.Aggregates.ContactAggregate;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class ContactResultType : ObjectGraphType<Contact>
    {
        public ContactResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The contact id, unique identifier in the database.");
            Field(f => f.UserId).Description("The userid of the user whose contact this is.");
            Field(f => f.ContactUserId, type: typeof(IdGraphType)).Description("The userid of the user who is the contact.");
            Field(f => f.EmailAddress, type: typeof(IdGraphType)).Description("The email address of the user who is the contact.");
            Field(f => f.User, type: typeof(UserResultType)).Description("The user who is the contact, can be null.");
        }
    }
}
