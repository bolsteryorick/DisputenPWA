using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Groups;
using DisputenPWA.API.GraphQL.Weather;
using DisputenPWA.Domain.GroupAggregate.Queries;
using DisputenPWA.Domain.WeatherAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL.Queries
{
    public class DisputenAppQueries : DisputenAppBaseType
    {
        public DisputenAppQueries(IMediator mediator)
        {
            Field<WeatherResultType>(
                "GetWeather",
                description: "Get weather forecast",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "date" }
                ),
                resolve: context => mediator.Send(new GetWeatherForecastQuery(context.GetArgument<string>("date")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );

            Field<GroupResultType>(
                "GetGroup",
                description: "Gets a group from the database.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: context => mediator.Send(new GetGroupQuery(context.GetArgument<Guid>("id")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }
    }
}
