using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.GroupAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public class DisputenAppMutations : DisputenAppBaseType
    {
        public DisputenAppMutations(IMediator mediator)
        {
            Field<GroupResultType>(
                "CreateGroup",
                description: "Creates a group in the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "description" }
                ),
                resolve: context => mediator.Send(new CreateGroupCommand(context.GetArgument<string>("name"), context.GetArgument<string>("description")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<GroupResultType>(
                "UpdateGroup",
                description: "Updates a group in the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "description" }
                ),
                resolve: context => mediator.Send(new UpdateGroupCommand(context.GetArgument<Guid>("id") ,context.GetArgument<string>("name"), context.GetArgument<string>("description")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<GroupResultType>(
                "DeleteGroup",
                description: "Deletes a group in the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: context => mediator.Send(new DeleteGroupCommand(context.GetArgument<Guid>("id")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }
    }
}
