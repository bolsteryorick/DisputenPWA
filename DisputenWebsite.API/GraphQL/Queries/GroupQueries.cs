using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Application.Groups.Handlers.Queries;
using DisputenPWA.Domain.GroupAggregate.Helpers;
using DisputenPWA.Domain.GroupAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddGroupQueries(IMediator mediator)
        {
            Field<GroupResultType>(
                "GetGroup", 
                description: "Gets a group from the database.", arguments: GroupArguments(),
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
