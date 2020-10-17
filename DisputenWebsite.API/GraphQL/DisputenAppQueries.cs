using DisputenPWA.API.Extensions;
using DisputenPWA.API.GraphQL.Weather;
using DisputenPWA.Domain.WeatherAggregate.Queries;
using GraphQL.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DisputenPWA.API.GraphQL
{
    public class DisputenAppQueries : DisputenAppBaseType
    {
        public DisputenAppQueries(IMediator mediator)
        {
            Field<WeatherResultType>(
                "GetWeather",
                description: "Get weather forecast",
                resolve: context => mediator.Send(new GetWeatherForecastQuery(), context.CancellationToken).Map(r => ProcessResult(context, r))
                );
        }
    }
}
