using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System.Linq;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        protected void AddUserQueries(IMediator mediator)
        {
            Field<UserResultType>(
                    "GetUser",
                    description: "Gets the current user.",
                    resolve: context => mediator.Send(GetUserQuery(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<NestedUserResultType>(
                    "GetOtherUser",
                    description: "Gets another user.",
                    arguments: OtherUserQueryArguments(),
                    resolve: context => mediator.Send(OtherUserQuery(context), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }

        private UserQuery GetUserQuery(ResolveFieldContext<object> context)
        {
            return new UserQuery(GetUserPropertyHelper(context));
        }

        private UserPropertyHelper GetUserPropertyHelper(ResolveFieldContext<object> context)
        {
            return new UserPropertyHelper(
                context.SubFields.Select(x => x.Value)
            );
        }

        private QueryArguments OtherUserQueryArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "userId" }
            );
        }

        private OtherUserQuery OtherUserQuery(ResolveFieldContext<object> context)
        {
            return new OtherUserQuery(
                context.GetArgument<string>("userId")
            );
        }
    }
}
