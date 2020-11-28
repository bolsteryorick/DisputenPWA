using DisputenPWA.Domain.Aggregates.UserAggregate;
using GraphQL.Types;

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
            Field(f => f.Attendences, nullable: true, type: typeof(ListGraphType<AttendeeResultType>)).Description("The group memberships this user.");
        }
    }
}