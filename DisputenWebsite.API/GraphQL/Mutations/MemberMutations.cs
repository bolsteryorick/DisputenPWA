using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddMemberMutations(IMediator mediator)
        {
            Field<MemberResultType>(
                "CreateMember",
                description: "Creates a member in the database for given user id and group id.",
                arguments: CreateMemberArguments(),
                resolve: context => mediator.Send(CreateMemberCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<MemberResultType>(
                "CreateMembers",
                description: "Creates members in the database for given group id and user ids.",
                arguments: CreateMembersArguments(),
                resolve: context => mediator.Send(CreateMembersCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<MemberResultType>(
                "DeleteMember",
                description: "Deletes member by member id, usable by group admin.",
                arguments: DeleteMemberArguments(),
                resolve: context => mediator.Send(DeleteMemberCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<MemberResultType>(
                "LeaveGroup",
                description: "Deletes member by member id, usable by member itself.",
                arguments: LeaveGroupArguments(),
                resolve: context => mediator.Send(LeaveGroupCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<MemberResultType>(
                "UpdateMember",
                description: "Updates the member, only the passed arguments will be updated.",
                arguments: UpdateMemberArguments(),
                resolve: context => mediator.Send(UpdateMemberCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }

        private QueryArguments CreateMemberArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" },
                new QueryArgument<BooleanGraphType> { Name = "isAdmin" }
            );
        }

        private CreateMemberCommand CreateMemberCommand(ResolveFieldContext<object> context)
        {
            return new CreateMemberCommand(
                context.GetArgument<string>("userId"),
                context.GetArgument<bool>("isAdmin"),
                context.GetArgument<Guid>("groupId")
            );
        }

        private QueryArguments CreateMembersArguments()
        {
            return new QueryArguments(
                new QueryArgument<ListGraphType<StringGraphType>> { Name = "userIds" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "groupId" }
            );
        }

        private CreateMembersCommand CreateMembersCommand(ResolveFieldContext<object> context)
        {
            return new CreateMembersCommand(
                context.GetArgument<List<string>>("userIds"),
                context.GetArgument<Guid>("groupId")
            );
        }

        private QueryArguments DeleteMemberArguments()
        {
            return new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "memberId" }
            );
        }

        private DeleteMemberCommand DeleteMemberCommand(ResolveFieldContext<object> context)
        {
            return new DeleteMemberCommand(
                context.GetArgument<Guid>("memberId")
            );
        }

        private QueryArguments LeaveGroupArguments()
        {
            return new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "memberId" }
            );
        }

        private LeaveGroupCommand LeaveGroupCommand(ResolveFieldContext<object> context)
        {
            return new LeaveGroupCommand(
                context.GetArgument<Guid>("groupId")
            );
        }

        private QueryArguments UpdateMemberArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "memberId" },
                new QueryArgument<BooleanGraphType> { Name = "isAdmin" }
            );
        }

        private UpdateMemberCommand UpdateMemberCommand(ResolveFieldContext<object> context)
        {
            return new UpdateMemberCommand(
                context.GetArgument<Guid>("memberId"),
                context.GetArgument<bool?>("isAdmin")
            );
        }
    }
}