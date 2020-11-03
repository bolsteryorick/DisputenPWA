using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
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
                description: "Gets a group from the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<DateTimeGraphType> { Name = "lowestEndDate" },
                    new QueryArgument<DateTimeGraphType> { Name = "highestStartDate" }
                ),
                resolve: context => mediator.Send(
                    new GetGroupQuery(
                        context.GetArgument<Guid>("id"),
                        context.GetArgument<DateTime?>("lowestEndDate"),
                        context.GetArgument<DateTime?>("highestStartDate"),
                        new GroupPropertyHelper(context.SubFields.Select(x => x.Value))
                    ), 
                    context.CancellationToken
                    )
                .Map(r => ProcessResult(context, r))
                );
        }


    }
}
