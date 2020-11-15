using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.GroupAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddGroupMutations(IMediator mediator)
        {
            Field<GroupResultType>(
                "CreateGroup",
                description: "Creates a group in the database.",
                arguments: CreateGroupArguments(),
                resolve: context => mediator.Send(CreateGroupCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<GroupResultType>(
                "UpdateGroup",
                description: "Updates a group in the database. Properties that are not sent are not updated.",
                arguments: UpdateGroupArguments(),
                resolve: context => mediator.Send(UpdateGroupCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<GroupResultType>(
                "DeleteGroup",
                description: "Deletes a group in the database.",
                arguments: DeleteGroupArguments(),
                resolve: context => mediator.Send(DeleteGroupCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<GroupResultType>(
                "SeedGroups",
                description: "Adds x random groups with random events to database.",
                arguments: SeedGroupsArguments(),
                resolve: context => mediator.Send(SeedGroupsCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }

        private QueryArguments CreateGroupArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "description" }
            );
        }

        private CreateGroupCommand CreateGroupCommand(ResolveFieldContext<object> context)
        {
            return new CreateGroupCommand(
                context.GetArgument<string>("name"),
                context.GetArgument<string>("description")
            );
        }

        private QueryArguments UpdateGroupArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                new QueryArgument<StringGraphType> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "description" }
            );
        }

        private UpdateGroupCommand UpdateGroupCommand(ResolveFieldContext<object> context)
        {
            return new UpdateGroupCommand(
                context.GetArgument<Guid>("id"),
                context.GetArgument<string>("name"),
                context.GetArgument<string>("description")
            );
        }

        private QueryArguments DeleteGroupArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            );
        }

        private DeleteGroupCommand DeleteGroupCommand(ResolveFieldContext<object> context)
        {
            return new DeleteGroupCommand(
                context.GetArgument<Guid>("id")
            );
        }

        private QueryArguments SeedGroupsArguments()
        {
            return new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "nrOfGroups" },
                new QueryArgument<IntGraphType> { Name = "maxEventsPerGroup" },
                new QueryArgument<IntGraphType> { Name = "maxMembersPerGroup" }
            );
        }

        private SeedGroupsCommand SeedGroupsCommand(ResolveFieldContext<object> context)
        {
            return new SeedGroupsCommand(
                context.GetArgument<int>("nrOfGroups"),
                context.GetArgument<int>("maxEventsPerGroup"),
                context.GetArgument<int>("maxMembersPerGroup")
            );
        }
    }
}