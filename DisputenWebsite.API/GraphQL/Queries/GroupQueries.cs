using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.Aggregates.GroupAggregate;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Linq;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddGroupQueries(IMediator mediator)
        { 
            Field<GroupResultType>(
                "GetGroup", 
                description: "Gets a group from the database. This is the only way to get events for a group outside the standard event range.", arguments: GroupArguments(),
                resolve: context => mediator.Send(GroupQuery(context), context.CancellationToken).Map(r => ProcessResult(context, r))
            );
        }

        private QueryArguments GroupArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                new QueryArgument<DateTimeGraphType> { Name = "lowestEndDate" },
                new QueryArgument<DateTimeGraphType> { Name = "highestStartDate" }
            );
        }

        private GroupQuery GroupQuery(ResolveFieldContext<object> context)
        {
            return new GroupQuery(
                context.GetArgument<Guid>("id"),
                CreateGroupPropertyHelper(context)
            );
        }

        private GroupPropertyHelper CreateGroupPropertyHelper(ResolveFieldContext<object> context)
        {
            return new GroupPropertyHelper(
                context.SubFields.Select(x => x.Value),
                context.GetArgument<DateTime?>("lowestEndDate"),
                context.GetArgument<DateTime?>("highestStartDate")
            );
        }

    }
}
