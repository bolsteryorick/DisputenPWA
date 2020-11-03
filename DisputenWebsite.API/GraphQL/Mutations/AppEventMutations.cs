using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.AppEvents;
using DisputenPWA.Domain.EventAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddAppEventMutations(IMediator mediator)
        {
            Field<AppEventResultType>(
                "CreateAppEvent",
                description: "Creates an app event in the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "startTime" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "endTime" },
                    new QueryArgument<StringGraphType> { Name = "description" }
                ),
                resolve: context =>
                    mediator.Send(
                        new CreateAppEventCommand(
                            context.GetArgument<string>("name"),
                            context.GetArgument<string>("description"),
                            context.GetArgument<DateTime>("startTime"),
                            context.GetArgument<DateTime>("endTime"),
                            context.GetArgument<Guid>("groupId")
                        ),
                        context.CancellationToken
                    ).Map(r => ProcessResult(context, r))
                );

            Field<AppEventResultType>(
                "UpdateAppEvent",
                description: "Updates an app event in the database. Properties that are not sent are not updated.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "name" },
                    new QueryArgument<StringGraphType> { Name = "description" },
                    new QueryArgument<DateTimeGraphType> { Name = "startTime" },
                    new QueryArgument<DateTimeGraphType> { Name = "endTime" }
                ),
                resolve: context =>
                    mediator.Send(
                        new UpdateAppEventCommand(
                            context.GetArgument<Guid>("id"),
                            context.GetArgument<string>("name"),
                            context.GetArgument<string>("description"),
                            context.GetArgument<DateTime>("startTime"),
                            context.GetArgument<DateTime>("endTime")
                        ),
                        context.CancellationToken
                    ).Map(r => ProcessResult(context, r))
                );

            Field<AppEventResultType>(
                "DeleteAppEvent",
                description: "Deletes an app event in the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: context => mediator.Send(new DeleteAppEventCommand(context.GetArgument<Guid>("id")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }
    }
}
