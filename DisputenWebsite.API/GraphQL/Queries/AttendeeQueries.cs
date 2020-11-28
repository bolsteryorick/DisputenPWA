using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate;
using DisputenPWA.Domain.Aggregates.AttendeeAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Linq;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddAttendeeQueries(IMediator mediator)
        {
            Field<AttendeeResultType>(
                   "GetAttendee",
                   description: "Gets the member by member id.",
                   arguments: GetAttendeeArguments(),
                   resolve: context => mediator.Send(GetAttendee(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                   );
        }

        private QueryArguments GetAttendeeArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "attendeeId" }
            );
        }

        private GetAttendee GetAttendee(ResolveFieldContext<object> context)
        {
            return new GetAttendee(
                context.GetArgument<Guid>("memberId"),
                GetAttendeePropertyHelper(context)
            );
        }

        private AttendeePropertyHelper GetAttendeePropertyHelper(ResolveFieldContext<object> context)
        {
            return new AttendeePropertyHelper(
                context.SubFields.Select(x => x.Value)
            );
        }
    }
}