using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisputenPWA.Domain.WeatherAggregate.Queries
{
    public class GetWeatherForecastQuery : IRequest<GetWeatherForecastQueryResult>
    {
        public string Date { get; }

        public GetWeatherForecastQuery(string date)
        {
            Date = date;
        }
    }
}
