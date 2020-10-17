using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Weather;
using DisputenPWA.Domain.WeatherAggregate.Commands;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL
{
    public class DisputenAppMutations : DisputenAppBaseType
    {
        public DisputenAppMutations(IMediator mediator)
        {
            Field<WeatherResultType>(
                "SetWeather",
                description: "Set weather forecast",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "summary" }
                ),
                resolve: context => mediator.Send(new SetWeatherForecastCommand(context.GetArgument<string>("summary")), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }
    }
}
