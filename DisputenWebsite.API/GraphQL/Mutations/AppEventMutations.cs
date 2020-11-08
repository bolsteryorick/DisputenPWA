using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.EventAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddAppEventMutations(IMediator mediator)
        {
            Field<AppEventResultType>(
                "CreateAppEvent",
                description: "Creates an app event in the database.",
                arguments: CreateAppEventArguments(),
                resolve: context => mediator.Send(CreateAppEventCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<AppEventResultType>(
                "UpdateAppEvent",
                description: "Updates an app event in the database. Properties that are not sent are not updated.",
                arguments: UpdateAppEventArguments(),
                resolve: context => mediator.Send(UpdateAppEventCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<AppEventResultType>(
                "DeleteAppEvent",
                description: "Deletes an app event in the database.",
                arguments: DeleteAppEventArguments(),
                resolve: context => mediator.Send(DeleteAppEventCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }

        private QueryArguments CreateAppEventArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" },
                new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "startTime" },
                new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "endTime" },
                new QueryArgument<StringGraphType> { Name = "description" }
            );
        }

        private CreateAppEventCommand CreateAppEventCommand(ResolveFieldContext<object> context)
        {
            return new CreateAppEventCommand(
                context.GetArgument<string>("name"),
                context.GetArgument<string>("description"),
                context.GetArgument<DateTime>("startTime"),
                context.GetArgument<DateTime>("endTime"),
                context.GetArgument<Guid>("groupId")
            );
        }

        private QueryArguments UpdateAppEventArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                new QueryArgument<StringGraphType> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "description" },
                new QueryArgument<DateTimeGraphType> { Name = "startTime" },
                new QueryArgument<DateTimeGraphType> { Name = "endTime" }
            );
        }

        private UpdateAppEventCommand UpdateAppEventCommand(ResolveFieldContext<object> context)
        {
            return new UpdateAppEventCommand(
                context.GetArgument<Guid>("id"),
                context.GetArgument<string>("name"),
                context.GetArgument<string>("description"),
                context.GetArgument<DateTime>("startTime"),
                context.GetArgument<DateTime>("endTime")
            );
        }

        private QueryArguments DeleteAppEventArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            );
        }

        private DeleteAppEventCommand DeleteAppEventCommand(ResolveFieldContext<object> context)
        {
            return new DeleteAppEventCommand(
                context.GetArgument<Guid>("id")
            );
        }
    }
}
