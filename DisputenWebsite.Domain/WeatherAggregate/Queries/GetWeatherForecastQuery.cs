using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.WeatherAggregate.Queries
{
    public class GetWeatherForecastQuery : IRequest<GetWeatherForecastQueryResult>
    {
        public GetWeatherForecastQuery()
        {
        }
    }
}
