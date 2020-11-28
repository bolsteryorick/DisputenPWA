using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using GraphQL.Types;

namespace DisputenPWA.API.GraphQL.Groups
{
    public class GroupResultType : ObjectGraphType<Group>
    {
        public GroupResultType()
        {
            Field(f => f.Id, type: typeof(IdGraphType)).Description("The group id, unique identifier in the database.");
            Field(f => f.Name, nullable: true).Description("The name of the group.");
            Field(f => f.Description, nullable: true).Description("The description of the group.");
            Field(f => f.AppEvents, nullable: true, type: typeof(ListGraphType<AppEventResultType>)).Description("The events of the group.");
            Field(f => f.Members, nullable: true, type: typeof(ListGraphType<MemberResultType>)).Description("The members of the group.");
        }
    }
}