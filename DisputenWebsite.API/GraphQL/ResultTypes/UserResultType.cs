using DisputenPWA.Domain.UserAggregate;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL.ResultTypes
{
    public class UserResultType : ObjectGraphType<User>
    {
        public UserResultType()
        {
            Field(f => f.UserName, nullable: false).Description("The name of the user.");
        }
    }
}
