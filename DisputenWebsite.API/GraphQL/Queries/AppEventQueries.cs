using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.AppEvents;
using DisputenPWA.Domain.EventAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;

namespace DisputenPWA.API.GraphQL.Queries
{
    public partial class DisputenAppQueries
    {
        public void AddAppEventQueries(IMediator mediator)
        {
            Field<AppEventResultType>(
                "GetAppEvent",
                description: "Gets an app event from the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: context => mediator.Send(new GetAppEventQuery(context.GetArgument<Guid>("id")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }
    }
}
