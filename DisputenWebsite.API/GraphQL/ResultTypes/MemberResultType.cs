using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.MemberAggregate;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class MemberResultType : ObjectGraphType<Member>
    {
        public MemberResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The group id, unique identifier in the database.");
            Field(f => f.UserId, nullable: true).Description("The id of the user.");
            Field(f => f.GroupId, type: typeof(IdGraphType), nullable: true).Description("The id of the group.");
            Field(f => f.Group, nullable: true, type: typeof(GroupResultType)).Description("The group this member has membership in.");
            Field(f => f.User, nullable: true, type: typeof(NestedUserResultType)).Description("The user data for this member.");
        }
    }
}
