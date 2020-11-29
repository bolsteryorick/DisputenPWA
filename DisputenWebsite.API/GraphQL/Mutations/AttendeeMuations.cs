using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;

namespace DisputenPWA.API.GraphQL.Mutations
{
    public partial class DisputenAppMutations
    {
        protected void AddAttendeeMutations(IMediator mediator)
        {
            Field<AttendeeResultType>(
                "CreateAttendee",
                description: "Creates an attendee in the database. This means the user with the sent user id will attend the event.",
                arguments: CreateAttendeeArguments(),
                resolve: context => mediator.Send(CreateAttendeeCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<AttendeeResultType>(
                "JoinEvent",
                description: "Creates an attendee in the database for current user. This means the current user will attend the event.",
                arguments: JoinEventArguments(),
                resolve: context => mediator.Send(JoinEventCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<AttendeeResultType>(
                "DeleteAttendee",
                description: "Deletes attendee by attendee id, useable by group admin.",
                arguments: DeleteAttendeeArguments(),
                resolve: context => mediator.Send(DeleteAttendeeCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<AttendeeResultType>(
                "LeaveEvent",
                description: "Deletes attendee by attendee id, usable by attendee itself.",
                arguments: LeaveEventArguments(),
                resolve: context => mediator.Send(LeaveEventCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<AttendeeResultType>(
               "UpdateAttendee",
               description: "Updates the attendee, only the passed arguments will be updated.",
               arguments: UpdateAttendeeArguments(),
               resolve: context => mediator.Send(UpdateAttendeeCommand(context), context.CancellationToken).Map(r => ProcessResult(context, r))
               );
        }

        private QueryArguments CreateAttendeeArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "eventId" }
            );
        }

        private CreateAttendeeCommand CreateAttendeeCommand(ResolveFieldContext<object> context)
        {
            return new CreateAttendeeCommand(
                context.GetArgument<string>("userId"),
                context.GetArgument<Guid>("eventId")
            );
        }

        private QueryArguments JoinEventArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "eventId" }
            );
        }

        private JoinEventCommand JoinEventCommand(ResolveFieldContext<object> context)
        {
            return new JoinEventCommand(
                context.GetArgument<Guid>("eventId")
            );
        }

        private QueryArguments DeleteAttendeeArguments()
        {
            return new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "attendeeId" }
            );
        }

        private DeleteAttendeeCommand DeleteAttendeeCommand(ResolveFieldContext<object> context)
        {
            return new DeleteAttendeeCommand(
                context.GetArgument<Guid>("attendeeId")
            );
        }

        private QueryArguments LeaveEventArguments()
        {
            return new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "attendeeId" }
            );
        }

        private LeaveEventCommand LeaveEventCommand(ResolveFieldContext<object> context)
        {
            return new LeaveEventCommand(
                context.GetArgument<Guid>("attendeeId")
            );
        }

        private QueryArguments UpdateAttendeeArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "attendeeId" },
                new QueryArgument<BooleanGraphType> { Name = "paid" }
            );
        }

        private UpdateAttendeeCommand UpdateAttendeeCommand(ResolveFieldContext<object> context)
        {
            return new UpdateAttendeeCommand(
                context.GetArgument<Guid>("attendeeId"),
                context.GetArgument<bool?>("paid")
            );
        }
    }
}
