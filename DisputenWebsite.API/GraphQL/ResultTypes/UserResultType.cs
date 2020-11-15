using DisputenPWA.Domain.UserAggregate;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class UserResultType : ObjectGraphType<User>
    {
        public UserResultType()
        {
            Field(f => f.Id, type: typeof(StringGraphType)).Description("The user's id.");
            Field(f => f.Email, type: typeof(StringGraphType)).Description("The user's email.");
            Field(f => f.UserName, type: typeof(StringGraphType)).Description("The user's username.");
            Field(f => f.Memberships, nullable: true, type: typeof(ListGraphType<MemberResultType>)).Description("The group memberships this user.");
        }
    }
}