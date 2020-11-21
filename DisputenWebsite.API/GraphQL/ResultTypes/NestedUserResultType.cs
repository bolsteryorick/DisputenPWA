using DisputenPWA.Domain.UserAggregate;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class NestedUserResultType : ObjectGraphType<User>
    {
        public NestedUserResultType()
        {
            Field(f => f.Id, type: typeof(StringGraphType)).Description("The user's id.");
            Field(f => f.Email, type: typeof(StringGraphType)).Description("The user's email.");
            Field(f => f.UserName, type: typeof(StringGraphType)).Description("The user's username.");
        }
    }
}
