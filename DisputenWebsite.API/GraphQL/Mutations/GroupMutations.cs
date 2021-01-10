using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.Domain.Aggregates.GroupAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddGroupMutations(IMediator mediator)
        {
            Field<GroupResultType>(
                "CreateGroup",
                description: "Creates a group in the database, current user will be the only member and will be admin.",
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
                "SeedGroups",
                description: "Adds random events, groups, members and attendees to the database.",
                arguments: SeedGroupsArguments(),
                resolve: context => mediator.Send(SeedGroupsCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }

        private QueryArguments CreateGroupArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                new QueryArgument<StringGraphType> { Name = "description" },
                new QueryArgument<ListGraphType<StringGraphType>> { Name = "userIds" }
            );
        }

        private CreateGroupCommand CreateGroupCommand(ResolveFieldContext<object> context)
        {
            return new CreateGroupCommand(
                context.GetArgument<string>("name"),
                context.GetArgument<string>("description"),
                context.GetArgument<List<string>>("userIds")
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

        private QueryArguments SeedGroupsArguments()
        {
            return new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "nrOfGroups" },
                new QueryArgument<IntGraphType> { Name = "maxEventsPerGroup" },
                new QueryArgument<IntGraphType> { Name = "maxMembersPerGroup" },
                new QueryArgument<IntGraphType> { Name = "maxAttendeesPerEvent" }
            );
        }

        private SeedGroupsCommand SeedGroupsCommand(ResolveFieldContext<object> context)
        {
            return new SeedGroupsCommand(
                context.GetArgument<int>("nrOfGroups"),
                context.GetArgument<int>("maxEventsPerGroup"),
                context.GetArgument<int>("maxMembersPerGroup"),
                context.GetArgument<int>("maxAttendeesPerEvent")
            );
        }
    }
}