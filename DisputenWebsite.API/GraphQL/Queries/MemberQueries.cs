using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.MemberAggregate;
using DisputenPWA.Domain.Aggregates.MemberAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Linq;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddMemberQueries(IMediator mediator)
        {
            Field<MemberResultType>(
                   "GetMember",
                   description: "Gets the member by member id.",
                   arguments: MemberQueryArguments(),
                   resolve: context => mediator.Send(MemberQuery(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                   );
        }

        private QueryArguments MemberQueryArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "memberId" }
            );
        }

        private MemberQuery MemberQuery(ResolveFieldContext<object> context)
        {
            return new MemberQuery(
                context.GetArgument<Guid>("memberId"),
                GetMemberPropertyHelper(context)
            );
        }

        private MemberPropertyHelper GetMemberPropertyHelper(ResolveFieldContext<object> context)
        {
            return new MemberPropertyHelper(
                context.SubFields.Select(x => x.Value)
            );
        }
    }
}
