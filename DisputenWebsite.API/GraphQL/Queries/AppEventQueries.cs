using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.ResultTypes;
using DisputenPWA.Domain.EventAggregate.Queries;
using DisputenPWA.Domain.Helpers.PropertyHelpers;
using GraphQL.Types;
using MediatR;
using System;
using System.Linq;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        public void AddAppEventQueries(IMediator mediator)
        {
            Field<AppEventResultType>(
                "GetAppEvent",
                description: "Gets an app event from the database.",
                arguments: AppEventArguments(),
                resolve: context => mediator.Send(AppEventQuery(context), context.CancellationToken).Map(r => ProcessResult(context, r)));
        }

        private QueryArguments AppEventArguments()
        {
            return new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
            );
        }

        private AppEventQuery AppEventQuery(ResolveFieldContext<object> context)
        {
            return new AppEventQuery(
                context.GetArgument<Guid>("id"),
                new AppEventPropertyHelper(context.SubFields.Select(x => x.Value))
            );
        }
    }
}